using ClientServicing.Main.Models.Policy;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy
{
    public interface ICheckHasProductValidationMethods
    {
        public void ValidateCheckHasProductRequestDataIsNotNullOrEmptyOrLessThanZero(CheckHasProductRequest checkHasProductRequest);
        public void ValidateCheckHasProductResponseDataIsNotNullOrEmpty(CheckHasProductResponse checkHasProductResponse);
    }
}
