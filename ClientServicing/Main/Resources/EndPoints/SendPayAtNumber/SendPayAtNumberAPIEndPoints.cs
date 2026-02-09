using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Resources.EndPoints.SendPayAtNumber
{
    public class SendPayAtNumberAPIEndPoints
    {
        public enum EndPoints
        {
            send_text_message
        }
        public static string GetEndPoint(EndPoints endPoints)
        {
            return endPoints switch
            {
                EndPoints.send_text_message => "/api/v1.0/SendPayAtNumber/send_text_message",
                _ => throw new ArgumentOutOfRangeException(nameof(endPoints), endPoints, null)
            };
        }
    }
}
