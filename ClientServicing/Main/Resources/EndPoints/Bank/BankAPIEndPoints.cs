using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Resources.EndPoints.Bank
{
    public class BankAPIEndPoints
    {
        public enum EndPoints
        {
            FetchBank,
            ValidateBankAccount,
            ValidateBankAccountQAVSR,
            ValidateAccountNumberUsageLimit,
            GetBankingDetailHistory,
            CanChangeBankAccount
        }
        public static string GetEndPoint(EndPoints endPoint)
        {
            return endPoint switch
            {
                EndPoints.FetchBank => "/api/Bank/FetchBanks",
                EndPoints.ValidateBankAccount => "/api/Bank/ValidateBankAccount",
                EndPoints.ValidateBankAccountQAVSR => "/api/Bank/ValidateBankAccountQAVSR",
                EndPoints.ValidateAccountNumberUsageLimit => "/api/Bank/ValidateAccountNumberUsageLimit/{accountNumber}",
                EndPoints.GetBankingDetailHistory => "/api/Bank/GetBankDetailHistory/{policyNo}",
                EndPoints.CanChangeBankAccount => "/api/Bank/CanChangeBankAccount/{bankAccountId}",
                _ => throw new ArgumentOutOfRangeException(nameof(endPoint), endPoint, null)
            };
        }
    }
}
