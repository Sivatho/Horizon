using ClientServicing.Main.Models.BeneficiaryDetails;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.BeneficiaryDetails
{
    public interface IPolicyBeneficiaryDetailsValidationMethods 
    {
        public void ValidateResponseIsNotNullOrEmpty(PolicyBeneficiaryDetailsResponse policyBeneficiaryDetailsResponse);        
    }
}
