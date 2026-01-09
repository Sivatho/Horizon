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
        public void ValidatFetchBanksResponseDataIsNotNullOrEmpty(FetchBanksResponse fetchBanksResponse);
        public void ValidateFetchBanksRequestDataIsNotNullOrEmptyOrLessThanZero(FetchBanksRequest fetchBanksRequest);
    }
}
