using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FrtAfcApiClient;

namespace OfflineTicketValidationTestProgram
{
    internal class Program
    {
        private static FareApiClient apiClient;
        private static List<CachedKeyPair> cachedKeyPairs = new List<CachedKeyPair>();

        /// <summary>
        /// Represents a cached key pair for offline validation
        /// </summary>
        public class CachedKeyPair
        {
            public int KeyVersion { get; set; }
            public ECDsa PublicKey { get; set; }
            public byte[] XorKey { get; set; }
            public DateTime KeyCreated { get; set; }
            public DateTime KeyExpiry { get; set; }

            public bool IsValid => DateTime.Now <= KeyExpiry;

            public void Dispose()
            {
                PublicKey?.Dispose();
            }
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Offline Ticket Validation Test Program ===");
            Console.WriteLine("Using .NET Framework 4.8 native cryptography");
            Console.WriteLine();

            try
            {
                // Step 1: Load configuration
                await LoadConfiguration();

                // Step 2: Initialize API client and authenticate
                await InitializeApiClient();

                // Step 3: Fetch cryptographic keys from server
                await RefreshCachedKeysAsync();

                // Step 4: Test offline validation
                await TestOfflineValidation();

                Console.WriteLine();
                Console.WriteLine("=== Testing Complete ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                Console.WriteLine($"Details: {ex}");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static async Task LoadConfiguration()
        {
            Console.WriteLine("1. Loading configuration...");

            string configPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "faregate_config.txt");

            Console.WriteLine($"   Config path: {configPath}");

            if (!File.Exists(configPath))
            {
                Console.WriteLine("   Config file not found. Creating default config...");

                var defaultConfig = @"API_ENDPOINT=http://127.0.0.1:5281
API_USERNAME=testfaregate
API_PASSWORD=testpassword
CURRENT_STATION=FLZ
FAREGATE_DIRECTION=ENTRY";
                File.WriteAllText(configPath, defaultConfig);
                Console.WriteLine($"   Created default config file at: {configPath}");
                Console.WriteLine("   Please update the config file with correct values and restart.");
                return;
            }

            SimpleConfig.Load(configPath);
            Console.WriteLine("   Configuration loaded successfully.");
        }

        private static async Task InitializeApiClient()
        {
            Console.WriteLine("2. Initializing API client...");

            string apiEndpoint = SimpleConfig.Get("API_ENDPOINT", "http://127.0.0.1:5281");
            string username = SimpleConfig.Get("API_USERNAME", "testfaregate");
            string password = SimpleConfig.Get("API_PASSWORD", "testpassword");

            Console.WriteLine($"   API endpoint: {apiEndpoint}");
            Console.WriteLine($"   Username: {username}");
            Console.WriteLine($"   Password: {new string('*', password.Length)}");

            apiClient = new FareApiClient(apiEndpoint);
            apiClient.SetBasicAuthentication(username, password);

            // Test authentication
            Console.WriteLine("   Testing authentication...");
            bool authSuccess = await apiClient.TestCredentialsAsync();

            if (authSuccess)
            {
                Console.WriteLine("   ✓ Authentication successful");
            }
            else
            {
                throw new Exception("Authentication failed. Please check your credentials.");
            }
        }

        private static async Task RefreshCachedKeysAsync()
        {
            Console.WriteLine("3. Fetching validation keys from server...");

            try
            {
                var validationKeys = await apiClient.GetValidationKeysAsync();
                Console.WriteLine($"   Retrieved {validationKeys.Count} validation keys from server");

                foreach (var keyInfo in validationKeys)
                {
                    Console.WriteLine($"   Processing key version {keyInfo.KeyVersion}, valid from {keyInfo.StartDateTime} to {keyInfo.ExpiryDateTime}");

                    try
                    {
                        var publicKey = FrtTicket.CreatePublicKeyFromBase64(keyInfo.PublicKey);
                        var xorKeyBytes = Convert.FromBase64String(keyInfo.XorKey);

                        var cachedKey = new CachedKeyPair
                        {
                            KeyVersion = keyInfo.KeyVersion,
                            PublicKey = publicKey,
                            XorKey = xorKeyBytes,
                            KeyCreated = keyInfo.StartDateTime,
                            KeyExpiry = keyInfo.ExpiryDateTime
                        };

                        cachedKeyPairs.Add(cachedKey);
                        Console.WriteLine($"   ✓ Cached key version {keyInfo.KeyVersion}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"   ✗ Failed to process key version {keyInfo.KeyVersion}: {ex.Message}");
                    }
                }

                Console.WriteLine($"   Total cached keys: {cachedKeyPairs.Count}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch validation keys from server: {ex.Message}", ex);
            }
        }

        private static async Task TestOfflineValidation()
        {
            Console.WriteLine("4. Testing offline validation...");

            if (cachedKeyPairs.Count == 0)
            {
                Console.WriteLine("   ✗ No cached keys available for testing");
                return;
            }

            Console.WriteLine("   Please enter ticket QR codes to test (empty line to finish):");

            while (true)
            {
                Console.Write("   Ticket QR code: ");
                string ticketCode = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(ticketCode))
                    break;

                await ValidateTicketOffline(ticketCode);
            }
        }

        private static async Task ValidateTicketOffline(string ticketCode)
        {
            Console.WriteLine($"   Testing ticket: {ticketCode.Substring(0, Math.Min(20, ticketCode.Length))}...");

            var validKeys = cachedKeyPairs.Where(k => k.IsValid).OrderByDescending(k => k.KeyVersion).ToList();

            if (validKeys.Count == 0)
            {
                Console.WriteLine("   ✗ No valid cached keys available");
                return;
            }

            foreach (var keyPair in validKeys)
            {
                try
                {
                    Console.WriteLine($"   Trying key version {keyPair.KeyVersion}...");

                    bool decoded = FrtTicket.TryDecodeTicket(
                        encodedTicket: ticketCode,
                        signingKey: keyPair.PublicKey,
                        xorObfuscatorKey: keyPair.XorKey,
                        out long ticketNumber,
                        out int valueCents,
                        out string issuingStation,
                        out DateTime issueDateTime,
                        out byte ticketType);

                    if (decoded)
                    {
                        Console.WriteLine($"   ✓ Successfully decoded with key version {keyPair.KeyVersion}");
                        Console.WriteLine($"      Ticket Number: {ticketNumber}");
                        Console.WriteLine($"      Value: ¥{valueCents / 100.0:F2} ({valueCents} cents)");
                        Console.WriteLine($"      Issuing Station: {issuingStation}");
                        Console.WriteLine($"      Issue Date/Time: {issueDateTime}");
                        Console.WriteLine($"      Ticket Type: {GetTicketTypeName(ticketType)} ({ticketType})");

                        var validationResult = PerformValidationChecks(ticketNumber, valueCents, issuingStation, issueDateTime, ticketType);
                        if (validationResult.IsValid)
                        {
                            Console.WriteLine($"   ✓ Ticket validation: PASSED");
                        }
                        else
                        {
                            Console.WriteLine($"   ✗ Ticket validation: FAILED - {validationResult.ErrorMessage}");
                        }
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"   ✗ Key version {keyPair.KeyVersion} failed to decode ticket");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"   ✗ Error with key version {keyPair.KeyVersion}: {ex.Message}");
                }
            }

            Console.WriteLine("   ✗ Failed to decode ticket with any available keys");
        }

