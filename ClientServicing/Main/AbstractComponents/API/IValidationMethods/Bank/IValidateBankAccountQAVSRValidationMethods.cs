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
        public void ValidateValidateBankAccountQAVSRRequestDataIsNotNullOrEmptyOrLessThanZero(ValidateBankAccountQAVSRRequest validateBankAccountQAVSRRequest);
        public void ValidateValidateBankAccountQAVSRResponsetResponseDataIsNotNullOrEmptyOrLessThanZero(ValidateBankAccountQAVSRResponse validateBankAccountQAVSRResponse);
    }
}
