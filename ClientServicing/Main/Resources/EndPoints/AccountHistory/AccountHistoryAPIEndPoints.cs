using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Resources.EndPoints.AccountHistoryAPIEndPoints
{
    public class AccountHistoryAPIEndPoints
    {
        public enum EndPoints
        {
            policyAcountingHistory,
            policyAcountingHistorySummary,
            policyCashReceipt,
            GetStatementLineID,
            CashReceiptInfoUpsert,
            ManualReceiptInfoUpsert
        }
        public static string GetEndPoint(EndPoints endPoint)
        {
            return endPoint switch
            {
                EndPoints.policyAcountingHistory => "/api/AccountingHistory/policyAcountingHistory",
                EndPoints.policyAcountingHistorySummary => "/api/AccountingHistory/policyAcountingHistorySummary",
                EndPoints.policyCashReceipt => "/api/AccountingHistory/policyCashReceipt",
                EndPoints.GetStatementLineID => "/api/AccountingHistory/GetStatementLineID",
                EndPoints.CashReceiptInfoUpsert => "/api/AccountingHistory/CashReceiptInfoUpsert",
                EndPoints.ManualReceiptInfoUpsert => "/api/AccountingHistory/ManualReceiptInfoUpsert",
                _ => throw new ArgumentOutOfRangeException(nameof(endPoint), endPoint, null)
            };
        }
    }
}
