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
            Username = string.Empty;
            Password = string.Empty;
            ApiClient?.Dispose();
            ApiClient = null;
        }
        
        /// <summary>
        /// Checks if valid credentials are currently stored
        /// </summary>
        public static bool IsAuthenticated => !string.IsNullOrEmpty(Username) && ApiClient != null;
    }
}
