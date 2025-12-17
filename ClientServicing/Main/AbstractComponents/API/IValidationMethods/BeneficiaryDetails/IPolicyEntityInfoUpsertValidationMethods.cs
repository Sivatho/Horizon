using ClientServicing.Main.Models.BeneficiaryDetails;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.BeneficiaryDetails
{
    internal interface IPolicyEntityInfoUpsertValidationMethods
    {
        public void ValidateResponseIsNotNullOrEmpty(PolicyEntityInfoUpsertResponse policyEntityInfoUpsertResponse);
        //public void ValidateResponseIsNullOrWhiteSpace(PolicyEntityInfoUpsertResponse policyEntityInfoUpsertResponse);
        public void ValidatePolicyEntityInfoUpsertResponseDataIsNotNullOrEmpty(PolicyEntityInfoUpsertResponse policyEntityInfoUpsertResponse);
    }
}
