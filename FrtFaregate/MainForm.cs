using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Net.Http;
using System.Net.Http.Headers;
using FrtAfcApiClient;

namespace FrtFaregate
{
    public partial class MainForm : Form
    {
        // Unmanaged code to hide the blinking cursor in the text box
        [DllImport("user32.dll")]
        private static extern bool HideCaret(IntPtr hWnd);

        const string DefaultMiddleText = "请刷卡或扫码";
        const string DefaultBottomText = "Please Tap Card or Scan Ticket";

        // Cache animation images to prevent RAM growth
        private Image tapCardImage;
        private Image successImage;
        private Image failImage;

        // Cache sound effects to prevent RAM growth
        private SoundPlayer successSound;
        private SoundPlayer seniorTicketSound;
        private SoundPlayer studentTicketSound;
        private SoundPlayer failSound;

        // Cached cryptographic keys for offline validation - now supports multiple key versions
        private List<CachedKeyPair> cachedKeyPairs = new List<CachedKeyPair>();
        private DateTime cachedKeysLastRefresh = DateTime.MinValue;

        // API client for online validation
        private FareApiClient apiClient;
        private Timer keyRefreshTimer;

        // File-based blacklist path (replacing SQLite)
        private readonly string blacklistFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "local_faregate_blacklist.txt");

        enum DisplayType
        {
            RegularTap,
            SeniorTicket,
            StudentTicket,
            Fail
        }

        /// <summary>
        /// Represents a cached key pair with version and expiry information
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

        public MainForm()
        {
            InitializeComponent();
            CenterControlHorizontally(UserPromptPictureBox);
            CenterControlHorizontally(MiddleTextLabel);
            CenterControlHorizontally(BottomTextLabel);

            // Load images in RAM
            tapCardImage = Properties.Resources.TapCard;
            successImage = Properties.Resources.CheckSign;
            failImage = Properties.Resources.XSign;

            // Preload sound effects into memory to prevent stuttering during playback
            successSound = LoadSoundFromResource(Properties.Resources.RegularTap);
            seniorTicketSound = LoadSoundFromResource(Properties.Resources.SeniorTap);
            studentTicketSound = LoadSoundFromResource(Properties.Resources.StudentTap);
            failSound = LoadSoundFromResource(Properties.Resources.Fail);

            // Set the text
            MiddleTextLabel.Text = DefaultMiddleText;
            BottomTextLabel.Text = DefaultBottomText;

            // Create the event that hides the caret when the text box gains focus
            TicketScanTextBox.GotFocus += (s, e) => HideCaret(TicketScanTextBox.Handle);

            // Initialize API client and key refresh system
            InitializeApiClient();
            InitializeBlacklistDatabase();
            InitializeKeyRefreshTimer();
        }

