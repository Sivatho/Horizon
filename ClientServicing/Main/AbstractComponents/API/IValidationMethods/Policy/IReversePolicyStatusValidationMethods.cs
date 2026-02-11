using ClientServicing.Main.Models.Policy;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy
{
    public interface IReversePolicyStatusValidationMethods
    {
        public void ValidationReversePolicyStatusRequestIsNotNullOrEmptyAndIsNotLessThanZero(ReversePolicyStatusRequest reversePolicyStatusRequest );
        public void ValidationReversePolicyStatusResponseIsNotNullOrEmpty(CheckHasProductResponse reversePolicyStatusResponse);
    }
}
