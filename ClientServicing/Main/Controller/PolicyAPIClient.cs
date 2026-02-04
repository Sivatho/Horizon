using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.Bank;
using ClientServicing.Main.Resources.EndPoints.GSD;
using ClientServicing.Main.Resources.EndPoints.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.Policy.PolicyAPIEndPoints;

namespace ClientServicing.Main.Controller
{
    public class PolicyAPIClient : IPolicy
    {
        readonly RestClient restClient;
        readonly UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        public PolicyAPIClient()
        {
            var options = new RestClientOptions()
            {
                BaseUrl = new Uri(utilitiesHelper.GetApiBaseUrl()),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            restClient = new RestClient(options);
        }

        public async Task<RestResponse> AdvancedPersonSearchAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.AdvancedPersonSearch);
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

        public async Task<RestResponse> ChangeMainMemberUpsertAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.ChangeMainMemberUpsert);
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

        public async Task<RestResponse> ChangePolicyDOCAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.ChangePolicyDOC);
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

        public async Task<RestResponse> CheckHasProductAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.CheckHasProduct);
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

        public async Task<RestResponse> CheckPolicyIfMainMemberOnlyAsync(int policyNo)
        {
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.CheckPolicyIfMainMemberOnly);
            Method method = Method.Get;
            IDictionary<string, string>? headers = new Dictionary<string, string>
            {
                { "Accept", "*/*" }
            };
            IDictionary<string, string>? queryParams = null;
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
                queryParams, urlSegment);

            // Act
            var response = await ApiRequestAndResponseHelper.ExecuteAsync(restClient, request, stopwatch);
            return response;
        }

        public async Task<RestResponse> CheckRefundAvailabilityAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.CheckRefundAvailability);
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

        public async Task<RestResponse> CheckRestartEligibilityAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.CheckRestartEligibility);
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

        public async Task<RestResponse> CheckWaitingPeriodAsync<T>(T payload) where T : class
        { ////Claims is spCheckWaitingPeriod no longer used as there is a new project in development for claims
            // Arrange
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.CheckWaitingPeriod);
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

        public async Task<RestResponse> EntityInfoUpsertAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.EntityInfoUpsert);
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

        public async Task<RestResponse> FetchPolicyStatusAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.FetchPolicyStatus);
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

        public async Task<RestResponse> GetBenefitCoverScreenHospitalAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.GetBenefitCoverScreenHospital);
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

        public async Task<RestResponse> GetBenefitCoverScreenWealthAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.GetBenefitCoverScreenWealth);
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

        public async Task<RestResponse> GetCustomerPolicyInfoByEntityNoAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.GetCustomerPolicyInfoByEntityNo);
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

        public async Task<RestResponse> GetMainMemChangeHistoryAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.GetMainMemChangeHistory);
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

        public async Task<RestResponse> GetPolicyAndMainMemberDetailsByPolicyNumberAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.GetPolicyAndMainMemberDetailsByPolicyNumber);
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

        public async Task<RestResponse> GetPolicyProductLineAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.GetPolicyProductLine);
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

        public async Task<RestResponse> GetPossibleMainMembersAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.GetPossibleMainMembers);
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

        public async Task<RestResponse> GetUnmentPremiumAsync(int policyNo)
        {
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.GetUnmentPremium);
            Method method = Method.Get;
            IDictionary<string, string>? headers = new Dictionary<string, string>
            {
                { "Accept", "*/*" }
            };
            IDictionary<string, string>? queryParams = null;
            IDictionary< string, int>? urlSegment = new Dictionary<string, int>
            {
                ["PolicyNo"] = policyNo
            };
            var request = ApiRequestAndResponseHelper.BuildRequest<object?>(
                url,
                method,
                null,
                out var stopwatch,
                headers,
                queryParams, urlSegment);

            // Act
            var response = await ApiRequestAndResponseHelper.ExecuteAsync(restClient, request, stopwatch);
            return response;
        }

        public async Task<RestResponse> InsertPolicyNoteAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.InsertPolicyNote);
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

        public async Task<RestResponse> PersonSearchAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.PersonSearch);
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

        public async Task<RestResponse> PingAsync()
        {
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.Ping);
            Method method = Method.Get;
            var request = ApiRequestAndResponseHelper.BuildRequest<object?>(
                url,
                method,
                null,
                out var stopwatch);

            // Act
            var response = await ApiRequestAndResponseHelper.ExecuteAsync(restClient, request, stopwatch);
            return response;
        }

        public async Task<RestResponse> PrePopulateEntityInfoByIDAsync<T>(T payload) where T : class
        {
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.PrePopulateEntityInfoByID);
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

        public async Task<RestResponse> ProcessRefundAndBilcoCancellationAsync<T>(T payload) where T : class
        {
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.ProcessRefundAndBilcoCancellation);
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

        public async Task<RestResponse> SendInternalEmailsAsync<T>(T payload) where T : class
        {
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.SendInternalEmails);
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

        public async Task<RestResponse> StoreOTPAsync<T>(T payload) where T : class
        {
            // Arrange
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.StoreOTP);
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

        public async Task<RestResponse> VerifyAndUpdateOTPAsync<T>(T payload) where T : class
        {
            // Arrange            
            var url = PolicyAPIEndPoints.GetEndPoint(EndPoints.VerifyAndUpdateOTP);
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
