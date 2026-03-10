using ClientServicing.Main.AbstractComponents.API.Base;
using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.Bank;
using ClientServicing.Main.Resources.EndPoints.BeneficiaryDetails;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.BeneficiaryDetails.BeneficiaryDetailsAPIEndPoints;

namespace ClientServicing.Main.Controller
{
    public class BeneficiaryDetailsAPIClient : IBeneficiaryDetails
    {
        readonly RestClient restClient;
        readonly UtilitiesHelper utilitiesHelper = new UtilitiesHelper();
        private readonly IRestLibrary sharedRestLibrary;

        public BeneficiaryDetailsAPIClient(IRestLibrary sharedRestLibrary)
        {
            restClient = sharedRestLibrary.RestClient ?? throw new ArgumentNullException(nameof(sharedRestLibrary.RestClient));
        }
        public async Task<RestResponse> GetAndCachePolicyBeneficiaryDetailsAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = BeneficiaryDetailsAPIEndPoints.GetEndPoint(EndPoints.GetAndCachePolicyBeneficiaryDetails);
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
        public async Task<RestResponse> GetCachedBeneficiaryListAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = BeneficiaryDetailsAPIEndPoints.GetEndPoint(EndPoints.GetCachedBeneficiaryList);
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
        public async Task<RestResponse> GetInsuredWithBenefit<T>(T payload) where T : class
        {
            // Arrange
            var url = BeneficiaryDetailsAPIEndPoints.GetEndPoint(EndPoints.GetInsuredWithBenefit);
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
        public async Task<RestResponse> PolicyBeneficiaryDetailsAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = BeneficiaryDetailsAPIEndPoints.GetEndPoint(EndPoints.policyBeneficiaryDetails);
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
        public async Task<RestResponse> PolicyEntityInfoUpsertAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = BeneficiaryDetailsAPIEndPoints.GetEndPoint(EndPoints.PolicyEntityInfoUpsert);
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
        public async Task<RestResponse> SaveUpdatedBeneficiariesAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = BeneficiaryDetailsAPIEndPoints.GetEndPoint(EndPoints.SaveUpdatedBeneficiaries);
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
        public async Task<RestResponse> UpdatePolicyBeneficiaryCacheAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = BeneficiaryDetailsAPIEndPoints.GetEndPoint(EndPoints.UpdatePolicyBeneficiaryCache);
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
    }
}
