using ClientServicing.Main.Resources.EndPoints.Debicheck;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static ClientServicing.Main.Resources.EndPoints.Debicheck.DebicheckAPIEndPoints;

namespace ClientServicing.Main.Controller
{
    public class DebicheckAPIClient
    {
        readonly RestClient restClient;
        readonly UtilitiesHelper utilitiesHelper = new UtilitiesHelper();
        public DebicheckAPIClient()
        {
            var options = new RestClientOptions()
            {
                BaseUrl = new Uri(utilitiesHelper.GetApiBaseUrl()),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                //Authenticator = new OauthAPIAuthenticator()
            };
            restClient = new RestClient(options);
        }
        public async Task<RestResponse> DebicheckCheckStatusAPIClientAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(DebicheckAPIEndPoints.GetEndPoint(EndPoints.CheckStatus), Method.Post);
                request.AddJsonBody(payload);
                request.AddHeader("Accept", "application/json");
                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);
                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"\tDebicheckAPIClientAsync > Response failed. Status:" +
                        $" {response.StatusCode}," +
                        $" {response.ErrorMessage}");
                }
                return response;
            }
            catch (Exception ex)
            {
                //Log Exception
                TestContext.Out.WriteLine($"\tDebicheckAPIClientAsync > Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"\tDebicheckAPIClientAsync > Stack Trace: {ex.StackTrace}");
                //Return a failed response
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
        }
        public async Task<RestResponse> DebicheckMandateRequestAPIClientAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(DebicheckAPIEndPoints.GetEndPoint(EndPoints.MandatesRequest), Method.Post);
                request.AddJsonBody(payload);
                request.AddHeader("Accept", "application/json");
                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);
                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"\tDebicheckMandateRequestAPIClientAsync > Response failed. Status:" +
                        $" {response.StatusCode}," +
                        $" {response.ErrorMessage}");
                }
                return response;
            }
            catch (Exception ex)
            {
                //Log Exception
                TestContext.Out.WriteLine($"\tDebicheckMandateRequestAPIClientAsync > Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"\tDebicheckMandateRequestAPIClientAsync > Stack Trace: {ex.StackTrace}");
                //Return a failed response
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
        }

        internal async Task DebicheckMandateAPIClientAsync<T>(T mandaterequest)
        {
            throw new NotImplementedException();
        }
    }
}
