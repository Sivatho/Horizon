using ClientServicing.Main.Models.Bank;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank
{
    public interface ICanChangeBankAccountIdValidationMethods
    {
        public void ValidateCanChangeBankAccountRequestDataIsNotNullOrEmpty(BankAccountRequest canChangeBankAccountRequest);
        public void ValidateCanChangeBankAccountResponseDataIsNotNullAndIsTrueOrFalseAndTypeOfString(CanChangeBankAccountResponse canChangeBankAccountResponse);
    }
}
