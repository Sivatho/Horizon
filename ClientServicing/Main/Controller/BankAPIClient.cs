using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.Bank;
using ClientServicing.Main.Resources.EndPoints.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.Bank.BankAPIEndPoints;

namespace ClientServicing.Main.Controller
{
    public class BankAPIClient : IBank, IDisposable
    {
        readonly RestClient restClient;
        readonly UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        public BankAPIClient()
        {
            var options = new RestClientOptions()
            {
                BaseUrl = new Uri(utilitiesHelper.GetApiBaseUrl()),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                //Authenticator = new OauthAPIAuthenticator()
            };
            restClient = new RestClient(options);
        }

        public async Task<RestResponse> CanChangeBankAccountAsync(int bankAccountId)
        {
            //Arrange
            var url = BankAPIEndPoints.GetEndPoint(EndPoints.CanChangeBankAccount);
             Method method = Method.Get;
             IDictionary<string, string>? headers = null;
             IDictionary<string, int>? urlSegment = new Dictionary<string, int>
             {
                 ["bankAccountId"] = bankAccountId
             };
             var request = ApiRequestAndResponseHelper.GetRequestDetails<object?>(
                 url,
                 method,
                 null,
                 out var stopwatch,
                 null,
                 null,
                 urlSegment);

             // Act
             var response = await ApiRequestAndResponseHelper.ExecuteAsync(restClient, request, stopwatch);
             return response;
        }
        public async Task<RestResponse> FetchBanksAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = BankAPIEndPoints.GetEndPoint(EndPoints.FetchBank);
            Method method = Method.Post;
            var request = ApiRequestAndResponseHelper.GetRequestDetails(
                url,
                method,
                payload,
                out var stopwatch);

            // Act
            var response = await ApiRequestAndResponseHelper.ExecuteAsync(restClient, request, stopwatch);
            return response;
        }
        public async Task<RestResponse> GetBankingDetailHistoryAsync(int policyNo)
        {
            //Arrange
            var url = BankAPIEndPoints.GetEndPoint(EndPoints.GetBankingDetailHistory);
            Method method = Method.Get;
            IDictionary<string, string>? headers = null;
            IDictionary<string, int>? urlSegment = new Dictionary<string, int>
            {
                ["policyNo"] = policyNo
            };
            var request = ApiRequestAndResponseHelper.GetRequestDetails<object?>(
                url,
                method,
                null,
                out var stopwatch,
                null,
                null,
                urlSegment);

            // Act
            var response = await ApiRequestAndResponseHelper.ExecuteAsync(restClient, request, stopwatch);
            return response;
        }
        public async Task<RestResponse> ValidateAccountNumberUsageLimitAsync(string accountNumber)
        {
            try
            {
                //Arrange
                var request = new RestRequest(BankAPIEndPoints.GetEndPoint(EndPoints.ValidateAccountNumberUsageLimit), Method.Get);
                request.AddUrlSegment("accountNumber", accountNumber);

                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"ValidateAccountNumberUsageLimit > Response failed. Status:" +
                        $" {response.StatusCode}," +
                        $" {response.ErrorMessage}");
                }
                return response;
            }
            catch(Exception ex) {
                TestContext.Out.WriteLine($"\tValidateAccountNumberUsageLimit > Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"\tValidateAccountNumberUsageLimit > Stack Trace: {ex.StackTrace}");
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
        }
        public async Task<RestResponse> ValidateBankAccountAsync<T>(T payload) where T : class
        {
            try {
                // Arrange
                var request = new RestRequest(BankAPIEndPoints.GetEndPoint(EndPoints.ValidateBankAccount),Method.Post);
                request.AddJsonBody(payload);
                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                // Assert
                if (!response.IsSuccessful) {
                    TestContext.Out.WriteLine($"ValidateBankAccount > Response failed. Status:" +
                        $" {response.StatusCode}," +
                        $" {response.ErrorMessage}");
                }
                return response;
            }
            catch (Exception ex) {
                TestContext.Out.WriteLine($"\tValidateBankAccount > Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"\tValidateBankAccount > Stack Trace: {ex.StackTrace}");
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
        }
        public async Task<RestResponse> ValidateBankAccountQAVSRAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = BankAPIEndPoints.GetEndPoint(EndPoints.ValidateBankAccountQAVSR);
            Method method = Method.Post;
            var request = ApiRequestAndResponseHelper.GetRequestDetails(
                url,
                method,
                payload,
                out var stopwatch);

            // Act
            var response = await ApiRequestAndResponseHelper.ExecuteAsync(restClient, request, stopwatch);
            return response;
        }
        public void Dispose()
        {
            restClient?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}