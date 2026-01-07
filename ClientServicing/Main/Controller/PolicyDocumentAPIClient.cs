using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.PolicyDocument;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.Controller
{
    public class PolicyDocumentAPIClient : IPolicyDocument
    {
        readonly RestClient restClient;
        readonly UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        public PolicyDocumentAPIClient()
        {
            var options = new RestClientOptions()
            {
                BaseUrl = new Uri(utilitiesHelper.GetApiBaseUrl()),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            restClient = new RestClient(options);
        }
        public async Task<RestResponse> CheckPolicyDocumentExistAsync<T>(T payload) where T : class
        {
            try {
                //Arrange
                var request = new RestRequest(PolicyDocumentAPIEndPoints.GetEndPoint(PolicyDocumentAPIEndPoints.EndPoints.CheckPolicyDocumentExist), Method.Post);
                request.AddJsonBody(payload);
                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);
                //Assert
                if (!response.IsSuccessful)
                {
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

        public async Task<RestResponse> RetrievePolicyDocumentDetailsAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyDocumentAPIEndPoints.GetEndPoint(PolicyDocumentAPIEndPoints.EndPoints.RetrievePolicyDocumentDetails), Method.Post);
                request.AddJsonBody(payload);
                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);
                //Assert
                if (!response.IsSuccessful)
                {
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

        public async Task<RestResponse> RetrievePolicyDocumentsAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyDocumentAPIEndPoints.GetEndPoint(PolicyDocumentAPIEndPoints.EndPoints.RetrievePolicyDocuments), Method.Post);
                request.AddJsonBody(payload);
                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);
                //Assert
                if (!response.IsSuccessful)
                {
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

        public async Task<RestResponse> UpsertPolicyDocumentAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyDocumentAPIEndPoints.GetEndPoint(PolicyDocumentAPIEndPoints.EndPoints.UpsertPolicyDocument), Method.Post);
                request.AddJsonBody(payload);
                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);
                //Assert
                if (!response.IsSuccessful)
                {
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
