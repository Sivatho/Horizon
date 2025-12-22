using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Resources.EndPoints.GSD
{
    public class GSDAPIEndPoint
    {
        public enum EndPoints
        {
            EmployeeEnquiry,
            AffordabilityEnquiry
        }
        public static string GetEndPoint(EndPoints endPoints) {
            return endPoints switch {
                EndPoints.EmployeeEnquiry => "/api/Gsd/EmployeeEnquiry",
                EndPoints.AffordabilityEnquiry => "api/Gsd/AffordabilityEnquiry",
                _ => throw new ArgumentOutOfRangeException(nameof(endPoints), endPoints, null)
            };
        }
    }
}
