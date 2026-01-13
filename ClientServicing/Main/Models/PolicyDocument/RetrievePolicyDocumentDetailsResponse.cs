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
        public CheckPolicyDocumentExistRequest data { get; set; }
        public int? documentTypeCD { get; set; }
        public RetrievePolicyDocumentFileDetails? fileDetails { get; set; }
    }
    public class RetrievePolicyDocumentFileDetails 
    {
        public string? fileId { get; set; }
        public string? referenceId { get; set; }
        public string? fileName { get; set; }
        public DateTime? fileCreatedDate { get; set; }
        public string? base64FileContents { get; set; }
        public string? fileExtension { get; set; }
        public string[]? tags { get; set; }
        public Dictionary<string, string>[]? properties { get; set; }
    }
}
