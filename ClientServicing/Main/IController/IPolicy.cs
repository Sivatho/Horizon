using RestSharp;

namespace ClientServicing.Main.IController
{
    public interface IPolicy
    {
        public Task<RestResponse> PingAsync();
        public Task<RestResponse> AdvancedPersonSearchAsync<T>(T payload) where T : class;
        public Task<RestResponse> GetCustomerPolicyInfoByEntityNoAsync<T>(T entity) where T : class;
        public Task<RestResponse> PersonSearchAsync<T>(T payload) where T : class;
        public Task<RestResponse> CheckHasProductAsync<T>(T payload) where T : class;
        public Task<RestResponse> PrePopulateEntityInfoByIDAsync<T>(T payload) where T : class;
        public Task<RestResponse> GetPolicyAndMainMemberDetailsByPolicyNumberAsync<T>(T entity) where T : class;
        public Task<RestResponse> EntityInfoUpsertAsync<T>(T payload) where T : class;
        public Task<RestResponse> GetPossibleMainMembersAsync<T>(T payload) where T : class;
        public Task<RestResponse> GetMainMemChangeHistoryAsync<T>(T payload) where T : class;
        public Task<RestResponse> ChangeMainMemberUpsertAsync<T>(T payload) where T : class;
        public Task<RestResponse> GetBenefitCoverScreenHospitalAsync<T>(T payload) where T : class;
        public Task<RestResponse> GetBenefitCoverScreenWealthAsync<T>(T payload) where T : class;
        public Task<RestResponse> CheckRestartEligibilityAsync<T>(T payload) where T : class;
        public Task<RestResponse> StoreOTPAsync<T>(T payload) where T : class;
        public Task<RestResponse> SendInternalEmailsAsync<T>(T payload) where T : class;
        public Task<RestResponse> VerifyAndUpdateOTPAsync<T>(T payload) where T : class;
        public Task<RestResponse> CheckRefundAvailabilityAsync<T>(T payload) where T : class;
        public Task<RestResponse> ChangePolicyDOCAsync<T>(T payload) where T : class;
        public Task<RestResponse> FetchPolicyStatusAsync<T>(T payload) where T : class;
        public Task<RestResponse> InsertPolicyNoteAsync<T>(T payload) where T : class;
        public Task<RestResponse> CheckWaitingPeriodAsync<T>(T payload) where T : class;
        public Task<RestResponse> GetUnmentPremiumAsync(int policyNo);
        public Task<RestResponse> ProcessRefundAndBilcoCancellationAsync<T>(T payload) where T : class;
        public Task<RestResponse> CheckPolicyIfMainMemberOnlyAsync(int policyNo);
        public Task<RestResponse> GetPolicyProductLineAsync<T>(T payload) where T : class;
        public Task<RestResponse> ReversePolicyStatusAsync<T>(T payload) where T : class;
    }
}
