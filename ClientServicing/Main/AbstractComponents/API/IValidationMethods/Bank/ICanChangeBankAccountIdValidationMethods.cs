using ClientServicing.Main.Models.Bank;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank
{
    public interface ICanChangeBankAccountIdValidationMethods
    {
        public void ValidateCanChangeBankAccountResponseDataIsNotNullOrEmpty(CanChangeBankAccountResponse canChangeBankAccountResponse);
    }
}
