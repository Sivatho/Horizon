using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Policy
{
    public class ChangeMainMemberUpsertRequest
    {
        public int entityNo { get; set; }
        public int policyNo { get; set; }
        public string userName { get; set; }
        public string auditToken { get; set; }
    }
}
