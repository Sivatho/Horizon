using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.Bank;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank
{
    public interface IValidateBankAccountResponseValidationMethods
    {
        public void ValidateResponseIsNotNullOrEmpty(ValidateBankAccountResponse validateBankAccountResponse);
        public void ValidateResponseIsNullOrWhiteSpace(ValidateBankAccountResponse validateBankAccountResponse);
        public void ValidateValidateBankAccountResponseDataIsNotNullOrEmpty(ValidateBankAccountResponse validateBankAccountResponse);
    }
}
