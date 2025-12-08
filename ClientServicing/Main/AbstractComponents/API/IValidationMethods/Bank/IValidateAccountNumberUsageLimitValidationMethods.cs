using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.Bank;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank
{
    public interface IValidateAccountNumberUsageLimitValidationMethods
    {
        public void ValidateResponseIsNotNullOrEmpty(ValidateAccountNumberUsageLimitResponse validateAccountNumberUsageLimitResponse);
        public void ValidateResponseIsNullOrWhiteSpace(ValidateAccountNumberUsageLimitResponse validateAccountNumberUsageLimitResponse);
        public void ValidateValidateAccountNumberUsageLimitResponseDataIsNotNullOrEmpty(ValidateAccountNumberUsageLimitResponse validateAccountNumberUsageLimitResponse);
    }
}
