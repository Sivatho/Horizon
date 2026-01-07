using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Policy
{
    public class CheckHasProductRequest
    {
        public string legalRefNo { get; set; }
        public int partnerCd { get; set; }
        public int entityNo { get; set; }
        public int schemeCD { get; set; }
        public int planCD { get; set; }
        public string schemeDescr { get; set; }
    }
}
