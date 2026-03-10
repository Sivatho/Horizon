using ClientServicing.Main.Models.BeneficiaryDetails;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.BeneficiaryDetails
{
    public interface IPolicyBeneficiaryDetailsValidationMethods 
    {
        void ValidatePolicyBeneficiaryDetailsRequestIsNotNullOrEmpty_And_GreaterThanOrEqualToZeroOrTypeOfString_And_IsNullOrTypeOfString(PolicyBeneficiaryDetailsRequest policyBeneficiaryDetailsRequest);
        void ValidatePolicyBeneficiaryDetailsResponseIsNotNullOrEmpty_And_IsTrueOrFalse_And_IsNullOrTypeOfString_And_IntergerIsNotLessThan0(PolicyBeneficiaryDetailsResponse policyBeneficiaryDetailsResponse);        
    }
}
