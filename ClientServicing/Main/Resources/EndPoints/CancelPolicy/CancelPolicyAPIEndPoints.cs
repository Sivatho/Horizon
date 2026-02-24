namespace ClientServicing.Main.Resources.EndPoints.CancelPolicy
{
    public class CancelPolicyAPIEndPoints
    {
        public enum EndPoints
        {
            UpdateCancelPolicyDetails
        }
        public static string GetEndPoint(EndPoints endPoint)
        {
            return endPoint switch
            {
                EndPoints.UpdateCancelPolicyDetails => "/api/cancelpolicy/update",
                _ => throw new ArgumentOutOfRangeException(nameof(endPoint), endPoint, null)
            };
        }
    }
}
