using ClientServicing.Main.AbstractComponents.API.Base;
using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.Bank;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.Bank.BankAPIEndPoints;

namespace ClientServicing.Main.Controller
{
    public class BankAPIClient : IBank
    {
        readonly RestClient restClient;
        private readonly IRestLibrary sharedRestLibrary;

        public BankAPIClient(IRestLibrary sharedRestLibrary)
        {
            restClient = sharedRestLibrary.RestClient ?? throw new ArgumentNullException(nameof(sharedRestLibrary.RestClient));
        }

        public async Task<RestResponse> CanChangeBankAccountAsync(int bankAccountId)
        {
            //Arrange
            var url = BankAPIEndPoints.GetEndPoint(EndPoints.CanChangeBankAccount);
             Method method = Method.Get;
            IDictionary<string, string>? headers = new Dictionary<string, string>
            {
                { "Accept", "*/*" }
            };
            IDictionary<string, int>? urlSegment = new Dictionary<string, int>
             {
                 ["bankAccountId"] = bankAccountId
             };
             var request = ApiRequestAndResponseHelper.BuildRequest<object?>(
                 url,
                 method,
                 null,
                 out var stopwatch,
                 headers,
                 null,
                 urlSegment);
             // Act
             var response = await ApiRequestAndResponseHelper.ExecuteAsync(restClient, request, stopwatch);
             return response;
        }
        public async Task<RestResponse> FetchBanksAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = BankAPIEndPoints.GetEndPoint(EndPoints.FetchBank);
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
            var response = await ApiRequestAndResponseHelper.ExecuteAsync(restClient, request, stopwatch);
            return response;
        }
        public async Task<RestResponse> GetBankingDetailHistoryAsync(int policyNo)
        {
            //Arrange
            var url = BankAPIEndPoints.GetEndPoint(EndPoints.GetBankingDetailHistory);
            Method method = Method.Get;
            IDictionary<string, string>? headers = new Dictionary<string, string>
            {
                { "Accept", "*/*" }
            };
            IDictionary<string, int>? urlSegment = new Dictionary<string, int>
            {
                ["policyNo"] = policyNo
            };
            var request = ApiRequestAndResponseHelper.BuildRequest<object?>(
                url,
                method,
                null,
                out var stopwatch,
                headers,
                null,
                urlSegment);
            // Act
            var response = await ApiRequestAndResponseHelper.ExecuteAsync(restClient, request, stopwatch);
            return response;
        }
        public async Task<RestResponse> ValidateAccountNumberUsageLimitAsync(string accountNumber)
        {       
            //Arrange
            var url = BankAPIEndPoints.GetEndPoint(EndPoints.ValidateAccountNumberUsageLimit);
            Method method = Method.Get;
            IDictionary<string, string>? headers = new Dictionary<string, string>
            {
                { "Accept", "*/*" }
            };
            IDictionary<string, int>? urlSegment = new Dictionary<string, int>
            {
                ["accountNumber"] = int.Parse(accountNumber)
            };
            var request = ApiRequestAndResponseHelper.BuildRequest<object?>(
                url,
                method,
                null,
                out var stopwatch,
                headers,
                null,
                urlSegment);
            // Act
            var response = await ApiRequestAndResponseHelper.ExecuteAsync(restClient, request, stopwatch);
            return response;
        }
        public async Task<RestResponse> ValidateBankAccountAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = BankAPIEndPoints.GetEndPoint(EndPoints.ValidateBankAccount);
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
            var response = await ApiRequestAndResponseHelper.ExecuteAsync(restClient, request, stopwatch);
            return response;
        }
        public async Task<RestResponse> ValidateBankAccountQAVSRAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = BankAPIEndPoints.GetEndPoint(EndPoints.ValidateBankAccountQAVSR);
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