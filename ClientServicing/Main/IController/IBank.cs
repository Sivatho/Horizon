using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace ClientServicing.Main.IController
{
    public interface IBank
    {
        Task<RestResponse> FetchBanksAsync<T>(T payload) where T : class;
        Task<RestResponse> ValidateBankAccountAsync<T>(T payload) where T : class;
        Task<RestResponse> ValidateBankAccountQAVSRAsync<T>(T payload) where T : class;
        Task<RestResponse> ValidateAccountNumberUsageLimitAsync(string accountNumber);
        Task<RestResponse> GetBankingDetailHistoryAsync(string policyNo);
    }
}
