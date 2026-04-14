using AppCore.Main.IController;
using AppCore.Main.Resources.EndPoints.Quating;
using ClientServicing.Main.AbstractComponents.API.Base;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static AppCore.Main.Resources.EndPoints.Quating.QuotingAPIEndPoints;

namespace AppCore.Main.Controller
{
    public class QuotingAPIClient : IQuoting
    {
        readonly RestClient restClient;
        public QuotingAPIClient(IRestLibrary restLibrary)
        {
            restClient = restLibrary.RestClient ?? throw new ArgumentNullException(nameof(restLibrary.RestClient));
        }

        public async Task<RestResponse> CreateNewBusinessQuoteAsync<T>(T payload) where T : class
        {
            //Arrange
            var url = QuotingAPIEndPoints.GetEndPoint(EndPoints.CreateNewBusinesssQuoate);

            //Act
            return await PostRequestAndResponsePayload(payload, url);
        }

        public async Task<RestResponse> GetQuoteRulesetAsync<T>(T payload) where T : class
        {
            //Arrange
            var url = QuotingAPIEndPoints.GetEndPoint(EndPoints.GetQuoteRuleset);

            //Act
            return await PostRequestAndResponsePayload(payload, url);
        }

        public async Task<RestResponse> PostRequestAndResponsePayload<T>(T payload, string url) where T : class {
            //Arrange           
            Method method = Method.Post;
            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Accept", "*/*" },
                { "Content-Type", "application/json-patch+json"}
            };
            var request = ApiRequestAndResponseHelper.BuildRequest(
               url,
               method,
               payload,
               out var stopwatch,
               headers);

            //Act
            var response = await ApiRequestAndResponseHelper.ExecuteAsync(restClient, request, stopwatch);
            return response;
        }
    }
}