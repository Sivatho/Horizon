using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.GSD;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.GSD.GSDAPIEndPoint;

namespace ClientServicing.Main.Controller
{
    public class GSDAPIClient : IGSD
    {
        readonly RestClient restClient;
        readonly UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        public GSDAPIClient()
        {
            var options = new RestClientOptions()
            {
                BaseUrl = new Uri(utilitiesHelper.GetApiBaseUrl()),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            restClient = new RestClient(options);
        }

        public async Task<RestResponse> AffordabilityEnquiryAsync<T>(T payload) where T : class
        {
            try {
                //Arrange
                var request = new RestRequest(GSDAPIEndPoint.GetEndPoint(EndPoints.AffordabilityEnquiry), Method.Post);
                request.AddJsonBody(payload);

                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
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

        public async Task<RestResponse> EmployeeEnquiryAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(GSDAPIEndPoint.GetEndPoint(EndPoints.EmployeeEnquiry), Method.Post);
                request.AddJsonBody(payload);

                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
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
