using ClientServicing.Main.AbstractComponents.API.Base;
using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.SendPayAtNumber;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.SendPayAtNumber.SendPayAtNumberAPIEndPoints;

namespace ClientServicing.Main.Controller
{
    public class SendPayAtNumberAPIClient : ISendPayAtNumber
    {
        private readonly RestClient _restClient;
        readonly UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        public SendPayAtNumberAPIClient()
        {
            var restLibrary = new RestLibrary();
            _restClient = restLibrary.restClient;
        }

        public async Task<RestResponse> SendTextMessageAsync<T>(T payload) where T : class
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
            var response = await ApiRequestAndResponseHelper.ExecuteAsync(_restClient, request, stopwatch);
            return response;
        }
    }
}
