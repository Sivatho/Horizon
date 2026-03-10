using ClientServicing.Main.Models.BeneficiaryDetails;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.BeneficiaryDetails
{
    public interface IGetInsuredWithBenefitValidationMethods
    {
        public void ValidateGetInsuredWithBenefitRequestIsNotNullOrEmpty(GetInsuredWithBenefitRequest getInsuredWithBenefitRequest);
        public void ValidateGetInsuredWithBenefitDataIsNotNull_And_IsTrueOrFalse_And_TypeOfString_And_IsNotLessThanOrEqualTo0(GetInsuredWithBenefitResponse getInsuredWithBenefit);
    }
}
