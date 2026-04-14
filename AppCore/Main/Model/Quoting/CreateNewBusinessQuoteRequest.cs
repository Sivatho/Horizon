namespace AppCore.Main.Model.Quoting
{
    public class CreateNewBusinessQuoteRequest
    {
        public int schemaCd { get; set; }
        public int partnerCd { get; set; }
        public int planCd { get; set; }
        public string user { get; set; }
        public string effectiveDate { get; set; }
        public int quoteTypeCd { get; set; }
    }
}