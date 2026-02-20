using ClientServicing.Main.AbstractComponents.API.Base;
using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.AccountHistoryAPIEndPoints;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.AccountHistoryAPIEndPoints.AccountHistoryAPIEndPoints;


namespace ClientServicing.Main.Controller
{
    public class AccountingHistoryAPIClient : IAccountHistory
    {
        private readonly RestClient _restClient;

        public IRestLibrary SharedRestLibrary { get; }

        public AccountingHistoryAPIClient(IRestLibrary sharedRestLibrary)
        {
            _restClient = sharedRestLibrary.RestClient ?? throw new ArgumentNullException(nameof(sharedRestLibrary.RestClient));
        }

        public async Task<RestResponse> policyAccountingHistoryAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = AccountHistoryAPIEndPoints.GetEndPoint(EndPoints.policyAccountingHistory);
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

        public async Task<RestResponse> policyCashReceiptAsync<T>(T payload) where T : class
        {
            var url = AccountHistoryAPIEndPoints.GetEndPoint(EndPoints.policyCashReceipt);
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

        public async Task<RestResponse> GetStatementLineIDAsync<T>(T payload) where T : class
        {
            var url = AccountHistoryAPIEndPoints.GetEndPoint(EndPoints.GetStatementLineID);
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

        public async Task<RestResponse> CashReceiptInfoUpsertAsync<T>(T payload) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<RestResponse> ManualReceiptInfoUpsertAsync<T>(T payload) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<RestResponse> policyAccountingHistorySummaryAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = AccountHistoryAPIEndPoints.GetEndPoint(EndPoints.policyAcountingHistorySummary);
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


