using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.Bank;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank
{
    public interface IValidateBankAccountQAVSRValidationMethods
    {
        public void ValidateResponseIsNotNullOrEmpty(ValidateBankAccountQAVSRResponse validateBankAccountQAVSRResponse);
        public void ValidateResponseIsNullOrWhiteSpace(ValidateBankAccountQAVSRResponse validateBankAccountQAVSRResponse);
        public void ValidateValidateBankAccountQAVSRResponsetResponseDataIsNotNullOrEmpty(ValidateBankAccountQAVSRResponse validateBankAccountQAVSRResponse);
    }
}