        private static string GetTicketTypeName(byte ticketType)
        {
            switch (ticketType)
            {
                case 0: return "Full Fare";
                case 1: return "Student";
                case 2: return "Senior";
                case 3: return "Free Exit";
                case 4: return "Day Pass";
                case 255: return "Debug";
                default: return "Unknown";
            }
        }

        private static (bool IsValid, string ErrorMessage) PerformValidationChecks(
            long ticketNumber, int valueCents, string issuingStation, DateTime issueDateTime, byte ticketType)
        {
            string faregateDirection = SimpleConfig.Get("FAREGATE_DIRECTION", "ENTRY").ToUpper();
            string currentStation = SimpleConfig.Get("CURRENT_STATION", "FLZ");

            if (issueDateTime.AddHours(24) < DateTime.Now)
            {
                return (false, "Ticket expired");
            }

            if (ticketType > 4 && ticketType != 255)
            {
                return (false, "Invalid ticket type");
            }

            if (ticketType == 4 && issueDateTime.Date != DateTime.Now.Date)
            {
                return (false, "Day pass not valid for today");
            }

            if (ticketType != 3 && valueCents <= 0)
            {
                return (false, "Ticket has no value");
            }

            if (faregateDirection == "ENTRY")
            {
                if (ticketType == 3)
                {
                    return (false, "Free exit ticket cannot be used for entry");
                }
                if (ticketType >= 0 && ticketType <= 2)
                {
                    if (!string.Equals(issuingStation, currentStation, StringComparison.OrdinalIgnoreCase))
                    {
                        return (false, $"Ticket issued at {issuingStation}, cannot enter at {currentStation}");
                    }
                }
            }
            else if (faregateDirection == "EXIT")
            {
                if (ticketType == 3)
                {
                    if (!string.Equals(issuingStation, currentStation, StringComparison.OrdinalIgnoreCase))
                    {
                        return (false, $"Free exit ticket can only be used at issuing station {issuingStation}");
                    }
                }
            }

            return (true, string.Empty);
        }
    }

    /// <summary>
    /// Simple configuration file reader (same as faregate)
    /// </summary>
    public static class SimpleConfig
    {
        private static Dictionary<string, string> config = new Dictionary<string, string>();

        public static void Load(string filePath)
        {
            config.Clear();
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue;

                var parts = line.Split('=');
                if (parts.Length == 2)
                {
                    config[parts[0].Trim()] = parts[1].Trim();
                }
            }
        }

        public static string Get(string key, string defaultValue = "")
        {
            return config.TryGetValue(key, out string value) ? value : defaultValue;
        }
    }
}