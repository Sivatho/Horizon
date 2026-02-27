namespace ClientServicing.Main.Models.Debicheck
{
    public class DetermineMandateTypeRequest
    {
        public string? policyNumber { get; set; }
        public int sourceSystemId { get; set; }
        public bool hasBankApp { get; set; }
    }

    public class DetermineMandateTypeRequestData {
        public  List<DetermineMandateTypeRequest> listOfDetermineMandateTypeRequest { get; set; }
    }
}
