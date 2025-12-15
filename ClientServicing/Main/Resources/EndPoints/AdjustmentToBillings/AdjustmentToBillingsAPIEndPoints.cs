using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Resources.EndPoints.AdjustmentToBillings
{
    public class AdjustmentToBillingsAPIEndPoints
    {
        public enum EndPoints
        {
            AddAdjustmentToBillings,
            CancelAdjustmentToBillings,
            GetAdjustedPeriods,
            GetAdjustmentToBillingsHistory,
            GetOutstandingPolicyPremiums
        }
        public static string GetEndPoint(EndPoints endPoint)
        {
            return endPoint switch
            {
                EndPoints.AddAdjustmentToBillings => "/api/AdjustmentToBillings/AddAdjustmentToBillings",
                EndPoints.CancelAdjustmentToBillings => "/api/AdjustmentToBillings/CancelAdjustmentToBillings",
                EndPoints.GetAdjustedPeriods => "/api/AdjustmentToBillings/GetAdjustedPeriods",
                EndPoints.GetAdjustmentToBillingsHistory => "/api/AdjustmentToBillings/GetAdjustmentToBillingsHistory",
                EndPoints.GetOutstandingPolicyPremiums => "/api/AdjustmentToBillings/GetOutstandingPolicyPremiums",
                _ => throw new ArgumentOutOfRangeException(nameof(endPoint), endPoint, null)
            };
        }
    }
}
