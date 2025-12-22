using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.Email;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.Email.EmailAPIEndPoint;


namespace ClientServicing.Main.Controller
{
    public class EmailAPIClient : IEmail
    {
        readonly RestClient restClient;
        readonly UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        public EmailAPIClient()
        {
            var options = new RestClientOptions()
            {
                BaseUrl = new Uri(utilitiesHelper.GetApiBaseUrl()),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            restClient = new RestClient(options);
        }

        public async Task<RestResponse> SendInternalEmailsAsync<T>(T payload) where T : class
        {
            try
            {
                // Arrange
                var request = new RestRequest(EmailAPIEndPoint.GetEndPoint(EndPoints.SendInternalEmails), Method.Post);
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
