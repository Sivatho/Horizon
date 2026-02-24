namespace ClientServicing.Main.Resources.EndPoints.CCMEvent
{
    public class CCMEventAPIEndPoints
    {
        public enum EndPoints {
            TriggerEvent, GetEventDetailConstructBPE
        }
        public static string GetEndPoint(EndPoints endPoint) {
            return endPoint switch
            {
                EndPoints.TriggerEvent => "api/CCMEvent/TriggerEvent",
                EndPoints.GetEventDetailConstructBPE => "api/CCMEvent/GetEventDetailConstructBPE"
            };
        }
    }
}
