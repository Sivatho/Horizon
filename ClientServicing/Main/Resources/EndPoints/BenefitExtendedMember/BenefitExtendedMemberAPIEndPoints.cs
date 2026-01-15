using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Resources.EndPoints.BenefitExtendedMember
{
    public class BenefitExtendedMemberAPIEndPoints
    {
        public enum EndPoints
        {
            policyBenefitExtendedMember,
            UpdateBenefitExtendedMember
        }

        public static string GetEndPoint(EndPoints endPoint)
        {
            return endPoint switch
            {
                EndPoints.policyBenefitExtendedMember => "/api/BenefitExtendedMember/policyBenefitExtendedMember",
                EndPoints.UpdateBenefitExtendedMember => "/api/BenefitExtendedMember/UpdateBenefitExtendedMember",
                _ => throw new ArgumentOutOfRangeException(nameof(endPoint), endPoint, null)
            };
        }

    }
}
