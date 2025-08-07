using FrtAfcApiClient;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FrtAfcClientTestApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine("FRT AFC Station Name Test Application");
            for (int i = 0; i < 50; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
            Console.WriteLine();

            // API endpoint - adjust this to match your running server
            const string apiEndpoint = "https://localhost:7184";

            // Test station codes
            string[] testStationCodes = { "XZC", "JLL", "XMD", "FLZ" };

            var client = new FareApiClient(apiEndpoint);

            Console.WriteLine($"Connected to: {apiEndpoint}");
            Console.WriteLine();

            // Test each station code
            foreach (var stationCode in testStationCodes)
            {
                Console.WriteLine($"Testing station code: {stationCode}");
                for (int i = 0; i < 50; i++)
                {
                    Console.Write("=");
                }
                Console.WriteLine();

                try
                {
                    var stationInfo = await client.GetStationNameAsync(stationCode);

                    Console.WriteLine("Station found:");
                    Console.WriteLine($"   Station Code: {stationInfo.StationCode}");
                    Console.WriteLine($"   English Name: {stationInfo.EnglishName}");
                    Console.WriteLine($"   Chinese Name: {stationInfo.ChineseName}");
                    Console.WriteLine($"   Zone ID: {stationInfo.ZoneId}");
                    Console.WriteLine($"   Is Active: {stationInfo.IsActive}");
                }
                catch (StationNotFoundException ex)
                {
                    Console.WriteLine($"Station not found: {ex.Message}");
                }
                catch (FrtAfcApiException ex)
                {
                    Console.WriteLine($"API Error: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Invalid input: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }

                Console.WriteLine();
            }

            // Interactive test
            Console.WriteLine("Interactive Station Lookup");
            Console.WriteLine("Enter station codes to test (press Enter with empty input to exit):");
            Console.WriteLine();

            while (true)
            {
                Console.Write("Station Code (3 letters): ");
                var input = Console.ReadLine()?.Trim().ToUpper();

                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                try
                {
                    var stationInfo = await client.GetStationNameAsync(input);

                    Console.WriteLine($"SUCCESS: {stationInfo.EnglishName} ({stationInfo.ChineseName})");
                    Console.WriteLine($"   Zone {stationInfo.ZoneId} | Active: {stationInfo.IsActive}");
                }
                catch (StationNotFoundException)
                {
                    Console.WriteLine($"ERROR: Station '{input}' not found in database");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"INVALID INPUT: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }

                Console.WriteLine();
            }

            Console.WriteLine("Goodbye!");
        }
    }
}