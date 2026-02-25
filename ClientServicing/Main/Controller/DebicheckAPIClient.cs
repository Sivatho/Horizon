using ClientServicing.Main.AbstractComponents.API.Base;
using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.Debicheck;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.Debicheck.DebicheckAPIEndPoints;

namespace ClientServicing.Main.Controller
{
    public class DebicheckAPIClient : IDebicheck
    {
        private readonly RestClient _restClient;
        private readonly IRestLibrary sharedRestLibrary;

        public DebicheckAPIClient(IRestLibrary sharedRestLibrary)
        {
            _restClient = sharedRestLibrary.RestClient ?? throw new ArgumentNullException(nameof(sharedRestLibrary.RestClient));
        }
        public async Task<RestResponse> CheckStatusAsync<T>(T payload) where T : class
        {
            var url = DebicheckAPIEndPoints.GetEndPoint(EndPoints.CheckStatus);
            Method method = Method.Post;
            IDictionary<string, string>? headers = new Dictionary<string, string>
            {
                { "Accept", "application/json" }
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

        public async Task<RestResponse> DebicheckRequestRetryAsync<T>(T payload) where T : class
        {
            var url = DebicheckAPIEndPoints.GetEndPoint(EndPoints.DebicheckRequestRetry);
            Method method = Method.Post;
            IDictionary<string, string>? headers = new Dictionary<string, string>
            {
                { "Accept", "application/json" }
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

        public async Task<RestResponse> DebicheckRetryCheckStatusAsync<T>(T payload) where T : class
        {
            var url = DebicheckAPIEndPoints.GetEndPoint(EndPoints.DebicheckRetryCheckStatus);
            Method method = Method.Post;
            IDictionary<string, string>? headers = new Dictionary<string, string>
            {
                { "Accept", "application/json" }
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

        public async Task<RestResponse> DetermineMandateTypeAsync<T>(T payload) where T : class
        {
            var url = DebicheckAPIEndPoints.GetEndPoint(EndPoints.DetermineMandateType);
            Method method = Method.Post;
            IDictionary<string, string>? headers = new Dictionary<string, string>
            {
                { "Accept", "application/json" }
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

        public async Task<RestResponse> MandatesRequestAsync<T>(T payload) where T : class
        {
            var url = DebicheckAPIEndPoints.GetEndPoint(EndPoints.MandatesRequest);
            Method method = Method.Post;
            IDictionary<string, string>? headers = new Dictionary<string, string>
            {
                { "Accept", "application/json" }
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
