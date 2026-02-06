using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Resources.EndPoints.RemovalFromBillings
{
    public class RemovalFromBillingsAPIEndPoints
    {
        public enum EndPoints
        {
            RemovalFromBillingsHistory,
            CancelRemovalFromBillings,
            RemoveFromBillings
        }
        public static string GetEndPoint(EndPoints endPoints)
        {
            return endPoints switch
            {
                EndPoints.RemovalFromBillingsHistory => "/api/RemovalFromBillings/RemovalFromBillingsHistory",
                EndPoints.CancelRemovalFromBillings =>  "/api/RemovalFromBillings/CancelRemovalFromBillings",
                EndPoints.RemoveFromBillings =>         "/api/RemovalFromBillings/RemoveFromBillings",
                _ => throw new ArgumentOutOfRangeException(nameof(endPoints), endPoints, null)
            };
        }
    }
}
