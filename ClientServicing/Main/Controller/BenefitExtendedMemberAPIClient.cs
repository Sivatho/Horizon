using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.BenefitExtendedMember;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.BenefitExtendedMember.BenefitExtendedMemberAPIEndPoints;


namespace ClientServicing.Main.Controller
{
    public class BenefitExtendedMemberAPIClient : IBenefitExtendedMember
    {
        readonly RestClient restClient;
        readonly UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        public BenefitExtendedMemberAPIClient()
        {
            var options = new RestClientOptions()
            {
                BaseUrl = new Uri(utilitiesHelper.GetApiBaseUrl()),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                //Authenticator = new OauthAPIAuthenticator()
            };
            restClient = new RestClient(options);
        }

     

        public async Task<RestResponse> BenefitExtendedMemberAPIClientAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(BenefitExtendedMemberAPIEndPoints.GetEndPoint(EndPoints.policyBenefitExtendedMember), Method.Post);
                request.AddJsonBody(payload);
                request.AddHeader("Accept", "application/json");

                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"\tpolicyBenefitExtendedMember > Response failed. Status:" +
                        $" {response.StatusCode}," +
                        $" {response.ErrorMessage}");
                }
                return response;
            }
            catch (Exception ex)
            {
                //Log Exception
                TestContext.Out.WriteLine($"\tpolicyBenefitExtendedMember > Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"\tpolicyBenefitExtendedMember > Stack Trace: {ex.StackTrace}");
                //Return a failed response
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
        }

        // Fixed implementation: use POST when sending a JSON payload and ensure this correctly implemented method is called by tests.
        public async Task<RestResponse> policyBenefitExtendedMemberAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(BenefitExtendedMemberAPIEndPoints.GetEndPoint(EndPoints.policyBenefitExtendedMember), Method.Post);
                request.AddJsonBody(payload);


                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"policyBenefitExtendedMember > Response failed. Status: {response.StatusCode}," +
                        $" {response.ErrorMessage}");
                }
                return response;
            }
            catch (Exception ex)
            {
                //Log Exception
                TestContext.Out.WriteLine($"\tpolicyBenefitExtendedMember > Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"\tpolicyBenefitExtendedMember > Stack Trace: {ex.StackTrace}");
                //Return a failed response
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };

                throw new NotImplementedException();
            }
        }

        public async Task<RestResponse> ben<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(BenefitExtendedMemberAPIEndPoints.GetEndPoint(EndPoints.policyBenefitExtendedMember), Method.Post);
                request.AddJsonBody(payload);

                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"BenefitExtendedMember > Response failed. Status:" +
                        $" {response.StatusCode}," +
                        $" {response.ErrorMessage}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"\tBenefitExtendedMember > Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"\tBenefitExtendedMember > Stack Trace: {ex.StackTrace}");
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
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

        public async Task<RestResponse> policyAccountingHistorySummaryAsync<T>(T payload) where T : class
        {
            // Create a request using the RestClient
            var request = new RestRequest("/accounting/history/summary", Method.Post);

            // Add the payload to the request body
            request.AddJsonBody(payload);

            // Execute the request asynchronously and return the response
            var response = await restClient.ExecuteAsync(request);

            // Optionally log the request and response for debugging
            utilitiesHelper.LogRequestAndResponse(request, response);

            return response;
        }

        public async Task<RestResponse> UpdateBenefitExtendedMemberAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(BenefitExtendedMemberAPIEndPoints.GetEndPoint(EndPoints.UpdateBenefitExtendedMember), Method.Post);
                request.AddJsonBody(payload);

                //Act
                var response = await restClient.ExecuteAsync(request);
                utilitiesHelper.LogRequestAndResponse(request, response);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"UpdateBenefitExtendedMember > Response failed. Status: {response.StatusCode}," +
                        $" {response.ErrorMessage}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"\tUpdateBenefitExtendedMember > Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"\tUpdateBenefitExtendedMember > Stack Trace: {ex.StackTrace}");
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
        }

    }
}




