using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Policy
{
    public class ChangePolicyDocResponse
    {
        public ExecutionOutcome  executionOutcome { get; set; }
        public ChangePolicyDoc[]? data { get; set; }
    }
    public class ChangePolicyDoc {
        public int policy_History_ID { get; set; }
        public int policy_NO { get; set; }
        public int? parentPolicyNO { get; set; }
        public string date_of_Commencement { get; set; }
        public int plan_CD { get; set; }
        public int status_CD { get; set; }
        public string status_Date { get; set; }
        public string eff_From { get; set; }
        public string eff_To { get; set; }
        public string exp_Date { get; set; }
        public int? deduct_Day { get; set; }
        public string? bankAccountID { get; set; }
        public int? paymentTypeCD { get; set; }
        public int paymentFreqCD { get; set; }
        public string payAtNo { get; set; }        
        public string aud_Create_User { get; set; }
        public string aud_Create_Date { get; set; }
        public string aud_Mod_User { get; set; }
        public string aud_Mod_Date { get; set; }
        public bool earlyTracking { get; set; }
    }
}
