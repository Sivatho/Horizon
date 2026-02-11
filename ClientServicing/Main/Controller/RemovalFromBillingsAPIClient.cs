using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.RemovalFromBillings;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.RemovalFromBillings.RemovalFromBillingsAPIEndPoints;

namespace ClientServicing.Main.Controller
{
    public class RemovalFromBillingsAPIClient : IRemovalFromBillings
    {
        readonly RestClient restClient;
        readonly UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        public RemovalFromBillingsAPIClient()
        {
            var options = new RestClientOptions()
            {
                BaseUrl = new Uri(utilitiesHelper.GetApiBaseUrl()),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            restClient = new RestClient(options);
        }

        public async Task<RestResponse> CancelRemovalFromBillingsAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = RemovalFromBillingsAPIEndPoints.GetEndPoint(EndPoints.CancelRemovalFromBillings);
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

        public async Task<RestResponse> RemovalFromBillingsHistoryAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = RemovalFromBillingsAPIEndPoints.GetEndPoint(EndPoints.RemovalFromBillingsHistory);
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

        public async Task<RestResponse> RemoveFromBillingsAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = RemovalFromBillingsAPIEndPoints.GetEndPoint(EndPoints.RemoveFromBillings);
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
