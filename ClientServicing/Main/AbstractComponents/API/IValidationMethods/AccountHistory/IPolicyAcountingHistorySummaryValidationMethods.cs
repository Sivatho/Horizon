using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.AccountHistory;

namespace ClientServicing.Main.AbstractComponents.API.IValidationMethods.AccountHistory
{
    public interface IPolicyAcountingHistorySummaryValidationMethods    
    {
        public void ValidatePolicyAccountHistorySummaryRequestPayload(PolicyAccountHistorySummaryRequest accountHistorySummaryRequest);
        public void ValidatePolicyAccountHistorySummaryResponsePayload(PolicyAccountHistorySummaryResponse accountHistorySummaryResponse);
    }
}
