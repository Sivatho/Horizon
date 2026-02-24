namespace ClientServicing.Main.Models.CCMEvent
{
    public class GetEventDetailConstructBPERequest
    {
        public int policyNo { get; set; }
        public int eventTypeCd { get; set; }
        public DateTime effectiveDate { get; set; }
    }
}
