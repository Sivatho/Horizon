using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.Bank;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.Bank.BankAPIEndPoints;

namespace ClientServicing.Main.Controller
{
    public class BankAPIClient : IBank, IDisposable
    {
        readonly RestClient restClient;
        readonly UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        public BankAPIClient(string baseUrl)
        {
            var options = new RestClientOptions(baseUrl)
            {
                BaseUrl = new Uri(baseUrl),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                //Authenticator = new OauthAPIAuthenticator()
            };
            restClient = new RestClient(options);
        }

        public void Dispose()
        {
            restClient?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<RestResponse> FetchBanksAsync<T>(T payload) where T : class
        {
            try {
                //Arrange
                var request = new RestRequest(BankAPIEndPoints.GetEndPoint(EndPoints.FetchBank),Method.Post);
                request.AddJsonBody(payload);
                
                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);
                
                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"FetchBank > Response failed. Status: {response.StatusCode}," +
                        $" {response.ErrorMessage}");
                }
                return response;
            }
            catch (Exception ex) {
                //Log Exception
                TestContext.Out.WriteLine($"\tFetchBank > Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"\tFetchBank > Stack Trace: {ex.StackTrace}");
                //Return a failed response
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<RestResponse> GetBankingDetailHistoryAsync(int policyNo)
        {
            try
            {
                //Arrange
                var request = new RestRequest(BankAPIEndPoints.GetEndPoint(EndPoints.GetBankingDetailHistory), Method.Get);
                request.AddParameter("policyNo", policyNo);               
                
                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"GetBankingDetailHistory > Response failed. Status: {response.StatusCode}," +
                        $" {response.ErrorMessage}");
                }
                return response;
            } 
            catch (Exception ex) {
                //Log Exception
                TestContext.Out.WriteLine($"\tGetBankingDetailHistory > Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"\tGetBankingDetailHistory > Stack Trace: {ex.StackTrace}");
                //Return a failed response
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<RestResponse> ValidateAccountNumberUsageLimitAsync(string accountNumber)
        {
            try
            {
                //Arrange
                var request = new RestRequest(BankAPIEndPoints.GetEndPoint(EndPoints.ValidateAccountNumberUsageLimit), Method.Get);
                request.AddParameter("accountNumber", accountNumber);

                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"ValidateAccountNumberUsageLimit > Response failed. Status: {response.StatusCode}," +
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

        public Task<RestResponse> ValidateBankAccountAsync<T>(T payload) where T : class
        {
            //Arrange
            //Act
            //Assert
            throw new NotImplementedException();
        }

        public Task<RestResponse> ValidateBankAccountQAVSRAsync<T>(T payload) where T : class
        {
            //Arrange
            //Act
            //Assert
            throw new NotImplementedException();
        }
    }
}