        /// <summary>
        /// Initializes the API client for online ticket validation
        /// </summary>
        private void InitializeApiClient()
        {
            try
            {
                // Load configuration with better error handling
                string configPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "faregate_config.txt");

                if (!File.Exists(configPath))
                {
                    // Create default config file
                    var defaultConfig = @"API_ENDPOINT=http://127.0.0.1:5281
API_USERNAME=testfaregate
API_PASSWORD=testpassword
CURRENT_STATION=FLZ
FAREGATE_DIRECTION=ENTRY";
                    File.WriteAllText(configPath, defaultConfig);
                    Console.WriteLine($"Created default config file at: {configPath}");
                }

                SimpleConfig.Load(configPath);
                string apiEndpoint = SimpleConfig.Get("API_ENDPOINT", "http://127.0.0.1:5281");

                // Create API client
                apiClient = new FareApiClient(apiEndpoint);

                // Set authentication
                string username = SimpleConfig.Get("API_USERNAME", "testfaregate");
                string password = SimpleConfig.Get("API_PASSWORD", "testpassword");
                apiClient.SetBasicAuthentication(username, password);

                Console.WriteLine($"API client initialized with endpoint: {apiEndpoint}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Failed to initialize API client: {ex.Message}");
            }
        }

        /// <summary>
        /// Initializes the file-based blacklist for offline ticket tracking
        /// </summary>
        private void InitializeBlacklistDatabase()
        {
            try
            {
                if (!File.Exists(blacklistFilePath))
                {
                    // Create empty blacklist file
                    File.WriteAllText(blacklistFilePath, "");
                    Console.WriteLine($"Created blacklist file at: {blacklistFilePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Failed to initialize blacklist file: {ex.Message}");
            }
        }

        /// <summary>
        /// Initializes the timer that refreshes cryptographic keys from the server
        /// </summary>
        private void InitializeKeyRefreshTimer()
        {
            // Refresh keys on startup
            _ = RefreshCachedKeysAsync();

            // Set up timer to refresh keys every 5 minutes
            keyRefreshTimer = new Timer();
            keyRefreshTimer.Interval = 5 * 60 * 1000; // 5 minutes
            keyRefreshTimer.Tick += async (sender, e) => await RefreshCachedKeysAsync();
            keyRefreshTimer.Start();
        }

        /// <summary>
        /// Processes a scanned ticket QR code, attempting online validation first, then offline validation
        /// </summary>
        /// <param name="scannedCode">The QR code string from the ticket</param>
        private async Task ProcessScannedTicketAsync(string scannedCode)
        {
            if (string.IsNullOrWhiteSpace(scannedCode))
            {
                DisplayScanResult(DisplayType.Fail, "无效扫码", "Invalid Scan");
                return;
            }

            // Check blacklist first (for offline-validated tickets)
            if (await IsTicketBlacklistedAsync(scannedCode))
            {
                DisplayScanResult(DisplayType.Fail, "车票已使用", "Ticket Already Used");
                return;
            }

            // Step 1: Attempt online validation first
            var onlineResult = await ValidateTicketOnlineAsync(scannedCode);
            
            if (onlineResult.Success)
            {
                // Online validation successful
                DisplayValidationResult(onlineResult);
                return;
            }

            // Check if the online validation failed due to a definitive server response
            // If the server responded with a validation error, respect that decision
            if (onlineResult.IsDefinitiveFailure)
            {
                Console.WriteLine($"Server definitively rejected ticket: {onlineResult.ErrorMessage}");
                DisplayScanResult(DisplayType.Fail, "车票无效", onlineResult.ErrorMessage ?? "Server Rejected Ticket");
                return;
            }

            // Step 2: Only attempt offline validation if online validation failed due to connectivity issues
            Console.WriteLine($"Online validation failed due to connectivity: {onlineResult.ErrorMessage}. Attempting offline validation...");

            var offlineResult = await ValidateTicketOfflineAsync(scannedCode);
            if (offlineResult.Success)
            {
                // Offline validation successful - add to blacklist to prevent reuse
                await AddTicketToBlacklistAsync(scannedCode);
                DisplayValidationResult(offlineResult);
                return;
            }

            // Both online and offline validation failed
            DisplayScanResult(DisplayType.Fail, "车票无效", "Invalid Ticket");
        }

        /// <summary>
        /// Attempts to validate a ticket online using the API
        /// </summary>
        /// <param name="ticketCode">The ticket QR code</param>
        /// <returns>Validation result</returns>
        private async Task<TicketValidationResult> ValidateTicketOnlineAsync(string ticketCode)
        {
            try
            {
                if (apiClient == null)
                {
                    return TicketValidationResult.Failed("API client not initialized", isDefinitive: false);
                }

                // Get current station code and faregate direction from config
                string currentStation = SimpleConfig.Get("CURRENT_STATION", "FLZ");
                string faregateDirection = SimpleConfig.Get("FAREGATE_DIRECTION", "ENTRY").ToUpper();

                // Now we can pass the QR code directly to the API - no need to decode first!
                ValidateTicketResponse response;
                if (faregateDirection == "EXIT")
                {
                    response = await apiClient.ValidateTicketAtExitAsync(ticketCode, currentStation);
                }
                else // Default to ENTRY if not explicitly set to EXIT
                {
                    response = await apiClient.ValidateTicketAtEntryAsync(ticketCode, currentStation);
                }

                return TicketValidationResult.FromApiResponse(response, faregateDirection);
            }
            catch (TicketValidationException ex)
            {
                // These are definitive server responses - the server processed the request and rejected it
                string userMessage = ex.Message;
                if (ex.Message.Contains("Invalid QR code"))
                {
                    userMessage = "无效二维码";
                }
                else if (ex.Message.Contains("QR code decoding failed"))
                {
                    userMessage = "二维码解码失败";
                }
                else if (ex.Message.Contains("Ticket not found"))
                {
                    userMessage = "车票未找到";
                }
                else if (ex.Message.Contains("not in valid state"))
                {
                    userMessage = "车票状态无效";
                }
                else if (ex.Message.Contains("cannot be used for entry"))
                {
                    userMessage = "车票不能用于入站";
                }
                else if (ex.Message.Contains("cannot be used for exit"))
                {
                    userMessage = "车票不能用于出站";
                }
                else if (ex.Message.Contains("Insufficient fare"))
                {
                    userMessage = "车票金额不足";
                }
                
                return TicketValidationResult.Failed(userMessage, isDefinitive: true);
            }
            catch (HttpRequestException ex)
            {
                // Network/connectivity issues - not definitive
                return TicketValidationResult.Failed($"网络连接错误: {ex.Message}", isDefinitive: false);
            }
            catch (TaskCanceledException ex)
            {
                // Timeout - not definitive
                return TicketValidationResult.Failed("请求超时", isDefinitive: false);
            }
            catch (Exception ex)
            {
                // Other errors - treat as connectivity issues for safety
                return TicketValidationResult.Failed($"在线验证错误: {ex.Message}", isDefinitive: false);
            }
        }

        /// <summary>
        /// Refreshes cached cryptographic keys from the server for offline validation
        /// This method now handles multiple valid key pairs and maintains a collection
        /// </summary>
        private async Task RefreshCachedKeysAsync()
        {
            try
            {
                if (apiClient == null)
                {
                    Console.WriteLine("Warning: API client not available for key refresh");
                    return;
                }

                // Get ALL current day's signing keys from the server
                var signingKeys = await apiClient.GetCurrentDaySigningKeysAsync();

                Console.WriteLine($"Retrieved {signingKeys.Count} signing keys from server");

                // Process each signing key
                foreach (var keyInfo in signingKeys)
                {
                    // Check if we already have this key version
                    bool keyExists = cachedKeyPairs.Any(k => k.KeyVersion == keyInfo.KeyVersion);

                    if (!keyExists)
                    {
                        // Parse and create new key pair
                        var publicKey = ECDsa.Create();
                        var publicKeyBytes = Convert.FromBase64String(keyInfo.PublicKey);

                        // Import public key - handle .NET Framework compatibility
                        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                        {
                            try
                            {
                                // For .NET Framework on Windows, try to import as ECDSA public key
                                var cngKey = System.Security.Cryptography.CngKey.Import(
                                    publicKeyBytes,
                                    System.Security.Cryptography.CngKeyBlobFormat.EccPublicBlob);
                                publicKey = new System.Security.Cryptography.ECDsaCng(cngKey);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Warning: Failed to import public key version {keyInfo.KeyVersion}: {ex.Message}");
                                publicKey.Dispose();
                                continue; // Skip this key and try the next one
                            }
                        }
                        else
                        {
                            throw new PlatformNotSupportedException("ECDsa public key import is only supported on Windows with .NET Framework.");
                        }

                        // Create new cached key pair
                        var cachedKey = new CachedKeyPair
                        {
                            KeyVersion = keyInfo.KeyVersion,
                            PublicKey = publicKey,
                            XorKey = Convert.FromBase64String(keyInfo.XorKey),
                            KeyCreated = keyInfo.KeyCreated,
                            // Keys are valid until next rotation (3 AM next day)
                            KeyExpiry = DateTime.Now.Date.AddDays(1).AddHours(3)
                        };

                        // Add to our collection
                        cachedKeyPairs.Add(cachedKey);

                        Console.WriteLine($"Added cached key version {keyInfo.KeyVersion}, created: {keyInfo.KeyCreated}, expires: {cachedKey.KeyExpiry}");
                    }
                    else
                    {
                        Console.WriteLine($"Key version {keyInfo.KeyVersion} already cached, skipping");
                    }
                }

                // Clean up expired keys
                CleanupExpiredKeys();

                // Update last refresh time
                cachedKeysLastRefresh = DateTime.Now;

                Console.WriteLine($"Key refresh complete. Total cached keys: {cachedKeyPairs.Count}, Active keys: {cachedKeyPairs.Count(k => k.IsValid)}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Warning: Failed to refresh cached keys due to network error: {ex.Message}");
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine($"Warning: Key refresh timed out: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Warning: Unauthorized access when refreshing keys - check faregate permissions: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Failed to refresh cached keys: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets all currently valid key pairs for offline validation
        /// </summary>
        private List<CachedKeyPair> GetValidKeys()
        {
            CleanupExpiredKeys(); // Clean up first
            var validKeys = cachedKeyPairs.Where(k => k.IsValid).ToList();
            
            // Log key status for debugging
            if (validKeys.Count == 0)
            {
                Console.WriteLine("Warning: No valid cached keys available for offline validation");
            }
            else
            {
                Console.WriteLine($"Found {validKeys.Count} valid cached keys for offline validation");
            }
            
            return validKeys;
        }

        /// <summary>
        /// Attempts to validate a ticket offline using cached cryptographic keys
        /// Now tries all valid cached keys until one works
        /// </summary>
        /// <param name="ticketCode">The ticket QR code</param>
        /// <returns>Validation result</returns>
        private async Task<TicketValidationResult> ValidateTicketOfflineAsync(string ticketCode)
        {
            try
            {
                var validKeys = GetValidKeys();

                if (validKeys.Count == 0)
                {
                    return TicketValidationResult.Failed("No valid cached keys for offline validation", isDefinitive: false);
                }

                Console.WriteLine($"Attempting offline validation with {validKeys.Count} cached keys");

                // Try each valid key pair until one works (ordered by version descending - newest first)
                foreach (var keyPair in validKeys.OrderByDescending(k => k.KeyVersion))
                {
                    try
                    {
                        Console.WriteLine($"Trying key version {keyPair.KeyVersion}");

                        // Attempt to decode the ticket using this key pair
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
                            Console.WriteLine($"Successfully decoded ticket with key version {keyPair.KeyVersion}");

                            // Perform basic validation checks
                            var validationChecks = PerformOfflineValidationChecks(
                                ticketNumber, valueCents, issuingStation, issueDateTime, ticketType);

                            if (!validationChecks.IsValid)
                            {
                                Console.WriteLine($"Offline validation checks failed: {validationChecks.ErrorMessage}");
                                return TicketValidationResult.Failed(validationChecks.ErrorMessage, isDefinitive: false);
                            }

                            // If all checks pass, return success
                            return new TicketValidationResult
                            {
                                Success = true,
                                TicketType = ticketType,
                                ValueCents = valueCents,
                                IssuingStation = issuingStation,
                                IssueDateTime = issueDateTime,
                                TicketNumber = ticketNumber.ToString(),
                                InputMethod = "offline_validation",
                                Message = $"离线验证成功 (密钥版本 {keyPair.KeyVersion})"
                            };
                        }
                        else
                        {
                            Console.WriteLine($"Key version {keyPair.KeyVersion} failed to decode ticket");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to decode with key version {keyPair.KeyVersion}: {ex.Message}");
                        // Continue to next key
                    }
                }

                Console.WriteLine("Failed to decode ticket with any cached keys");
                return TicketValidationResult.Failed("Failed to decode ticket with any cached keys", isDefinitive: false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Offline validation error: {ex.Message}");
                return TicketValidationResult.Failed($"离线验证错误: {ex.Message}", isDefinitive: false);
            }
        }

        /// <summary>
        /// Cleans up resources when the form is disposed
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                keyRefreshTimer?.Dispose();
                apiClient?.Dispose();

                // Dispose all cached key pairs
                foreach (var keyPair in cachedKeyPairs)
                {
                    keyPair.Dispose();
                }
                cachedKeyPairs.Clear();

                successSound?.Dispose();
                seniorTicketSound?.Dispose();
                studentTicketSound?.Dispose();
                failSound?.Dispose();
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        // Centers controls horizontally
        public void CenterControlHorizontally(Control c)
        {
            c.Left = (c.Parent.Width - c.Width) / 2;
        }

        private void DisplayScanResult(DisplayType type, string middleText, string bottomText)
        {
            // Reset the hiding timer
            HideUserMessageTimer.Stop();

            // Switch the text
            MiddleTextLabel.Text = middleText;
            BottomTextLabel.Text = bottomText;

            // Switch the icon based on success or fail
            switch (type)
            {
                case DisplayType.RegularTap:
                    UserPromptPictureBox.Image = successImage;
                    successSound.Play();
                    break;
                case DisplayType.SeniorTicket:
                    UserPromptPictureBox.Image = successImage;
                    seniorTicketSound.Play();
                    break;
                case DisplayType.StudentTicket:
                    UserPromptPictureBox.Image = successImage;
                    studentTicketSound.Play();
                    break;
                case DisplayType.Fail:
                    UserPromptPictureBox.Image = failImage;
                    failSound.Play();
                    break;
            }

            // Start the hiding timer
            HideUserMessageTimer.Start();
        }

        // This timer keeps the text box selected
        private void SecurityTimer_Tick(object sender, EventArgs e)
        {
            TicketScanTextBox.Focus();
        }

        // Scan code
        private async void TicketScanTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                // Store the scanned code and clear the text box
                string scannedCode = TicketScanTextBox.Text;
                TicketScanTextBox.Clear();

                // Process the scanned code asynchronously
                await ProcessScannedTicketAsync(scannedCode);
            }
        }

        private SoundPlayer LoadSoundFromResource(Stream resourceStream)
        {
            try
            {
                var memoryStream = new MemoryStream();
                resourceStream.CopyTo(memoryStream);
                memoryStream.Position = 0;
                var player = new SoundPlayer(memoryStream);
                player.LoadAsync();
                return player;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Failed to load sound: {ex.Message}");
                return new SoundPlayer();
            }
        }

        /// <summary>
        /// Displays the result of ticket validation to the user
        /// </summary>
        private void DisplayValidationResult(TicketValidationResult result)
        {
            if (!result.Success)
            {
                DisplayScanResult(DisplayType.Fail, "验证失败", result.ErrorMessage ?? "Validation Failed");
                return;
            }

            // Get faregate direction for display
            string faregateDirection = SimpleConfig.Get("FAREGATE_DIRECTION", "ENTRY").ToUpper();
            string directionText = faregateDirection == "EXIT" ? "出站" : "入站";
            string directionEnglish = faregateDirection == "EXIT" ? "Exit" : "Entry";

            // Determine display type based on ticket type
            DisplayType displayType;
            string middleText = $"{directionText}验证成功";
            string bottomText = $"{directionEnglish} Valid";

            switch (result.TicketType)
            {
                case 1: // Student ticket
                    displayType = DisplayType.StudentTicket;
                    middleText = $"学生票{directionText}成功";
                    bottomText = $"Student {directionEnglish}";
                    break;
                case 2: // Senior ticket
                    displayType = DisplayType.SeniorTicket;
                    middleText = $"长者票{directionText}成功";
                    bottomText = $"Senior {directionEnglish}";
                    break;
                case 3: // Free exit ticket
                    displayType = DisplayType.RegularTap;
                    middleText = "免费出站票";
                    bottomText = "Free Exit";
                    break;
                case 4: // Day pass
                    displayType = DisplayType.RegularTap;
                    middleText = $"一日票{directionText}成功";
                    bottomText = $"Day Pass {directionEnglish}";
                    break;
                default: // Regular ticket (type 0) or others
                    displayType = DisplayType.RegularTap;
                    break;
            }

            // Add value information if available
            if (result.ValueCents > 0)
            {
                decimal valueYuan = result.ValueCents / 100.0m;
                bottomText += $" ¥{valueYuan:F2}";
            }

            DisplayScanResult(displayType, middleText, bottomText);
        }

        /// <summary>
        /// Checks if a ticket is in the local blacklist (already used offline)
        /// File-based implementation replacing SQLite
        /// </summary>
        private async Task<bool> IsTicketBlacklistedAsync(string ticketCode)
        {
            try
            {
                if (!File.Exists(blacklistFilePath))
                    return false;

                var lines = await ReadAllLinesAsync(blacklistFilePath);
                return lines.Any(line => line.StartsWith(ticketCode + "|") || line == ticketCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Failed to check blacklist: {ex.Message}");
                return false; // If we can't check, assume it's not blacklisted
            }
        }

        /// <summary>
        /// Adds a ticket to the local blacklist and manages file size
        /// File-based implementation replacing SQLite
        /// </summary>
        private async Task AddTicketToBlacklistAsync(string ticketCode)
        {
            try
            {
                var lines = new List<string>();

                if (File.Exists(blacklistFilePath))
                {
                    lines.AddRange(await ReadAllLinesAsync(blacklistFilePath));
                }

                // Don't add if already exists
                string entryToAdd = $"{ticketCode}|{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                if (!lines.Any(line => line.StartsWith(ticketCode + "|") || line == ticketCode))
                {
                    lines.Add(entryToAdd);
                }

                // Keep only the most recent 1000 entries
                if (lines.Count > 1000)
                {
                    lines = lines.Skip(lines.Count - 1000).ToList();
                    Console.WriteLine("Cleaned up old blacklist entries");
                }

                await WriteAllLinesAsync(blacklistFilePath, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Failed to add ticket to blacklist: {ex.Message}");
            }
        }

        /// <summary>
        /// Async file reading helper for .NET Framework compatibility
        /// </summary>
        private async Task<string[]> ReadAllLinesAsync(string path)
        {
            return await Task.Run(() => File.ReadAllLines(path));
        }

        /// <summary>
        /// Async file writing helper for .NET Framework compatibility
        /// </summary>
        private async Task WriteAllLinesAsync(string path, IEnumerable<string> lines)
        {
            await Task.Run(() => File.WriteAllLines(path, lines));
        }

        private void HideUserMessageTimer_Tick(object sender, EventArgs e)
        {
            // Reset to default text and image
            HideUserMessageTimer.Stop();
            MiddleTextLabel.Text = DefaultMiddleText;
            BottomTextLabel.Text = DefaultBottomText;
            UserPromptPictureBox.Image = tapCardImage;
        }

        /// <summary>
        /// Removes expired keys from the cache and disposes their resources
        /// </summary>
        private void CleanupExpiredKeys()
        {
            var expiredKeys = cachedKeyPairs.Where(k => !k.IsValid).ToList();

            foreach (var expiredKey in expiredKeys)
            {
                Console.WriteLine($"Removing expired key version {expiredKey.KeyVersion}");
                expiredKey.Dispose();
                cachedKeyPairs.Remove(expiredKey);
            }
        }

        /// <summary>
        /// Performs basic validation checks for offline ticket validation
        /// </summary>
        private (bool IsValid, string ErrorMessage) PerformOfflineValidationChecks(
            long ticketNumber, int valueCents, string issuingStation, DateTime issueDateTime, byte ticketType)
        {
            // Get faregate direction from config
            string faregateDirection = SimpleConfig.Get("FAREGATE_DIRECTION", "ENTRY").ToUpper();
            string currentStation = SimpleConfig.Get("CURRENT_STATION", "FLZ");

            // Check if ticket is expired (example: tickets expire after 24 hours)
            if (issueDateTime.AddHours(24) < DateTime.Now)
            {
                return (false, "车票已过期");
            }

            // Check if ticket type is valid
            if (ticketType > 4 && ticketType != 255) // 0-4 are valid types, 255 is debug
            {
                return (false, "无效车票类型");
            }

            // For day passes (type 4), check if it's for today
            if (ticketType == 4)
            {
                if (issueDateTime.Date != DateTime.Now.Date)
                {
                    return (false, "一日票不适用于今日");
                }
            }

            // Check if ticket has value (except for free tickets)
            if (ticketType != 3 && valueCents <= 0) // Type 3 is free exit ticket
            {
                return (false, "车票无价值");
            }

            // Faregate direction specific validations
            if (faregateDirection == "ENTRY")
            {
                // Entry-specific validations
                if (ticketType == 3) // Free exit ticket
                {
                    return (false, "免费出站票不能用于入站");
                }

                // For regular tickets, check if issuing station matches current station
                if (ticketType >= 0 && ticketType <= 2) // Regular, Student, Senior
                {
                    if (!string.Equals(issuingStation, currentStation, StringComparison.OrdinalIgnoreCase))
                    {
                        return (false, $"车票在{issuingStation}站发行，不能在{currentStation}站入站");
                    }
                }
            }
            else if (faregateDirection == "EXIT")
            {
                // Exit-specific validations
                if (ticketType == 3) // Free exit ticket
                {
                    // Free exit tickets can only be used at their issuing station
                    if (!string.Equals(issuingStation, currentStation, StringComparison.OrdinalIgnoreCase))
                    {
                        return (false, $"免费出站票只能在发行站{issuingStation}使用");
                    }
                }

                // For regular tickets at exit, we would need to check fare validity
                // This is simplified validation - in a real system, you'd need to track
                // where the passenger entered and calculate if they paid enough fare
            }

            return (true, string.Empty);
        }
    }

    /// <summary>
    /// Represents the result of a ticket validation operation
    /// </summary>
    public class TicketValidationResult
    {
        public bool Success { get; set; }
        public byte TicketType { get; set; }
        public int ValueCents { get; set; }
        public string IssuingStation { get; set; } = string.Empty;
        public DateTime IssueDateTime { get; set; }
        public string Message { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public string TicketNumber { get; set; } = string.Empty;
        public string InputMethod { get; set; } = string.Empty;
        
        /// <summary>
        /// Indicates whether this failure is definitive (server rejected) or due to connectivity issues
        /// If true, offline validation should NOT be attempted
        /// </summary>
        public bool IsDefinitiveFailure { get; set; } = false;

        public static TicketValidationResult Failed(string errorMessage, bool isDefinitive = true)
        {
            return new TicketValidationResult
            {
                Success = false,
                ErrorMessage = errorMessage,
                IsDefinitiveFailure = isDefinitive
            };
        }

        /// <summary>
        /// Creates a successful validation result from API response
        /// </summary>
        public static TicketValidationResult FromApiResponse(ValidateTicketResponse response, string direction)
        {
            return new TicketValidationResult
            {
                Success = true,
                TicketType = response.TicketType,
                TicketNumber = response.TicketNumber,
                InputMethod = response.InputMethod,
                Message = $"验证成功 ({direction})"
            };
        }
    }
}