using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Resources.EndPoints.CCMEvent
{
    public class CCMEventAPIEndPoints
    {
        public enum EndPoint {
            TriggerEvent, GetEventDetailConstructBPE
        }
        public static string GetEndPoints(EndPoint endPoint) {
            return endPoint switch
            {
                EndPoint.TriggerEvent => "api/CCMEvent/TriggerEvent",
                EndPoint.GetEventDetailConstructBPE => "api/CCMEvent/GetEventDetailConstructBPE"
            };
        }
    }
}
