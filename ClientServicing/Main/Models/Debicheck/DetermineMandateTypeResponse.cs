using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Debicheck
{
    public class DetermineMandateTypeResponse
    {
        public bool success { get; set; }
        public SuccessBoolMessageStringDataObjectResult result { get; set; }
    }
}
