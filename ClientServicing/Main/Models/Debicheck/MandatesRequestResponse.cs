using ClientServicing.Main.Models.General;
using System.Collections.Generic;

namespace ClientServicing.Main.Models.Debicheck
{
    public class MandatesRequestResponse
    {
        public bool success { get; set; }
        public bool? diderror { get; set; }
        public ExecutionOutcome? responseMessage { get; set; }
        public List<MandatesRequest>? result { get; set; }
    }
}