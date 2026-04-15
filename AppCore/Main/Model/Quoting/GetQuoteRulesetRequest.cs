namespace AppCore.Main.Model.Quoting
{
    public class GetQuoteRulesetRequest
    {
        public CreateNewBusinessQuoteRequest quoteRequest { get; set; }
        public int? planCategoryCd { get; set; }
        public int? brokerEntityNo { get; set; }
    }
}