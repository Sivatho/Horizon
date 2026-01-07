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
            CheckRestartEligibility
        }
        public static string GetEndPoint(EndPoints endPoints)
        {
            return endPoints switch
            {
                EndPoints.Ping =>                       "/api/Policy/Ping",
                EndPoints.AdvancedPersonSearch =>       "/api/Policy/advancedPersonSearch",
                EndPoints.GetCustomerPolicyInfoByEntityNo => "/api/Policy/GetCustomerPolicyInfoByEntityNo",
                EndPoints.PersonSearch =>               "/api/Policy/personSearch",
                EndPoints.CheckHasProduct =>            "/api/Policy/CheckHasProduct",
                EndPoints.PrePopulateEntityInfoByID =>  "/api/Policy/PrePopulateEntityInfoByID",
                EndPoints.GetPolicyAndMainMemberDetailsByPolicyNumber => "/api/Policy/GetPolicyAndMainMemberDetailsByPolicyNumber",
                EndPoints.EntityInfoUpsert =>           "/api/Policy/EntityInfoUpsert",
                EndPoints.GetPossibleMainMembers =>     "/api/Policy/GetPossibleMainMembers",
                EndPoints.GetMainMemChangeHistory =>    "/api/Policy/GetMainMemChangeHistory",
                EndPoints.ChangeMainMemberUpsert =>     "/api/Policy/ChangeMainMemberUpsert",
                EndPoints.GetBenefitCoverScreenHospital => "/api/Policy/GetBenefitCoverScreenHospital",
                EndPoints.GetBenefitCoverScreenWealth => "/api/Policy/GetBenefitCoverScreenWealth",
                EndPoints.CheckRestartEligibility =>     "/api/Policy/CheckRestartEligibility",
                _ => throw new ArgumentOutOfRangeException(nameof(endPoints), endPoints, null)
            };
        }
    }
}
