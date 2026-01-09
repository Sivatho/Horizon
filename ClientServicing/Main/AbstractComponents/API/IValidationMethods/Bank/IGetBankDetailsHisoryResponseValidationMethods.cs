using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.Response
{
    public interface IGetBankDetailsHisoryResponseValidationMethods
    {
        public void ValidateBankDetailHistoryResponseDataIsNotNullOrEmpty(GetBankDetailHistoryResponse getBankDetailHistoryResponse);
    }
}
