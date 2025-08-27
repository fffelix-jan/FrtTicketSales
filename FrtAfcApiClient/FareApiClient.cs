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
        /// Sets Basic Authentication credentials for API access.
        /// </summary>
        /// <param name="username">API username</param>
        /// <param name="password">API password</param>
        public void SetBasicAuthentication(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Username and password cannot be null or empty");
            }

            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
            _httpClient.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
        }

        /// <summary>
        /// Clears authentication credentials from the client.
        /// Call this method when the user logs out.
        /// </summary>
        public void ClearCredentials()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        /// <summary>
        /// Tests if the current credentials are valid by calling a lightweight authenticated endpoint.
        /// Perfect for login validation on POS machines.
        /// </summary>
        /// <returns>True if credentials are valid, false otherwise</returns>
        public async Task<bool> TestCredentialsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("currentdatetime");
                
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return false;
                }
                
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets all stations from the system.
        /// </summary>
        /// <returns>List of all station information including English and Chinese names</returns>
        public async Task<List<StationInfo>> GetAllStationsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("stations");
                response.EnsureSuccessStatusCode();

                var stations = await response.Content.ReadFromJsonAsync<List<StationInfo>>();
                return stations ?? new List<StationInfo>();
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException("Failed to get stations list from server", ex);
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

                // The server returns a complex object, not a simple DateTime
                var authResponse = await response.Content.ReadFromJsonAsync<AuthenticatedDateTimeResponse>();
                if (authResponse != null)
                {
                    return authResponse.CurrentDateTime;
                }
                
                throw new FrtAfcApiException("Server returned null response");
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

                if (stationInfo.Equals(default(StationInfo)))
                {
                    throw new FrtAfcApiException("Received null response from server");
                }
                return stationInfo;
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
                if (fareInfo.Equals(default(FareInfo)))
                {
                    throw new FrtAfcApiException("Received null response from server");
                }
                return fareInfo;
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException($"Failed to get fare from '{fromStation}' to '{toStation}'", ex);
            }
        }

        /// <summary>
        /// Issues a ticket based on ticket type. Routes to appropriate specific endpoint.
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

            // Route to specific endpoint based on ticket type using traditional switch
            switch (request.TicketType)
            {
                case 0:
                    return await IssueTicketInternalAsync("issueticket/full", request);
                case 1:
                    return await IssueTicketInternalAsync("issueticket/student", request);
                case 2:
                    return await IssueTicketInternalAsync("issueticket/senior", request);
                case 3:
                    return await IssueTicketInternalAsync("issueticket/free", request);
                case 4:
                    return await IssueTicketInternalAsync("issueticket/daypass", request);
                default:
                    throw new ArgumentException($"Invalid ticket type: {request.TicketType}");
            }
        }

        /// <summary>
        /// Issues a ticket with simplified parameters.
        /// </summary>
        /// <param name="valueCents">Ticket value in cents</param>
        /// <param name="issuingStation">3-letter station code where ticket is issued</param>
        /// <param name="ticketType">Ticket type (0=Full Fare, 1=Student, 2=Senior, 3=Free, 4=Day Pass)</param>
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

        /// <summary>
        /// Issues a full fare ticket.
        /// </summary>
        /// <param name="request">Ticket request details</param>
        /// <returns>Issued ticket data including ticket string for QR code</returns>
        public async Task<TicketData> IssueFullFareTicketAsync(TicketRequest request)
        {
            return await IssueTicketInternalAsync("issueticket/full", request);
        }

        /// <summary>
        /// Issues a student ticket.
        /// </summary>
        /// <param name="request">Ticket request details</param>
        /// <returns>Issued ticket data including ticket string for QR code</returns>
        public async Task<TicketData> IssueStudentTicketAsync(TicketRequest request)
        {
            return await IssueTicketInternalAsync("issueticket/student", request);
        }

        /// <summary>
        /// Issues a senior ticket.
        /// </summary>
        /// <param name="request">Ticket request details</param>
        /// <returns>Issued ticket data including ticket string for QR code</returns>
        public async Task<TicketData> IssueSeniorTicketAsync(TicketRequest request)
        {
            return await IssueTicketInternalAsync("issueticket/senior", request);
        }

        /// <summary>
        /// Issues a free entry ticket.
        /// </summary>
        /// <param name="request">Ticket request details (ValueCents will be ignored, set to 0 by server)</param>
        /// <returns>Issued ticket data including ticket string for QR code</returns>
        public async Task<TicketData> IssueFreeEntryTicketAsync(TicketRequest request)
        {
            return await IssueTicketInternalAsync("issueticket/free", request);
        }

        /// <summary>
        /// Issues a day pass ticket.
        /// </summary>
        /// <param name="request">Ticket request details</param>
        /// <returns>Issued ticket data including ticket string for QR code</returns>
        public async Task<TicketData> IssueDayPassTicketAsync(TicketRequest request)
        {
            return await IssueTicketInternalAsync("issueticket/daypass", request);
        }

        /// <summary>
        /// Reissues a damaged ticket.
        /// </summary>
        /// <param name="request">Reissue ticket request details</param>
        /// <returns>Reissue response with original and replacement ticket information</returns>
        public async Task<ReissueTicketResponse> ReissueTicketAsync(ReissueTicketRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ValidateReissueTicketRequest(request);

            try
            {
                var response = await _httpClient.PostAsJsonAsync("reissueticket", request);
                response.EnsureSuccessStatusCode();

                var reissueResponse = await response.Content.ReadFromJsonAsync<ReissueTicketResponse>();
                if (reissueResponse == null)
                {
                    throw new FrtAfcApiException("Received null response from server");
                }
                return reissueResponse;
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException("Failed to reissue ticket", ex);
            }
        }

        /// <summary>
        /// Reprints a damaged ticket by generating a new one and invalidating the original.
        /// The server will preserve the original ticket type and invoice status.
        /// </summary>
        /// <param name="originalTicketNumber">The ticket number of the damaged ticket</param>
        /// <param name="issuingStation">Station where the new ticket is being issued</param>
        /// <param name="valueCents">Value for the new ticket (optional - uses original if not specified)</param>
        /// <returns>Reissue response with original and replacement ticket information</returns>
        public async Task<ReissueTicketResponse> ReprintTicketAsync(string originalTicketNumber, string issuingStation, int? valueCents = null)
        {
            if (string.IsNullOrWhiteSpace(originalTicketNumber))
            {
                throw new ArgumentException("Original ticket number cannot be null or empty", nameof(originalTicketNumber));
            }

            if (string.IsNullOrWhiteSpace(issuingStation))
            {
                throw new ArgumentException("Issuing station cannot be null or empty", nameof(issuingStation));
            }

            ValidateStationCode(issuingStation, nameof(issuingStation));

            var request = new ReissueTicketRequest
            {
                OriginalTicketNumber = originalTicketNumber,
                IssuingStation = issuingStation.ToUpper(),
                ValueCents = valueCents ?? 0 // Use 0 to indicate server should use original value
            };

            return await ReissueTicketAsync(request);
        }

        /// <summary>
        /// Reprints a damaged ticket using the same details as the original.
        /// The server will preserve the original ticket type and invoice status.
        /// </summary>
        /// <param name="originalTicketNumber">The ticket number of the damaged ticket</param>
        /// <param name="issuingStation">Station where the new ticket is being issued</param>
        /// <returns>Reissue response with original and replacement ticket information</returns>
        public async Task<ReissueTicketResponse> ReprintTicketAsync(string originalTicketNumber, string issuingStation)
        {
            return await ReprintTicketAsync(originalTicketNumber, issuingStation, null);
        }

        /// <summary>
        /// Gets information about a specific ticket using ticket number or QR code.
        /// </summary>
        /// <param name="ticketInput">Ticket number or QR code string</param>
        /// <returns>Complete ticket information including all details</returns>
        public async Task<TicketInfo> GetTicketInfoAsync(string ticketInput)
        {
            if (string.IsNullOrWhiteSpace(ticketInput))
            {
                throw new ArgumentException("Ticket input cannot be null or empty", nameof(ticketInput));
            }

            var requestObj = new GetTicketInfoRequest { TicketInput = ticketInput.Trim() };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("getticketinfo", requestObj);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new TicketNotFoundException($"Ticket not found: {error}");
                }

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var error = await response.Content.ReadAsStringAsync();

                    // Check for specific error types
                    if (error.Contains("Invalid ticket number format"))
                    {
                        throw new TicketValidationException("Invalid ticket number format. Please enter a valid numeric ticket number.");
                    }
                    else if (error.Contains("Invalid QR code") || error.Contains("unable to decode ticket"))
                    {
                        throw new TicketValidationException("Invalid QR code. The scanned QR code is not a valid ticket code.");
                    }
                    else if (error.Contains("QR code decoding failed"))
                    {
                        throw new TicketValidationException("QR code decoding failed. The QR code may be damaged or corrupted.");
                    }
                    else
                    {
                        throw new FrtAfcApiException($"Ticket lookup failed: {error}");
                    }
                }

                response.EnsureSuccessStatusCode();

                var ticketInfo = await response.Content.ReadFromJsonAsync<TicketInfo>();
                if (ticketInfo == null)
                {
                    throw new FrtAfcApiException("Received null response from server");
                }
                return ticketInfo;
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException("Failed to get ticket information", ex);
            }
        }

        /// <summary>
        /// Validates a ticket at an entry faregate using ticket number or QR code.
        /// </summary>
        /// <param name="ticketInput">Ticket number or QR code string</param>
        /// <param name="validatingStation">Station code where validation occurs</param>
        /// <returns>Validation response</returns>
        public async Task<ValidateTicketResponse> ValidateTicketAtEntryAsync(string ticketInput, string validatingStation)
        {
            if (string.IsNullOrWhiteSpace(ticketInput))
            {
                throw new ArgumentException("Ticket input cannot be null or empty", nameof(ticketInput));
            }

            if (string.IsNullOrWhiteSpace(validatingStation))
            {
                throw new ArgumentException("Validating station cannot be null or empty", nameof(validatingStation));
            }

            ValidateStationCode(validatingStation, nameof(validatingStation));

            var request = new ValidateTicketRequest
            {
                TicketInput = ticketInput.Trim(),
                ValidatingStation = validatingStation.ToUpper()
            };

            return await ValidateTicketAtEntryAsync(request);
        }

        /// <summary>
        /// Validates a ticket at an entry faregate.
        /// </summary>
        /// <param name="request">Validation request details</param>
        /// <returns>Validation response</returns>
        public async Task<ValidateTicketResponse> ValidateTicketAtEntryAsync(ValidateTicketRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ValidateTicketValidationRequest(request);

            try
            {
                var response = await _httpClient.PostAsJsonAsync("validateticketatentry", request);

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    
                    // Check for specific error types
                    if (error.Contains("Invalid ticket number format"))
                    {
                        throw new TicketValidationException("Invalid ticket number format. Please enter a valid numeric ticket number.");
                    }
                    else if (error.Contains("Invalid QR code") || error.Contains("unable to decode ticket"))
                    {
                        throw new TicketValidationException("Invalid QR code. The scanned QR code is not a valid ticket code.");
                    }
                    else if (error.Contains("QR code decoding failed"))
                    {
                        throw new TicketValidationException("QR code decoding failed. The QR code may be damaged or corrupted.");
                    }
                    else
                    {
                        throw new TicketValidationException($"Entry ticket validation failed: {error}");
                    }
                }

                response.EnsureSuccessStatusCode();

                var validationResponse = await response.Content.ReadFromJsonAsync<ValidateTicketResponse>();
                if (validationResponse == null)
                {
                    throw new FrtAfcApiException("Received null response from server");
                }
                return validationResponse;
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException("Failed to validate ticket at entry", ex);
            }
        }

        /// <summary>
        /// Validates a ticket at an exit faregate using ticket number or QR code.
        /// </summary>
        /// <param name="ticketInput">Ticket number or QR code string</param>
        /// <param name="validatingStation">Station code where validation occurs</param>
        /// <returns>Validation response</returns>
        public async Task<ValidateTicketResponse> ValidateTicketAtExitAsync(string ticketInput, string validatingStation)
        {
            if (string.IsNullOrWhiteSpace(ticketInput))
            {
                throw new ArgumentException("Ticket input cannot be null or empty", nameof(ticketInput));
            }

            if (string.IsNullOrWhiteSpace(validatingStation))
            {
                throw new ArgumentException("Validating station cannot be null or empty", nameof(validatingStation));
            }

            ValidateStationCode(validatingStation, nameof(validatingStation));

            var request = new ValidateTicketRequest
            {
                TicketInput = ticketInput.Trim(),
                ValidatingStation = validatingStation.ToUpper()
            };

            return await ValidateTicketAtExitAsync(request);
        }

        /// <summary>
        /// Validates a ticket at an exit faregate.
        /// </summary>
        /// <param name="request">Validation request details</param>
        /// <returns>Validation response</returns>
        public async Task<ValidateTicketResponse> ValidateTicketAtExitAsync(ValidateTicketRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ValidateTicketValidationRequest(request);

            try
            {
                var response = await _httpClient.PostAsJsonAsync("validateticketatexit", request);

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    
                    // Check for specific error types
                    if (error.Contains("Invalid ticket number format"))
                    {
                        throw new TicketValidationException("Invalid ticket number format. Please enter a valid numeric ticket number.");
                    }
                    else if (error.Contains("Invalid QR code") || error.Contains("unable to decode ticket"))
                    {
                        throw new TicketValidationException("Invalid QR code. The scanned QR code is not a valid ticket code.");
                    }
                    else if (error.Contains("QR code decoding failed"))
                    {
                        throw new TicketValidationException("QR code decoding failed. The QR code may be damaged or corrupted.");
                    }
                    else
                    {
                        throw new TicketValidationException($"Exit ticket validation failed: {error}");
                    }
                }

                response.EnsureSuccessStatusCode();

                var validationResponse = await response.Content.ReadFromJsonAsync<ValidateTicketResponse>();
                if (validationResponse == null)
                {
                    throw new FrtAfcApiException("Received null response from server");
                }
                return validationResponse;
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException("Failed to validate ticket at exit", ex);
            }
        }

        /// <summary>
        /// Changes the current user's password.
        /// </summary>
        /// <param name="request">Password change request</param>
        /// <returns>Success response</returns>
        public async Task<PasswordChangeResponse> ChangePasswordAsync(ChangePasswordRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ValidatePasswordChangeRequest(request);

            try
            {
                var response = await _httpClient.PostAsJsonAsync("changepassword", request);

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new PasswordChangeException($"Password change failed: {error}");
                }

                response.EnsureSuccessStatusCode();

                var changeResponse = await response.Content.ReadFromJsonAsync<PasswordChangeResponse>();
                if (changeResponse == null)
                {
                    throw new FrtAfcApiException("Received null response from server");
                }
                return changeResponse;
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException("Failed to change password", ex);
            }
        }

        /// <summary>
        /// Gets current user's permission information.
        /// </summary>
        /// <returns>User permission details</returns>
        public async Task<UserPermissionInfo> GetCurrentUserPermissionsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("permissions");
                response.EnsureSuccessStatusCode();

                var permissionInfo = await response.Content.ReadFromJsonAsync<UserPermissionInfo>();
                if (permissionInfo == null)
                {
                    throw new FrtAfcApiException("Received null response from server");
                }
                return permissionInfo;
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException("Failed to get user permissions", ex);
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
                if (ticketData.Equals(default(TicketData)))
                {
                    throw new FrtAfcApiException("Received null response from server");
                }
                return ticketData;
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException("Failed to issue debug ticket", ex);
            }
        }

        /// <summary>
        /// Gets the current day pass price from the server.
        /// </summary>
        /// <returns>Day pass price information</returns>
        public async Task<DayPassPriceInfo> GetDayPassPriceAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("daypassprice");
                response.EnsureSuccessStatusCode();

                var priceInfo = await response.Content.ReadFromJsonAsync<DayPassPriceInfo>();
                if (priceInfo == null)
                {
                    throw new FrtAfcApiException("Received null response from server");
                }
                return priceInfo;
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException("Failed to get day pass price from server", ex);
            }
        }

        /// <summary>
        /// Updates the day pass price (requires system admin permissions).
        /// </summary>
        /// <param name="priceCents">New day pass price in cents</param>
        /// <returns>Update confirmation</returns>
        public async Task<UpdateDayPassPriceResponse> UpdateDayPassPriceAsync(int priceCents)
        {
            var request = new UpdateDayPassPriceRequest { PriceCents = priceCents };
            return await UpdateDayPassPriceAsync(request);
        }

        /// <summary>
        /// Updates the day pass price (requires system admin permissions).
        /// </summary>
        /// <param name="request">Update request with new price</param>
        /// <returns>Update confirmation</returns>
        public async Task<UpdateDayPassPriceResponse> UpdateDayPassPriceAsync(UpdateDayPassPriceRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ValidateUpdateDayPassPriceRequest(request);

            try
            {
                var response = await _httpClient.PostAsJsonAsync("daypassprice", request);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new FrtAfcApiException("Day pass price setting not found on server");
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    throw new FrtAfcApiException("Insufficient permissions to update day pass price");
                }

                response.EnsureSuccessStatusCode();

                var updateResponse = await response.Content.ReadFromJsonAsync<UpdateDayPassPriceResponse>();
                if (updateResponse == null)
                {
                    throw new FrtAfcApiException("Received null response from server");
                }
                return updateResponse;
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException("Failed to update day pass price", ex);
            }
        }

        /// <summary>
        /// Issues a day pass ticket using the current day pass price from the server.
        /// </summary>
        /// <param name="issuingStation">3-letter station code where ticket is issued</param>
        /// <returns>Issued ticket data</returns>
        public async Task<TicketData> IssueDayPassTicketAsync(string issuingStation)
        {
            if (string.IsNullOrWhiteSpace(issuingStation))
            {
                throw new ArgumentException("Issuing station cannot be null or empty", nameof(issuingStation));
            }

            ValidateStationCode(issuingStation, nameof(issuingStation));

            var request = new DayPassTicketRequest { IssuingStation = issuingStation.ToUpper() };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("issueticket/daypass", request);
                response.EnsureSuccessStatusCode();

                var ticketData = await response.Content.ReadFromJsonAsync<TicketData>();
                if (ticketData.Equals(default(TicketData)))
                {
                    throw new FrtAfcApiException("Received null response from server");
                }
                return ticketData;
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException("Failed to issue day pass ticket", ex);
            }
        }

        /// <summary>
        /// Issues an invoice for a ticket using ticket number or QR code.
        /// </summary>
        /// <param name="ticketInput">Ticket number or QR code string</param>
        /// <returns>Invoice issuance response</returns>
        public async Task<IssueInvoiceResponse> IssueInvoiceAsync(string ticketInput)
        {
            if (string.IsNullOrWhiteSpace(ticketInput))
            {
                throw new ArgumentException("Ticket input cannot be null or empty", nameof(ticketInput));
            }

            var request = new IssueInvoiceRequest { TicketInput = ticketInput.Trim() };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("issueinvoice", request);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new TicketNotFoundException($"Ticket not found: {error}");
                }

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new InvoiceException($"Invoice issuance failed: {error}");
                }

                response.EnsureSuccessStatusCode();

                var invoiceResponse = await response.Content.ReadFromJsonAsync<IssueInvoiceResponse>();
                if (invoiceResponse == null)
                {
                    throw new FrtAfcApiException("Received null response from server");
                }
                return invoiceResponse;
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException("Failed to issue invoice", ex);
            }
        }

        /// <summary>
        /// Refunds a ticket using ticket number or QR code.
        /// Only tickets in 'Paid' state (1) that are not invoiced can be refunded.
        /// </summary>
        /// <param name="ticketInput">Ticket number or QR code string</param>
        /// <returns>Refund response</returns>
        public async Task<RefundTicketResponse> RefundTicketAsync(string ticketInput)
        {
            if (string.IsNullOrWhiteSpace(ticketInput))
            {
                throw new ArgumentException("Ticket input cannot be null or empty", nameof(ticketInput));
            }

            var request = new RefundTicketRequest { TicketInput = ticketInput.Trim() };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("refundticket", request);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new TicketNotFoundException($"Ticket not found: {error}");
                }

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new RefundException($"Ticket refund failed: {error}");
                }

                response.EnsureSuccessStatusCode();

                var refundResponse = await response.Content.ReadFromJsonAsync<RefundTicketResponse>();
                if (refundResponse == null)
                {
                    throw new FrtAfcApiException("Received null response from server");
                }
                return refundResponse;
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException("Failed to refund ticket", ex);
            }
        }

        /// <summary>
        /// Gets the current day's signing keys (3 AM to 3 AM next day, with 12 AM-3 AM logic).
        /// </summary>
        /// <returns>List of signing key info for the current day window</returns>
        public async Task<List<SigningKeyInfo>> GetCurrentDaySigningKeysAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("currentdaykeys");
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new FrtAfcApiException($"No signing keys found: {error}");
                }
                response.EnsureSuccessStatusCode();

                var keyResponse = await response.Content.ReadFromJsonAsync<SigningKeysResponse>();
                if (keyResponse == null || keyResponse.SigningKeys == null)
                {
                    throw new FrtAfcApiException("Received null signing keys info from server");
                }
                return keyResponse.SigningKeys;
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException("Failed to get current day's signing keys", ex);
            }
        }

        public class SigningKeyInfo
        {
            public int KeyVersion { get; set; }
            public DateTime KeyCreated { get; set; }
            public string PublicKey { get; set; } = string.Empty;
            public string XorKey { get; set; } = string.Empty; // base64
        }

        public class SigningKeysResponse
        {
            public List<SigningKeyInfo> SigningKeys { get; set; } = new List<SigningKeyInfo>();
        }

        private void ValidateUpdateDayPassPriceRequest(UpdateDayPassPriceRequest request)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(request);

            if (!Validator.TryValidateObject(request, context, validationResults, true))
            {
                var errors = string.Join("; ", validationResults.Select(v => v.ErrorMessage));
                throw new ArgumentException($"Invalid day pass price update request: {errors}");
            }
        }

        private async Task<TicketData> IssueTicketInternalAsync(string endpoint, TicketRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ValidateTicketRequest(request);

            try
            {
                var response = await _httpClient.PostAsJsonAsync(endpoint, request);
                response.EnsureSuccessStatusCode();

                var ticketData = await response.Content.ReadFromJsonAsync<TicketData>();
                if (ticketData.Equals(default(TicketData)))
                {
                    throw new FrtAfcApiException("Received null response from server");
                }
                return ticketData;
            }
            catch (HttpRequestException ex)
            {
                throw new FrtAfcApiException($"Failed to issue ticket via {endpoint}", ex);
            }
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

        private void ValidateReissueTicketRequest(ReissueTicketRequest request)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(request);

            if (!Validator.TryValidateObject(request, context, validationResults, true))
            {
                var errors = string.Join("; ", validationResults.Select(v => v.ErrorMessage));
                throw new ArgumentException($"Invalid reissue ticket request: {errors}");
            }
        }

        private void ValidateTicketValidationRequest(ValidateTicketRequest request)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(request);

            if (!Validator.TryValidateObject(request, context, validationResults, true))
            {
                var errors = string.Join("; ", validationResults.Select(v => v.ErrorMessage));
                throw new ArgumentException($"Invalid ticket validation request: {errors}");
            }
        }

        private void ValidatePasswordChangeRequest(ChangePasswordRequest request)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(request);

            if (!Validator.TryValidateObject(request, context, validationResults, true))
            {
                var errors = string.Join("; ", validationResults.Select(v => v.ErrorMessage));
                throw new ArgumentException($"Invalid password change request: {errors}");
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
                ClearCredentials();
                _httpClient?.Dispose();
                _disposed = true;
            }
        }
    }

    // Data Transfer Objects (DTOs) - matching the server-side structs
    public struct StationInfo
    {
        public string StationCode { get; set; }
        public string EnglishName { get; set; }
        public string ChineseName { get; set; }
        public int ZoneId { get; set; }
        public bool IsActive { get; set; }
    }

    public struct FareInfo
    {
        public string FromStation { get; set; }
        public string ToStation { get; set; }
        public int FromZone { get; set; }
        public int ToZone { get; set; }
        public int FareCents { get; set; }
        public string FromStationName { get; set; }
        public string ToStationName { get; set; }
    }

    public struct TicketData
    {
        public string TicketString { get; set; }
        public string TicketNumber { get; set; }
        public DateTime IssueTime { get; set; }
    }

    public class TicketRequest
    {
        [Required(ErrorMessage = "Issuing station is required")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Station code must be 3 characters")]
        [RegularExpression(@"^[A-Z]+$", ErrorMessage = "Station code must be uppercase letters")]
        public string IssuingStation { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ticket value is required")]
        [Range(0, 100000, ErrorMessage = "Value must be between 0 and 100000 cents")]
        public int ValueCents { get; set; }

        [Required(ErrorMessage = "Ticket type is required")]
        [Range(0, 255, ErrorMessage = "Ticket type must be between 0 and 255")]
        public int TicketType { get; set; }
    }

    public class ReissueTicketRequest : TicketRequest
    {
        [Required(ErrorMessage = "Original ticket number is required")]
        public string OriginalTicketNumber { get; set; }
    }

    public class ValidateTicketRequest
    {
        [Required(ErrorMessage = "Ticket input is required")]
        public string TicketInput { get; set; } = string.Empty; // Changed from TicketNumber to TicketInput

        [StringLength(3, MinimumLength = 3, ErrorMessage = "Station code must be 3 characters")]
        [RegularExpression(@"^[A-Z]+$", ErrorMessage = "Station code must be uppercase letters")]
        public string ValidatingStation { get; set; } = string.Empty;
    }

    public class ChangePasswordRequest
    {
        [Required(ErrorMessage = "Current password is required")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [MinLength(8, ErrorMessage = "New password must be at least 8 characters")]
        public string NewPassword { get; set; }
    }

    // Response DTOs
    public class ReissueTicketResponse
    {
        public string OriginalTicket { get; set; } = string.Empty;
        public byte OriginalTicketState { get; set; }
        public byte OriginalTicketType { get; set; }
        public string OriginalIssuingStation { get; set; } = string.Empty;
        public int OriginalValueCents { get; set; } // Add this line
        public bool OriginalIsInvoiced { get; set; }
        public TicketData ReplacementTicket { get; set; }
        public byte ReplacementTicketState { get; set; }
        public byte ReplacementTicketType { get; set; }
        public int ReplacementValueCents { get; set; } // Add this line
        public bool ReplacementIsInvoiced { get; set; }
        public string Message { get; set; } = string.Empty;
        public string ReissuedBy { get; set; } = string.Empty;
        public DateTime ReissueTime { get; set; }
    }

    public class ValidateTicketResponse
    {
        public string TicketNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime ValidationTime { get; set; }
        public byte TicketType { get; set; }
        public string IssuingStation { get; set; } = string.Empty;
        public string ValidatingStation { get; set; } = string.Empty;
        public byte NewTicketState { get; set; }
        public string InputMethod { get; set; } = string.Empty; // "qr_code" or "ticket_number"
    }

    public class PasswordChangeResponse
    {
        public string Message { get; set; } = string.Empty;
    }

    public class UserPermissionInfo
    {
        public string Username { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int PermissionValue { get; set; }
        public List<string> Permissions { get; set; } = new List<string>();
        public bool CanViewStations { get; set; }
        public bool CanViewFares { get; set; }
        public bool CanIssueFullFare { get; set; }
        public bool CanIssueStudent { get; set; }
        public bool CanIssueSenior { get; set; }
        public bool CanIssueFree { get; set; }
        public bool CanIssueDayPass { get; set; }
        public bool CanReissue { get; set; }
        public bool CanViewTickets { get; set; }
        public bool CanValidateTickets { get; set; }
        public bool CanChangePassword { get; set; }
        public bool CanIssueInvoices { get; set; }
        public bool CanRefundTickets { get; set; }
        public bool IsSystemAdmin { get; set; }
    }

    public class AuthenticatedDateTimeResponse
    {
        public DateTime CurrentDateTime { get; set; }
        public string ServerTimeZone { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public int UserId { get; set; }
        public bool Authenticated { get; set; }
    }

    // Day Pass Price DTOs
    public class DayPassPriceInfo
    {
        public int DayPassPriceCents { get; set; }
        public decimal DayPassPriceYuan { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class UpdateDayPassPriceRequest
    {
        [Required(ErrorMessage = "Price in cents is required")]
        [Range(0, 100000, ErrorMessage = "Price must be between 0 and 100000 cents")]
        public int PriceCents { get; set; }
    }

    public class UpdateDayPassPriceResponse
    {
        public string Message { get; set; } = string.Empty;
        public int NewPriceCents { get; set; }
        public decimal NewPriceYuan { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; }
    }

    public class DayPassTicketRequest
    {
        [Required(ErrorMessage = "Issuing station is required")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Station code must be 3 characters")]
        [RegularExpression(@"^[A-Z]+$", ErrorMessage = "Station code must be uppercase letters")]
        public string IssuingStation { get; set; } = string.Empty;
    }

    public class IssueInvoiceRequest
    {
        [Required(ErrorMessage = "Ticket input is required")]
        public string TicketInput { get; set; } = string.Empty;
    }

    public class IssueInvoiceResponse
    {
        public string TicketNumber { get; set; } = string.Empty;
        public byte TicketType { get; set; }
        public byte TicketState { get; set; }
        public int ValueCents { get; set; }
        public decimal ValueYuan { get; set; }
        public string IssuingStation { get; set; } = string.Empty;
        public DateTime IssueDateTime { get; set; }
        public string InvoicedBy { get; set; } = string.Empty;
        public DateTime InvoiceTime { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    // Refund DTOs
    public class RefundTicketRequest
    {
        [Required(ErrorMessage = "Ticket input is required")]
        public string TicketInput { get; set; } = string.Empty;
    }

    public class RefundTicketResponse
    {
        public string TicketNumber { get; set; } = string.Empty;
        public byte TicketType { get; set; }
        public byte OriginalTicketState { get; set; }
        public byte NewTicketState { get; set; }
        public int ValueCents { get; set; }
        public decimal ValueYuan { get; set; }
        public string IssuingStation { get; set; } = string.Empty;
        public DateTime IssueDateTime { get; set; }
        public string RefundedBy { get; set; } = string.Empty;
        public DateTime RefundTime { get; set; }
        public string InputMethod { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    // Ticket Info DTOs
    public class GetTicketInfoRequest
    {
        [Required(ErrorMessage = "Ticket input is required")]
        public string TicketInput { get; set; } = string.Empty;
    }

    public class TicketInfo
    {
        public long TicketNumber { get; set; }
        public int ValueCents { get; set; }
        public decimal ValueYuan { get; set; }
        public string IssuingStation { get; set; } = string.Empty;
        public DateTime IssueDateTime { get; set; }
        public byte TicketType { get; set; }
        public byte TicketState { get; set; }
        public bool IsInvoiced { get; set; }
        public string InputMethod { get; set; } = string.Empty;
    }

    public class InvoiceException : FrtAfcApiException
    {
        public InvoiceException(string message) : base(message) { }
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

    public class TicketNotFoundException : FrtAfcApiException
    {
        public TicketNotFoundException(string message) : base(message) { }
    }

    public class PasswordChangeException : FrtAfcApiException
    {
        public PasswordChangeException(string message) : base(message) { }
    }

    public class DebugModeDisabledException : FrtAfcApiException
    {
        public DebugModeDisabledException(string message) : base(message) { }
    }

    public class RefundException : FrtAfcApiException
    {
        public RefundException(string message) : base(message) { }
    }

    public class TicketValidationException : FrtAfcApiException
    {
        public TicketValidationException(string message) : base(message) { }
        public TicketValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}