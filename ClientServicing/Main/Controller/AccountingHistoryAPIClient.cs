using ClientServicing.Main.AbstractComponents.API.Base;
using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.AccountHistoryAPIEndPoints;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.AccountHistoryAPIEndPoints.AccountHistoryAPIEndPoints;


namespace ClientServicing.Main.Controller
{
    public class AccountingHistoryAPIClient : IAccountHistory
    {
        private readonly RestClient _restClient;

        public IRestLibrary SharedRestLibrary { get; }

        public AccountingHistoryAPIClient(IRestLibrary sharedRestLibrary)
        {
            _restClient = sharedRestLibrary.RestClient ?? throw new ArgumentNullException(nameof(sharedRestLibrary.RestClient));
        }

        public async Task<RestResponse> policyAccountingHistoryAsync<T>(T policyNo) where T : class
        {
            // Arrange
            var url = AccountHistoryAPIEndPoints.GetEndPoint(EndPoints.policyAccountingHistory);
            Method method = Method.Post;
            var request = ApiRequestAndResponseHelper.BuildRequest(
                url,
                method,
                policyNo,
                out var stopwatch);

            // Act
            var response = await ApiRequestAndResponseHelper.ExecuteAsync(_restClient, request, stopwatch);
            return response;

        }

        public async Task<RestResponse> policyCashReceipt(int policyNo)
        {
            /**/
            // Arrange
            var url = AccountHistoryAPIEndPoints.GetEndPoint(EndPoints.policyCashReceipt);
            Method method = Method.Get;
            IDictionary<string, string>? headers = new Dictionary<string, string>
            {
                { "Accept", "*/*" }
            };
            IDictionary<string, string>? queryParams = null;
            IDictionary<string, int>? urlSegment = new Dictionary<string, int>
            {
                ["policyNo"] = policyNo
            };
            var request = ApiRequestAndResponseHelper.BuildRequest<object?>(
                url,
                method,
                null,
                out var stopwatch,
                headers,
                queryParams, urlSegment);

            // Act
            var response = await ApiRequestAndResponseHelper.ExecuteAsync(_restClient, request, stopwatch);
            return response;
            
            /*try
            {
                //Arrange
                var request = new RestRequest(AccountHistoryAPIEndPoints.GetEndPoint(EndPoints.policyCashReceipt), Method.Get);
                request.AddUrlSegment("policyNo", policyNo);

                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"policyCashReceipt > Response failed. Status:" +
                        $" {response.StatusCode}," +
                        $" {response.ErrorMessage}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"\tpolicyCashReceipt > Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"\tpolicyCashReceipt > Stack Trace: {ex.StackTrace}");
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }*/
        }

        public async Task<RestResponse> GetStatementLineCD<T>(T payload) where T : class
        {
            // Arrange
            var url = AccountHistoryAPIEndPoints.GetEndPoint(EndPoints.GetStatementLineID);
            Method method = Method.Post;
            var request = ApiRequestAndResponseHelper.BuildRequest(
                url,
                method,
                payload,
                out var stopwatch);

            // Act
            var response = await ApiRequestAndResponseHelper.ExecuteAsync(_restClient, request, stopwatch);
            return response;

            /*try
            {
                //Arrange
                var request = new RestRequest(AccountHistoryAPIEndPoints.GetEndPoint(EndPoints.GetStatementLineID), Method.Post);
                request.AddJsonBody(payload);
                //Act
                var response = await restClient.ExecuteAsync(request);
                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"GetStatementLineID > Response failed. Status:" +
                        $" {response.StatusCode}," +
                        $" {response.ErrorMessage}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"\tGetStatementLineID > Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"\tGetStatementLineID> Stack Trace: {ex.StackTrace}");
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }*/
        }

        public Task<RestResponse> CashReceiptInforUpsert<T>(T payload) where T : class
        {
            //Arrange
            //Act
            //Assert
            throw new NotImplementedException();
        }

        public Task<RestResponse> policyAcountingHistorySummaryAsync<T>(T payload) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<RestResponse> policyCashReceiptAsync<T>(T payload) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<RestResponse> GetStatementLineIDAsync<T>(T payload) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<RestResponse> CashReceiptInfoUpsertAsync(string accountNumber)
        {
            throw new NotImplementedException();
        }

        public Task<RestResponse> ManualReceiptInfoUpsertAsync(int policyNo)
        {
            throw new NotImplementedException();
        }

        public async Task policyAcountingHistoryAsync()
        {
            throw new NotImplementedException();
        }

        public async Task policyAcountingHistoryAsync(object policyNo)
        {
            throw new NotImplementedException();
        }

        public Task<RestResponse> policyAccountingHistorySummaryAsync<T>(T payload) where T : class
        {
            throw new NotImplementedException();
        }
    }
}


