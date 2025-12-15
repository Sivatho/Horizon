using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Resources.EndPoints.BeneficiaryDetails
{
    public class BeneficiaryDetailsAPIEndPoints
    {
        public enum EndPoints
        {
            policyBeneficiaryDetails,
            PolicyEntityInfoUpsert,
            GetAndCachePolicyBeneficiaryDetails,
            UpdatePolicyBeneficiaryCache,
            GetCachedBeneficiaryList,
            SaveUpdatedBeneficiaries
        }
        public static string GetEndPoint(EndPoints endPoint)
        {
            return endPoint switch
            {
                EndPoints.policyBeneficiaryDetails => "/api/BeneficiaryDetails/policyBeneficiaryDetails",
                EndPoints.PolicyEntityInfoUpsert => "/api/BeneficiaryDetails/PolicyEntityInfoUpsert",
                EndPoints.GetAndCachePolicyBeneficiaryDetails => "/api/BeneficiaryDetails/GetAndCachePolicyBeneficiaryDetails",
                EndPoints.UpdatePolicyBeneficiaryCache => "/api/BeneficiaryDetails/UpdatePolicyBeneficiaryCache",
                EndPoints.GetCachedBeneficiaryList => "/api/BeneficiaryDetails/GetCachedBeneficiaryList",
                EndPoints.SaveUpdatedBeneficiaries => "/api/BeneficiaryDetails/SaveUpdatedBeneficiaries",
                _ => throw new ArgumentOutOfRangeException(nameof(endPoint), endPoint, null)
            };
        }
    }
}
