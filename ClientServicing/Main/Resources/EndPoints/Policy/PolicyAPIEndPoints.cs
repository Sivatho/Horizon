namespace ClientServicing.Main.Resources.EndPoints.Policy
{
    public class PolicyAPIEndPoints
    {
        public enum EndPoints
        {
            Ping,
            AdvancedPersonSearch,
            GetCustomerPolicyInfoByEntityNo,
            PersonSearch,
            CheckHasProduct,
            PrePopulateEntityInfoByID,
            GetPolicyAndMainMemberDetailsByPolicyNumber,
            EntityInfoUpsert,
            GetPossibleMainMembers,
            GetMainMemChangeHistory,
            ChangeMainMemberUpsert,
            GetBenefitCoverScreenHospital,
            GetBenefitCoverScreenWealth,
            CheckRestartEligibility,
            StoreOTP,
            SendInternalEmails,
            VerifyAndUpdateOTP,
            CheckRefundAvailability,
            ChangePolicyDOC,
            FetchPolicyStatus,
            InsertPolicyNote,
            CheckWaitingPeriod,
            GetUnmentPremium,
            ProcessRefundAndBilcoCancellation,
            CheckPolicyIfMainMemberOnly,
            GetPolicyProductLine,
            ReversePolicyStatus
        }
        public static string GetEndPoint(EndPoints endPoints)
        {
            return endPoints switch
            {
                EndPoints.Ping                                          => "/api/Policy/Ping",
                EndPoints.AdvancedPersonSearch                          => "/api/Policy/advancedPersonSearch",
                EndPoints.GetCustomerPolicyInfoByEntityNo               => "/api/Policy/GetCustomerPolicyInfoByEntityNo",
                EndPoints.PersonSearch                                  => "/api/Policy/personSearch",
                EndPoints.CheckHasProduct                               => "/api/Policy/CheckHasProduct",
                EndPoints.PrePopulateEntityInfoByID                     => "/api/Policy/PrePopulateEntityInfoByID",
                EndPoints.GetPolicyAndMainMemberDetailsByPolicyNumber   => "/api/Policy/GetPolicyAndMainMemberDetailsByPolicyNumber",
                EndPoints.EntityInfoUpsert                              => "/api/Policy/EntityInfoUpsert",
                EndPoints.GetPossibleMainMembers                        => "/api/Policy/GetPossibleMainMembers",
                EndPoints.GetMainMemChangeHistory                       => "/api/Policy/GetMainMemChangeHistory",
                EndPoints.ChangeMainMemberUpsert                        => "/api/Policy/ChangeMainMemberUpsert",
                EndPoints.GetBenefitCoverScreenHospital                 => "/api/Policy/GetBenefitCoverScreenHospital",
                EndPoints.GetBenefitCoverScreenWealth                   => "/api/Policy/GetBenefitCoverScreenWealth",
                EndPoints.CheckRestartEligibility                       => "/api/Policy/CheckRestartEligibility",
                EndPoints.StoreOTP                                      => "/api/Policy/StoreOTP",
                EndPoints.SendInternalEmails                            => "/api/Policy/SendInternalEmails",
                EndPoints.VerifyAndUpdateOTP                            => "/api/Policy/VerifyAndUpdateOTP",
                EndPoints.CheckRefundAvailability                       => "/api/Policy/CheckRefundAvailability",
                EndPoints.ChangePolicyDOC                               => "/api/Policy/ChangePolicyDOC",
                EndPoints.FetchPolicyStatus                             => "/api/Policy/FetchPolicyStatus",
                EndPoints.InsertPolicyNote                              => "/api/Policy/InsertPolicyNote",
                EndPoints.CheckWaitingPeriod                            => "/api/Policy/CheckWaitingPeriod",
                EndPoints.GetUnmentPremium                              => "/api/Policy/GetUnmentPremium/{PolicyNo}",
                EndPoints.ProcessRefundAndBilcoCancellation             => "/api/Policy/ProcessRefundAndBilcoCancellation",
                EndPoints.CheckPolicyIfMainMemberOnly                   => "/api/Policy/CheckPolicyIfMainMemberOnly/{policyNo}",
                EndPoints.GetPolicyProductLine                          => "/api/Policy/GetPolicyProductLine",
                EndPoints.ReversePolicyStatus                           => "/api/Policy/ReversePolicyStatus",
                _ => throw new ArgumentOutOfRangeException(nameof(endPoints), endPoints, null)
            };
        }
    }
}
