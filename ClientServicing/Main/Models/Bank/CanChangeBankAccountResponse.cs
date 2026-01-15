namespace ClientServicing.Main.Models.Bank
{
    public class CanChangeBankAccountResponse
    {
        public bool succeeded { get; set; }
        public string message { get; set; }
        public string errors { get; set; }
        public CompleteStatusMessages data { get; set; }
    }
    public class CompleteStatusMessages
    {
        public bool proCompleted { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
    }
}
