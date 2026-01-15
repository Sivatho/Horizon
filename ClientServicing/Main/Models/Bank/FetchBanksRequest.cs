namespace ClientServicing.Main.Models.Bank
{
    public class FetchBanksRequest
    {
        public int? bankID { get; set; }
        public string? bankName { get; set; }
        public string? bankShortName { get; set; }
        public int? dispSeq { get; set; }
        public bool? isActive { get; set; }
        public DateTime? lastChanged { get; set; }
        public string? userID { get; set; }
    }
}
