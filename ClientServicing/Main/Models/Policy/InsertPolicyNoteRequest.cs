using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Policy
{
    public class InsertPolicyNoteRequest
    {
        public string testYN { get; set; }
        public int noteId { get; set; }
        public int levelCd { get; set; }
        public int policyNo { get; set; }
        public int benefitId { get; set; }
        public int entityNo { get; set; }
        public string noteText { get; set; }
        public DateTime effDate { get; set; }
        public DateTime expDate { get; set; }
    }
}
