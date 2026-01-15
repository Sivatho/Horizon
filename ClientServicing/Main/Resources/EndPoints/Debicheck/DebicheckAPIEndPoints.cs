using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Resources.EndPoints.Debicheck
{

    public class DebicheckAPIEndPoints

    {
        public enum EndPoints
        {

            CheckStatus,
            MandatesRequest,
            DetermineMandateType,
            DebicheckRetryCheckStatus,
            DebicheckRequestRetry


        }
        public static string GetEndPoint(EndPoints endPoint)

        {
            return endPoint switch
            {
                EndPoints.CheckStatus => "/api/Debicheck/CheckStatus",
                EndPoints.MandatesRequest => "/api/Debicheck/MandatesRequest",
                EndPoints.DetermineMandateType => "/api/Debicheck/DetermineMandateType",
                EndPoints.DebicheckRetryCheckStatus => "/api/Debicheck/DebicheckRetryCheckStatus",
                EndPoints.DebicheckRequestRetry => "/api/Debicheck/DebicheckRequestRetry",
               _ => throw new ArgumentOutOfRangeException(nameof(endPoint), endPoint, null)
            };
        }
    }
}
