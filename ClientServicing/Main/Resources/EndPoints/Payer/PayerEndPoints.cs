using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Resources.EndPoints.Payer
{
    public class PayerEndPoints
    {
        public enum EndPoints
        {
            GetPayerDetailsByPolicyNumber,
            UpsertBankingAndPayer
        }
        public static string GetEndPoint(EndPoints endPoints)
        {
            return endPoints switch
            {
                EndPoints.GetPayerDetailsByPolicyNumber => "/api/Payer/GetPayerDetailsByPolicyNumber",
                EndPoints.UpsertBankingAndPayer         => "/api/Payer/UpsertBankingAndPayer",
                _ => throw new ArgumentOutOfRangeException(nameof(endPoints), endPoints, null)
            };
        }
    }
}
