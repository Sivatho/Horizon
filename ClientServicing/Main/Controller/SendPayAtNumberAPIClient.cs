using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.SendPayAtNumber;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.SendPayAtNumber.SendPayAtNumberAPIEndPoints;

namespace ClientServicing.Main.Controller
{
    public class SendPayAtNumberAPIClient : ISendPayAtNumber
    {
        readonly RestClient restClient;
        readonly UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        public SendPayAtNumberAPIClient()
        {
            var options = new RestClientOptions()
            {
                BaseUrl = new Uri(utilitiesHelper.GetApiBaseUrl()),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            restClient = new RestClient(options);
        }

        public async Task<RestResponse> Send_Text_MessageAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = SendPayAtNumberAPIEndPoints.GetEndPoint(EndPoints.send_text_message);
            Method method = Method.Post;
            var request = ApiRequestAndResponseHelper.BuildRequest(
                url,
                method,
                payload,
                out var stopwatch);

            // Act
            var response = await ApiRequestAndResponseHelper.ExecuteAsync(restClient, request, stopwatch);
            return response;
        }
    }
}
