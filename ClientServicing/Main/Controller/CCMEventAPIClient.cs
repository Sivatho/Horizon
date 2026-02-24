using ClientServicing.Main.AbstractComponents.API.Base;
using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.CCMEvent;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.CCMEvent.CCMEventAPIEndPoints;

namespace ClientServicing.Main.Controller
{
    public class CCMEventAPIClient : ICCMEvent
    {
        private readonly RestClient _restClient;

        public IRestLibrary SharedRestLibrary { get; }

        public CCMEventAPIClient(IRestLibrary sharedRestLibrary)
        {
            _restClient = sharedRestLibrary.RestClient ?? throw new ArgumentNullException(nameof(sharedRestLibrary.RestClient));
        }

        public async Task<RestResponse> GetEventDetailConstructBPEAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = CCMEventAPIEndPoints.GetEndPoint(EndPoints.GetEventDetailConstructBPE);
            Method method = Method.Post;
            IDictionary<string, string>? headers = new Dictionary<string, string>
            {
                { "Accept", "*/*" }
            };
            var request = ApiRequestAndResponseHelper.BuildRequest(
                url,
                method,
                payload,
                out var stopwatch,
                headers);

            // Act
            var response = await ApiRequestAndResponseHelper.ExecuteAsync(_restClient, request, stopwatch);
            return response;
        }

        public async Task<RestResponse> TriggerEventAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = CCMEventAPIEndPoints.GetEndPoint(EndPoints.TriggerEvent);
            Method method = Method.Post;
            IDictionary<string, string>? headers = new Dictionary<string, string>
            {
                { "Accept", "*/*" }
            };
            var request = ApiRequestAndResponseHelper.BuildRequest(
                url,
                method,
                payload,
                out var stopwatch,
                headers);

            // Act
            var response = await ApiRequestAndResponseHelper.ExecuteAsync(_restClient, request, stopwatch);
            return response;
        }
    }
}
