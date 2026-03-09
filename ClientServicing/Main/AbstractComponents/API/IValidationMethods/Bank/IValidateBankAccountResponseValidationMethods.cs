using ClientServicing.Main.Models.Bank;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank
{
    public interface IValidateBankAccountResponseValidationMethods
    {
        void ValidateValidateBankAccountRequestDataIsNotNullOrEmpty(ValidateBankAccountRequest validateBankAccountRequest);
        public void ValidateValidateBankAccountResponseDataIsNotNullOrEmpty(ValidateBankAccountResponse validateBankAccountResponse);
    }
}

