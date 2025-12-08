using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.AccountHistoryAPIEndPoints;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClientServicing.Main.Resources.EndPoints.AccountHistoryAPIEndPoints.AccountHistoryAPIEndPoints;


namespace ClientServicing.Main.Controller
{
    public class AccountingHistoryAPIClient : IAccountHistory, IDisposable
    {
        readonly RestClient restClient;
        readonly UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        public AccountingHistoryAPIClient(string baseUrl)
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

        public async Task<RestResponse> policyAccountingHistoryAsync<T>(T policyNo) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(AccountHistoryAPIEndPoints.GetEndPoint(EndPoints.policyAccountingHistory), Method.Post);
                request.AddJsonBody(policyNo);
                request.AddHeader("Accept", "application/json");

                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"policyAcountingHistory > Response failed. Status:" +
                        $" {response.StatusCode}," +
                        $" {response.ErrorMessage}");
                }
                return response;
            }
            catch (Exception ex)
            {
                //Log Exception
                TestContext.Out.WriteLine($"\tpolicyAcountingHistory > Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"\tpolicyAcountingHistory > Stack Trace: {ex.StackTrace}");
                //Return a failed response
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
        }
        /*
        public async Task<RestResponse> policyAccountingHistorySummaryAsync<T>(T policyNo)

        {
            try
            {
                //Arrange
                var request = new RestRequest(AccountHistoryAPIEndPoints.GetEndPoint(EndPoints.policyAccountingHistorySummary), Method.Get);
                request.AddBody(policyNo);

                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"policyAcountingHistorySummary > Response failed. Status: {response.StatusCode}," +
                        $" {response.ErrorMessage}");
                }
                return response;
            }
            catch (Exception ex)
            {
                //Log Exception
                TestContext.Out.WriteLine($"\tpolicyAcountingHistorySummary > Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"\tpolicyAcountingHistorySummary > Stack Trace: {ex.StackTrace}");
                //Return a failed response
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
        }*/

        public async Task<RestResponse> policyCashReceipt(int policyNo)
        {
            try
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
            }
        }

        public async Task<RestResponse> GetStatementLineCD<T>(T payload) where T : class
        {
            try
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
            }
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


