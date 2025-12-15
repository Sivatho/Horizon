using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.BeneficiaryDetails;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.BeneficiaryDetails.BeneficiaryDetailsAPIEndPoints;

namespace ClientServicing.Main.Controller
{
    public class BeneficiaryDetailsAPIClient : IBeneficiaryDetails
    {
        readonly RestClient restClient;
   
         readonly UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        public BeneficiaryDetailsAPIClient()
        {
            var options = new RestClientOptions()
            {
                BaseUrl = new Uri(utilitiesHelper.GetApiBaseUrl()),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                //Authenticator = new OauthAPIAuthenticator()
            };
            restClient = new RestClient(options);
        }
        public async Task<RestResponse> GetAndCachePolicyBeneficiaryDetailsAsync<T>(T payload) where T : class
        {
            try {
                //Arrange
                var request = new RestRequest(BeneficiaryDetailsAPIEndPoints.GetEndPoint(EndPoints.GetAndCachePolicyBeneficiaryDetails), Method.Post);
                request.AddJsonBody(payload);

                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                //Assert
                if(!response.IsSuccessful) {
                    TestContext.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex) { 
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> GetCachedBeneficiaryListAsync<T>(T payload) where T : class
        {
            try {
                //Arrange
                var request = new RestRequest(BeneficiaryDetailsAPIEndPoints.GetEndPoint(EndPoints.GetCachedBeneficiaryList), Method.Post);
                request.AddJsonBody(payload);

                //Act 
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                //Assert
                if (!response.IsSuccessful) {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> PolicyBeneficiaryDetailsAsync<T>(T payload) where T : class
        {
            try { 
                //Arrange
                var request = new RestRequest(BeneficiaryDetailsAPIEndPoints.GetEndPoint(
                    EndPoints.policyBeneficiaryDetails),
                    Method.Post);
                request.AddJsonBody(payload);

                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                //Assert
                if (!response.IsSuccessful) {
                    TestContext.Out.WriteLine($"API call failed with status code: " +
                        $"{response.StatusCode} and message: " +
                        $"{response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> PolicyEntityInfoUpsertAsync<T>(T payload) where T : class
        {
            try { 
                //Arrange
                var request = new RestRequest(BeneficiaryDetailsAPIEndPoints.GetEndPoint(EndPoints.PolicyEntityInfoUpsert), Method.Post);
                request.AddJsonBody(payload);
                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);
                //Assert
                if (!response.IsSuccessful) {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            } 
            catch (Exception ex) {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> SaveUpdatedBeneficiariesAsync<T>(T payload) where T : class
        {
            try {
                //Arrange
                var request = new RestRequest(BeneficiaryDetailsAPIEndPoints.GetEndPoint(EndPoints.SaveUpdatedBeneficiaries), Method.Post);
                request.AddJsonBody(payload);
                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);
                //Assert
                if (!response.IsSuccessful) {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            } 
            catch (Exception ex) {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> UpdatePolicyBeneficiaryCacheAsync<T>(T payload) where T : class
        {
            try { 
                //Arrange
                var request = new RestRequest(BeneficiaryDetailsAPIEndPoints.GetEndPoint(EndPoints.UpdatePolicyBeneficiaryCache), Method.Post);
                request.AddJsonBody(payload);
                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);
                //Assert
                if (!response.IsSuccessful) {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }
    }
}
