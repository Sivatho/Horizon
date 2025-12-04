using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClientServicing.Main.IController
{
    public interface IAccountHistory
    {
        Task<RestResponse> policyAccountingHistoryAsync<T>(T policyNo) where T : class;
        Task<RestResponse> policyAccountingHistorySummaryAsync<T>(T payload) where T : class;
        Task<RestResponse> policyCashReceiptAsync<T>(T payload) where T : class;
        Task<RestResponse> GetStatementLineIDAsync<T>(T payload) where T : class;
        Task<RestResponse> CashReceiptInfoUpsertAsync(string accountNumber);
        Task<RestResponse> ManualReceiptInfoUpsertAsync(int policyNo);
    }
}

