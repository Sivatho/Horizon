using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Policy
{
    public class GetMainMemChangeHistoryResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public List<MainMemChangeHistoryDetail> data { get; set; }
    }
    public class MainMemChangeHistoryDetail
    {
        public int? entityNo { get; set; }
        public int entityGenderCD { get; set; }
        public int entityTitleCD { get; set; }
        public string entityName { get; set; }
        public string entitySurname { get; set; }
        public string legalRefNo { get; set; }
        public DateTime effFrom { get; set; }
        public DateTime effTo { get; set; }
        public string modifiedBy { get; set; }
        public DateTime modifiedDate { get; set; }
    }
}
