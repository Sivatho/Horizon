using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.CCMEvent
{
    public class TriggerEventResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public TriggerEvent data { get; set; }
    }
    public class TriggerEvent {
        public string token { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
    }
}
