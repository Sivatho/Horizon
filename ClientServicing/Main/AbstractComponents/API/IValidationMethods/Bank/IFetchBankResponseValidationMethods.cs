using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.Bank;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank
{
    public interface IFetchBankResponseValidationMethods
    {
        public void ValidateResponseIsNotNullOrEmpty(FetchBanksResponse fetchBanksResponse);
        public void ValidateResponseIsNullOrWhiteSpace(FetchBanksResponse fetchBanksResponse);
        public void ValidateFetchBankDataIsNotNullOrEmpty(FetchBanksResponse fetchBanksResponse);
    }
}
