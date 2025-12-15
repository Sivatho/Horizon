using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.BeneficiaryDetails;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.BeneficiaryDetails
{
    public interface IPolicyBeneficiaryDetailsValidationMethods 
    {
        public void ValidateResponseIsNotNullOrEmpty(PolicyBeneficiaryDetailsResponse policyBeneficiaryDetailsResponse);
        public void ValidateResponseIsNullOrWhiteSpace(PolicyBeneficiaryDetailsResponse policyBeneficiaryDetailsResponse);
        public void ValidatePolicyBeneficiaryDetailsDataIsNotNullOrEmpty(PolicyBeneficiaryDetailsResponse policyBeneficiaryDetailsResponse);
    }
}
