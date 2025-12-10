using System.Net;
using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.AccountHistoryAPIEndPoints;
using ClientServicing.Main.Resources.EndPoints.AdjustmentToBillings;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.AdjustmentToBillings.AdjustmentToBillingsEndPoints;

namespace ClientServicing.Main.Controller
{
    public class AdjustmentToBillingsAPIClient : IAdjustmentToBillings
    {
        readonly RestClient restClient;
        readonly UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        public AdjustmentToBillingsAPIClient(string baseUrl)
        {
            var options = new RestClientOptions(baseUrl)
            {
                BaseUrl = new Uri(baseUrl),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                //Authenticator = new OauthAPIAuthenticator()
            };
            restClient = new RestClient(options);
        }
        
        public async Task<RestResponse> AddAdjustementToBillingsAsync<T>(T payload) where T : class
        {
            try {
                //Arrange
                var request = new RestRequest(AdjustmentToBillingsEndPoints.GetEndPoint(EndPoints.AddAdjustmentToBillings), Method.Post);
                request.AddBody(payload);



                bool responseBool = true;

                // Create a RestResponse object and set its properties
                RestResponse restResponse = new RestResponse
                {
                    StatusCode = responseBool ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                    Content = responseBool.ToString() // or any other content you want
                };


                utilitiesHelper.LogRequestAndResponse(request, restResponse);
                
                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"AddAdjustementToBillings > Response failed. Status:" +
                        $" {response.StatusCode}," +
                        $" {response.ErrorMessage}");
                }
                return response;
            }
            catch (Exception ex) {
                TestContext.Out.WriteLine($"\tAddAdjustementToBillings > Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"\tAddAdjustementToBillings > Stack Trace: {ex.StackTrace}");
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<RestResponse> CancelAdjustmentToBillingsAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(AdjustmentToBillingsEndPoints.GetEndPoint(EndPoints.CancelAdjustmentToBillings), Method.Post);
                request.AddBody(payload);

                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"AddAdjustementToBillings > Response failed. Status:" +
                        $" {response.StatusCode}," +
                        $" {response.ErrorMessage}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"\tAddAdjustementToBillings > Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"\tAddAdjustementToBillings > Stack Trace: {ex.StackTrace}");
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<RestResponse> GetAdjustedPeriodsAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(AdjustmentToBillingsEndPoints.GetEndPoint(EndPoints.GetAdjustedPeriods), Method.Post);
                request.AddBody(payload);

                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"AddAdjustementToBillings > Response failed. Status:" +
                        $" {response.StatusCode}," +
                        $" {response.ErrorMessage}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"\tAddAdjustementToBillings > Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"\tAddAdjustementToBillings > Stack Trace: {ex.StackTrace}");
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<RestResponse> GetAdjustmentToBillingsHistoryAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(AdjustmentToBillingsEndPoints.GetEndPoint(EndPoints.GetAdjustmentToBillingsHistory), Method.Post);
                request.AddBody(payload);

                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"AddAdjustementToBillings > Response failed. Status:" +
                        $" {response.StatusCode}," +
                        $" {response.ErrorMessage}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"\tAddAdjustementToBillings > Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"\tAddAdjustementToBillings > Stack Trace: {ex.StackTrace}");
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<RestResponse> GetOutstandingPolicyPremiumsAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(AdjustmentToBillingsEndPoints.GetEndPoint(EndPoints.GetOutstandingPolicyPremiums), Method.Post);
                request.AddBody(payload);

                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"AddAdjustementToBillings > Response failed. Status:" +
                        $" {response.StatusCode}," +
                        $" {response.ErrorMessage}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"\tAddAdjustementToBillings > Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"\tAddAdjustementToBillings > Stack Trace: {ex.StackTrace}");
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
