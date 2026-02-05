using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.Payer;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.Payer.PayerEndPoints;

namespace ClientServicing.Main.Controller
{
    public class PayerAPIClient : IPayer
    {
        readonly RestClient restClient;
        readonly UtilitiesHelper utilitiesHelper = new UtilitiesHelper();
        public PayerAPIClient()
        {
            var options = new RestClientOptions()
            {
                BaseUrl = new Uri(utilitiesHelper.GetApiBaseUrl()),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            restClient = new RestClient(options);
        }
        public async Task<RestResponse> GetPayerDetailsByPolicyNumberAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = PayerEndPoints.GetEndPoint(EndPoints.GetPayerDetailsByPolicyNumber);
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

        public async Task<RestResponse> UpsertBankingAndPayerAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = PayerEndPoints.GetEndPoint(EndPoints.UpsertBankingAndPayer);
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
