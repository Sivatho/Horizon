using ClientServicing.Main.Models.Bank;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank
{
    public interface ICanChangeBankAccountIdValidationMethods
    {
        public void ValidateResponseIsNotNullOrEmpty(CanChangeBankAccountResponse canChangeBankAccountResponse);
        public void ValidateResponseIsNullOrWhiteSpace(CanChangeBankAccountResponse canChangeBankAccountResponse);
        public void ValidateCanChangeBankAccountResponseDataIsNotNullOrEmpty(CanChangeBankAccountResponse canChangeBankAccountResponse);
    }
}
