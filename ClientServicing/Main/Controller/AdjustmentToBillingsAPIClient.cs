using System.Net;
using ClientServicing.Main.AbstractComponents.API.Base;
using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.AccountHistoryAPIEndPoints;
using ClientServicing.Main.Resources.EndPoints.AdjustmentToBillings;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.AdjustmentToBillings.AdjustmentToBillingsAPIEndPoints;


namespace ClientServicing.Main.Controller
{
    public class AdjustmentToBillingsAPIClient : IAdjustmentToBillings

    {
        private readonly RestClient _restClient;

        public IRestLibrary SharedRestLibrary { get; }


        public AdjustmentToBillingsAPIClient(IRestLibrary sharedRestLibrary)
        {
            _restClient = sharedRestLibrary.RestClient ?? throw new ArgumentNullException(nameof(sharedRestLibrary.RestClient));
        }

        public async Task<RestResponse> AddAdjustementToBillingsAsync<T>(T payload) where T : class
        {

            //Arrange
            var url = AdjustmentToBillingsAPIEndPoints.GetEndPoint(EndPoints.AddAdjustmentToBillings);
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

			var response = await ApiRequestAndResponseHelper.ExecuteAsync(_restClient, request, stopwatch);
			return response;
		}

        public async Task<RestResponse> CancelAdjustmentToBillingsAsync<T>(T payload) where T : class
        {
            var url = AdjustmentToBillingsAPIEndPoints.GetEndPoint(EndPoints.CancelAdjustmentToBillings);
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

            var response = await ApiRequestAndResponseHelper.ExecuteAsync(_restClient, request, stopwatch);
            return response;
        }

		public async Task<RestResponse> GetAdjustedPeriodsAsync<T>(T payload) where T : class
		{
			var url = AdjustmentToBillingsAPIEndPoints.GetEndPoint(EndPoints.GetAdjustedPeriods);
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

			var response = await ApiRequestAndResponseHelper.ExecuteAsync(_restClient, request, stopwatch);
			return response;
		}

		public async Task<RestResponse> GetAdjustmentToBillingsHistoryAsync<T>(T payload) where T : class
		{
			var url = AdjustmentToBillingsAPIEndPoints.GetEndPoint(EndPoints.GetAdjustmentToBillingsHistory);
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

			var response = await ApiRequestAndResponseHelper.ExecuteAsync(_restClient, request, stopwatch);
			return response;
	 
        }

        public async Task<RestResponse> GetOutstandingPolicyPremiumsAsync<T>(T payload) where T : class
        {
			{
				var url = AdjustmentToBillingsAPIEndPoints.GetEndPoint(EndPoints.GetOutstandingPolicyPremiums);
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

				var response = await ApiRequestAndResponseHelper.ExecuteAsync(_restClient, request, stopwatch);
				return response;
			}
		}
    }

    public interface IAdjustmentToBillings
    {
    }
}
