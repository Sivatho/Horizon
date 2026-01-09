using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.PolicyDocument
{
    public class RetrievePolicyDocumentDetailsResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public CheckPolicyDocumentExistRequest checkPolicyDocumentExistRequest { get; set; }
        public int? documentTypeCD { get; set; }
        public string? fileDetails { get; set; }
    }
}
