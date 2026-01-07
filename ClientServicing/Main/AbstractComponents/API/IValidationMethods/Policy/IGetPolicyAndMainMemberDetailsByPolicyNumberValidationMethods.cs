using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.Policy;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy
{
    public interface IGetPolicyAndMainMemberDetailsByPolicyNumberValidationMethods
    {
        public void ValidateGetPolicyAndMainMemberDetailsByPolicyNumberRequestDataIsNotNullOrEmpty(PolicyBeneficiaryDetailsRequest getPolicyAndMainMemberDetailsByPolicyNumberRequest);
        public void ValidateGetPolicyAndMainMemberDetailsByPolicyNumberResponseDataIsNotNullOrEmpty(GetPolicyAndMainMemberDetailsByPolicyNumberResponse getPolicyAndMainMemberDetailsByPolicyNumberResponse);
    }
}
