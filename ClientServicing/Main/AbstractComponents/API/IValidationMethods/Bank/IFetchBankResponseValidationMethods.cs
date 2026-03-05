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
        public void ValidateFetchBanksRequestData_When_IsNotNullAndNotEmpty_And_GreaterOrEqualZero_And_NotEqualToDefaultDateTime(FetchBanksRequest fetchBanksRequest);
        public void ValidateFetchBanksRequestData_When_IsNotNullAndNotEmpty(FetchBanksRequest fetchBanksRequest);
        public void ValidatFetchBanksResponseData_When_IsNotNullAndNotEmpty_And_GreaterOrEqualZero_And_NotEqualToDefaultDateTime(FetchBanksResponse fetchBanksResponse);
    }
}
