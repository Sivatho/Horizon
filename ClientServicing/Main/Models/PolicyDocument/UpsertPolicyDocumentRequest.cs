using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.PolicyDocument
{
    public class UpsertPolicyDocumentRequest
    {
        public string sourceSystem { get; set; }
        public int policyDocumentNo { get; set; }
        public int policyNo { get; set; }
        public int documentId { get; set; }
        public int processCd { get; set; }
        public int statusId { get; set; }
        public DateTime statusDate { get; set; }
        public DateTime effFrom { get; set; }
        public DateTime effTo { get; set; }
        public string audCreateUser { get; set; }
        public DateTime audCreateDate { get; set; }
        public string audModUser { get; set; }
        public DateTime audModDate { get; set; }
        public int ? documentTypeCD { get; set; }
        public FileDetails fileDetails { get; set; }
    }
    public class FileDetails {
        public string fileId { get; set; }
        public string referenceId { get; set; }
        public string fileName { get; set; }
        public DateTime fileCreatedDate { get; set; }
        public string base64FileContents { get; set; }
        public string fileExtension { get; set; }
        public string[] tags { get; set; }
        public Dictionary<string, string>[] properties { get; set; }

    }
}
