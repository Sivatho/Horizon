using ClientServicing.Main.Models.AdjustementToBillings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.AdjustementToBillings
{
    public class GetAdjustmentToBillingsHistoryResponse
    {

      
        public bool succeeded  { get; set; }
        public string message  { get; set; }
        public string errors  { get; set; }
        public List<GetAdjustmentToBillingsHistoryRequest>[] data { get; set; }




    
}
}
