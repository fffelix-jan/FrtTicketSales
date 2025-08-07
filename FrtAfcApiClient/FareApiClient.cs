using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System.ComponentModel.DataAnnotations;

namespace FrtAfcApiClient
{
    public class FareApiClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private bool _disposed = false;

        public string EndpointUrl { get; private set; }

        public FareApiClient(string endpointUrl) : this(endpointUrl, new HttpClient())
        {
        }

        public FareApiClient(string endpointUrl, HttpClient httpClient)
        {
            if (string.IsNullOrWhiteSpace(endpointUrl))
            {
                throw new ArgumentException("Endpoint URL cannot be null or empty.", nameof(endpointUrl));
            }

            EndpointUrl = endpointUrl.TrimEnd('/');
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            // Set base address if not already set
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri($"{EndpointUrl}/api/v1/afc/");
            }
        }

        /// <summary>
        /// Gets the current server date and time.
        /// </summary>
        /// <returns>Server's current DateTime</returns>
        public async Task<DateTime> GetCurrentDateTimeAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("currentdatetime");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<DateTime>();
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException("Failed to get current datetime from server", ex);
            }
        }

        /// <summary>
        /// Gets station information by station code.
        /// </summary>
        /// <param name="stationCode">3-letter station code</param>
        /// <returns>Station information including English and Chinese names</returns>
        public async Task<StationInfo> GetStationNameAsync(string stationCode)
        {
            if (string.IsNullOrWhiteSpace(stationCode))
            {
                throw new ArgumentException("Station code cannot be null or empty", nameof(stationCode));
            }

            if (stationCode.Length != 3)
            {
                throw new ArgumentException("Station code must be exactly 3 characters", nameof(stationCode));
            }

            try
            {
                var response = await _httpClient.GetAsync($"getstationname?stationCode={stationCode.ToUpper()}");

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new StationNotFoundException($"Station with code '{stationCode.ToUpper()}' not found");
                }

                response.EnsureSuccessStatusCode();

                var stationInfo = await response.Content.ReadFromJsonAsync<StationInfo>();
                return stationInfo ?? throw new FrtAfcApiException("Received null response from server");
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException($"Failed to get station information for '{stationCode}'", ex);
            }
        }

        /// <summary>
        /// Gets fare information between two stations.
        /// </summary>
        /// <param name="fromStation">Origin station code (3 letters)</param>
        /// <param name="toStation">Destination station code (3 letters)</param>
        /// <returns>Fare information including cost and station details</returns>
        public async Task<FareInfo> GetFareAsync(string fromStation, string toStation)
        {
            ValidateStationCode(fromStation, nameof(fromStation));
            ValidateStationCode(toStation, nameof(toStation));

            try
            {
                var response = await _httpClient.GetAsync($"fare?fromStation={fromStation.ToUpper()}&toStation={toStation.ToUpper()}");

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new FareNotFoundException($"Fare lookup failed: {error}");
                }

                response.EnsureSuccessStatusCode();

                var fareInfo = await response.Content.ReadFromJsonAsync<FareInfo>();
                return fareInfo ?? throw new FrtAfcApiException("Received null response from server");
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException($"Failed to get fare from '{fromStation}' to '{toStation}'", ex);
            }
        }

        /// <summary>
        /// Issues a new ticket.
        /// </summary>
        /// <param name="request">Ticket request details</param>
        /// <returns>Issued ticket data including ticket string for QR code</returns>
        public async Task<TicketData> IssueTicketAsync(TicketRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ValidateTicketRequest(request);

            try
            {
                var response = await _httpClient.PostAsJsonAsync("issueticket", request);
                response.EnsureSuccessStatusCode();

                var ticketData = await response.Content.ReadFromJsonAsync<TicketData>();
                return ticketData ?? throw new FrtAfcApiException("Received null response from server");
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException("Failed to issue ticket", ex);
            }
        }

        /// <summary>
        /// Issues a debug ticket (only available when server is in debug mode).
        /// </summary>
        /// <returns>Debug ticket data</returns>
        public async Task<TicketData> IssueDebugTicketAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("issuedebugticket");

                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    throw new DebugModeDisabledException("Debug mode is not enabled on the server");
                }

                response.EnsureSuccessStatusCode();

                var ticketData = await response.Content.ReadFromJsonAsync<TicketData>();
                return ticketData ?? throw new FrtAfcApiException("Received null response from server");
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException("Failed to issue debug ticket", ex);
            }
        }

        /// <summary>
        /// Issues a ticket with simplified parameters.
        /// </summary>
        /// <param name="valueCents">Ticket value in cents</param>
        /// <param name="issuingStation">3-letter station code where ticket is issued</param>
        /// <param name="ticketType">Ticket type (0=Full Fare, 1=Student, 2=Senior, etc.)</param>
        /// <returns>Issued ticket data</returns>
        public async Task<TicketData> IssueTicketAsync(int valueCents, string issuingStation, int ticketType)
        {
            var request = new TicketRequest
            {
                ValueCents = valueCents,
                IssuingStation = issuingStation,
                TicketType = ticketType
            };

            return await IssueTicketAsync(request);
        }

        private void ValidateStationCode(string stationCode, string paramName)
        {
            if (string.IsNullOrWhiteSpace(stationCode))
            {
                throw new ArgumentException("Station code cannot be null or empty", paramName);
            }

            if (stationCode.Length != 3)
            {
                throw new ArgumentException("Station code must be exactly 3 characters", paramName);
            }

            if (!stationCode.All(char.IsLetter))
            {
                throw new ArgumentException("Station code must contain only letters", paramName);
            }
        }

        private void ValidateTicketRequest(TicketRequest request)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(request);

            if (!Validator.TryValidateObject(request, context, validationResults, true))
            {
                var errors = string.Join("; ", validationResults.Select(v => v.ErrorMessage));
                throw new ArgumentException($"Invalid ticket request: {errors}");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _httpClient?.Dispose();
                _disposed = true;
            }
        }
    }

    // Data Transfer Objects (DTOs) - matching the server-side structs
    public class StationInfo
    {
        public string StationCode { get; set; } = string.Empty;
        public string EnglishName { get; set; } = string.Empty;
        public string ChineseName { get; set; } = string.Empty;
        public int ZoneId { get; set; }
        public bool IsActive { get; set; }
    }

    public class FareInfo
    {
        public string FromStation { get; set; } = string.Empty;
        public string ToStation { get; set; } = string.Empty;
        public int FromZone { get; set; }
        public int ToZone { get; set; }
        public int FareCents { get; set; }
        public string FromStationName { get; set; } = string.Empty;
        public string ToStationName { get; set; } = string.Empty;
    }

    public class TicketData
    {
        public string TicketString { get; set; } = string.Empty;
        public string TicketNumber { get; set; } = string.Empty;
        public DateTime IssueTime { get; set; }
    }

    public class TicketRequest
    {
        [Required(ErrorMessage = "Issuing station is required")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Station code must be 3 characters")]
        [RegularExpression(@"^[A-Z]+$", ErrorMessage = "Station code must be uppercase letters")]
        public string IssuingStation { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ticket value is required")]
        [Range(1, 100000, ErrorMessage = "Value must be between 1 and 100000 cents")]
        public int ValueCents { get; set; }

        [Required(ErrorMessage = "Ticket type is required")]
        [Range(0, 255, ErrorMessage = "Ticket type must be between 0 and 255")]
        public int TicketType { get; set; }
    }

    // Custom Exception Classes
    public class FrtAfcApiException : Exception
    {
        public FrtAfcApiException(string message) : base(message) { }
        public FrtAfcApiException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class StationNotFoundException : FrtAfcApiException
    {
        public StationNotFoundException(string message) : base(message) { }
    }

    public class FareNotFoundException : FrtAfcApiException
    {
        public FareNotFoundException(string message) : base(message) { }
    }

    public class DebugModeDisabledException : FrtAfcApiException
    {
        public DebugModeDisabledException(string message) : base(message) { }
    }
}