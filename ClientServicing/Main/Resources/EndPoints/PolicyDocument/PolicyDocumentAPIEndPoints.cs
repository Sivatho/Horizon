using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Resources.EndPoints.PolicyDocument
{
    public class PolicyDocumentAPIEndPoints
    {
        public enum EndPoints
        {
            CheckPolicyDocumentExist,
            RetrievePolicyDocumentDetails,
            RetrievePolicyDocuments,
            UpsertPolicyDocument
        }
        public static string GetEndPoint(EndPoints endPoints) {
            return endPoints switch
            {
                EndPoints.CheckPolicyDocumentExist =>       "/api/PolicyDocument/CheckPolicyDocumentExist",
                EndPoints.RetrievePolicyDocumentDetails =>  "/api/PolicyDocument/RetrievePolicyDocumentDetails",
                EndPoints.RetrievePolicyDocuments =>        "/api/PolicyDocument/RetrievePolicyDocuments",
                EndPoints.UpsertPolicyDocument =>           "/api/PolicyDocument/UpsertPolicyDocument",
                _ => throw new ArgumentOutOfRangeException(nameof(endPoints), endPoints, null)
            };
        }
    }
}
