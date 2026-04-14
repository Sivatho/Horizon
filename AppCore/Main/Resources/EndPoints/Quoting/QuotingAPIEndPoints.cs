namespace AppCore.Main.Resources.EndPoints.Quating
{
    public class QuotingAPIEndPoints
    {
        public enum EndPoints
        {
            CreateNewBusinesssQuoate,
            GetQuoteRuleset
        }
        public static string GetEndPoint(EndPoints endPoint)
        {
            return endPoint switch
            {
                EndPoints.CreateNewBusinesssQuoate => "/api/Quating/CreateNewBusinessQuote",
                EndPoints.GetQuoteRuleset => "/api/Quating/GetQuoteRuleset",
                _ => throw new ArgumentOutOfRangeException(nameof(endPoint), endPoint, null)
            };
        }
    }
}