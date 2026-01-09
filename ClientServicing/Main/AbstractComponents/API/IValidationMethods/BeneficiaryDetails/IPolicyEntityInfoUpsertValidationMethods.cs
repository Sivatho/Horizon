using ClientServicing.Main.Models.BeneficiaryDetails;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.BeneficiaryDetails
{
    internal interface IPolicyEntityInfoUpsertValidationMethods
    {
        public void ValidatePolicyEntityInfoUpsertResponseDataIsNotNullOrEmpty(PolicyEntityInfoUpsertResponse policyEntityInfoUpsertResponse);
    }
}
