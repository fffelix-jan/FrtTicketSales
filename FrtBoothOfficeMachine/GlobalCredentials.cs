using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrtAfcApiClient;

namespace FrtBoothOfficeMachine
{
    /// <summary>
    /// Global storage for authenticated API credentials and client instance
    /// </summary>
    public static class GlobalCredentials
    {
        public static string Username { get; set; } = string.Empty;
        public static string Password { get; set; } = string.Empty;
        public static FareApiClient ApiClient { get; set; } = null;
        
        /// <summary>
        /// Clears all stored credentials and disposes the API client
        /// </summary>
        public static void Clear()
        {
            Username = null;
            Password = null;
            ApiClient?.Dispose();
            ApiClient = null;

            GC.Collect();
            GC.Collect();

            // Overwrite the Username and Password 10 times with random text
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                Username = new string(Enumerable.Range(0, Username?.Length ?? 0)
                    .Select(_ => (char)rand.Next(32, 127)).ToArray());
                Password = new string(Enumerable.Range(0, Password?.Length ?? 0)
                    .Select(_ => (char)rand.Next(32, 127)).ToArray());
                GC.Collect();
            }

            // Then set the Username and Password to null again
            Username = null;
            Password = null;
            GC.Collect();
            GC.Collect();
        }

        /// <summary>
        /// Checks if valid credentials are currently stored
        /// </summary>
        public static bool IsAuthenticated => !string.IsNullOrEmpty(Username) && ApiClient != null;
    }
}
