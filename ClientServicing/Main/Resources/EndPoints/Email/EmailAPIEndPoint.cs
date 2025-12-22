using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Resources.EndPoints.Email
{
    public class EmailAPIEndPoint
    {
        public enum EndPoints {
            SendInternalEmails
        }
        public static string GetEndPoint(EndPoints endPoint)
        {
            return endPoint switch
            {
                EndPoints.SendInternalEmails => "/api/Email/SendInternalEmails",
                _ => throw new ArgumentOutOfRangeException(nameof(endPoint), endPoint, null)
            };
        }
    }
}
