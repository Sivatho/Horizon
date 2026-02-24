using ClientServicing.Main.AbstractComponents.API.Base;
using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.AccountHistoryAPIEndPoints;
using ClientServicing.Main.Resources.EndPoints.CancelPolicy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.CancelPolicy.CancelPolicyAPIEndPoints;

namespace ClientServicing.Main.Controller
{
    public class CancelPolicyAPIClient : ICancelPolicy
    {
        private readonly IRestLibrary restLibrary;
        private readonly RestClient _restClient;

        public CancelPolicyAPIClient(IRestLibrary restLibrary)
        {
            _restClient = restLibrary.RestClient ?? throw new ArgumentNullException(nameof(restLibrary.RestClient));
        }
        public async Task<RestResponse> UpdateCancelPolicyDetailsAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = CancelPolicyAPIEndPoints.GetEndPoint(EndPoints.UpdateCancelPolicyDetails);
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
