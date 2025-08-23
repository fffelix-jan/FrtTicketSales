using FrtAfcApiClient;

namespace FrtTicketVendingMachine
{
    public static class GlobalCredentials
    {
        public static string Username { get; set; } = "";
        public static int UserId { get; set; } = 0;
        public static FareApiClient ApiClient { get; set; } = null;

        public static bool IsAuthenticated => !string.IsNullOrEmpty(Username) && ApiClient != null;

        public static void SetCredentials(string username, int userId, FareApiClient apiClient)
        {
            Username = username;
            UserId = userId;
            ApiClient = apiClient;
        }

        public static void ClearCredentials()
        {
            Username = "";
            UserId = 0;
            ApiClient?.Dispose();
            ApiClient = null;
        }
    }
}