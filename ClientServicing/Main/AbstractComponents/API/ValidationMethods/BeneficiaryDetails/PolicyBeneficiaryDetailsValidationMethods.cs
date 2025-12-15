using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.BeneficiaryDetails;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.General;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.BeneficiaryDetails
{
    public class PolicyBeneficiaryDetailsValidationMethods : AbstractValidationMethods, IPolicyBeneficiaryDetailsValidationMethods
    {
        public void ValidatePolicyBeneficiaryDetailsDataIsNotNullOrEmpty(PolicyBeneficiaryDetailsResponse policyBeneficiaryDetailsResponse)
        {
            throw new NotImplementedException();
        }

        public void ValidateResponseIsNotNullOrEmpty(PolicyBeneficiaryDetailsResponse policyBeneficiaryDetailsResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(policyBeneficiaryDetailsResponse, Is.Not.Null.Or.Not.Empty, "Response is Null or Empty.");
                Assert.That(policyBeneficiaryDetailsResponse.executionOutcome, Is.Not.Null.Or.Not.Empty, "Execution Outcome is Null or Empty.");
                Assert.That(policyBeneficiaryDetailsResponse.data, Is.Not.Null.Or.Not.Empty, "Data property is Null or Empty.");
                Assert.That(policyBeneficiaryDetailsResponse.data.beneficiaryDetailsItems, Is.Not.Null.Or.Not.Empty, "BeneficiaryDetailsItems property is Null or Empty.");
            });
            TestContext.WriteLine("Validated: Response is not null or Empty.");            
        }

        public void ValidateResponseIsNullOrWhiteSpace(PolicyBeneficiaryDetailsResponse policyBeneficiaryDetailsResponse)
        {
            throw new NotImplementedException();
        }

        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            var rules = new List<JsonValidationRule> {
                new JsonValidationRule {
                    PropertyName = "succeeded",
                    AllowedKinds = new[] {
                        JsonValueKind.True, JsonValueKind.False
                    }
                },
                new JsonValidationRule {
                    PropertyName = "message",
                    AllowedKinds = new[] {
                        JsonValueKind.String, JsonValueKind.Null
                    }
                },
                new JsonValidationRule {
                    PropertyName = "errors",
                    AllowedKinds = new[] {
                        JsonValueKind.String, JsonValueKind.Null
                    }
                },
                new JsonValidationRule {
                    PropertyName = "data",
                    AllowedKinds =  new[] { JsonValueKind.Object },
                    NestedRules = new Dictionary<string, JsonValueKind[]> {
                        { "totalAllocated",    new [] { JsonValueKind.Number } },
                        { "policyNo",          new [] { JsonValueKind.Number } },
                        { "auditToken",        new [] { JsonValueKind.Number , JsonValueKind.Null} },
                        { "beneficiaryDetailsItems", new [] { JsonValueKind.Array} }
                    },
                    ArrayItemRules = new Dictionary<string,JsonValueKind[]> {
                        { "rownumber",         new[] { JsonValueKind.Number } },
                        { "entityNo",          new[] { JsonValueKind.Number } },
                        { "policyNo",          new[] { JsonValueKind.Number } },
                        { "firstName",         new[] { JsonValueKind.Number } },
                        { "surname",           new[] { JsonValueKind.Number } },
                        { "titleCd",           new[] { JsonValueKind.Number } },
                        { "titleDescr",        new[] { JsonValueKind.Number } },
                        { "statusCd",          new[] { JsonValueKind.Number } },
                        { "statusDescr",       new[] { JsonValueKind.Number } },
                        { "entityRelationId",  new[] { JsonValueKind.Number } },
                        { "relationCd",        new[] { JsonValueKind.Number } },
                        { "relationDescr",     new[] { JsonValueKind.Number } },
                        { "legalReferenceNumber",   new[] { JsonValueKind.Number } },
                        { "legalReferenceNumberMasked", new[] { JsonValueKind.Number } },
                        { "legalReferenceNumberTypeCd", new[] { JsonValueKind.Number } },
                        { "percAllocation",    new[] { JsonValueKind.Number } },
                        { "dateOfBirth",       new[] { JsonValueKind.Number } },
                        { "status",            new[] { JsonValueKind.Number } },
                        { "physicalAddress",   new[] { JsonValueKind.Number } },
                        { "addressLine2",      new[] { JsonValueKind.Number } },
                        { "suburb",            new[] { JsonValueKind.Number } },
                        { "addressCity",       new[] { JsonValueKind.Number } },
                        { "addressPostCode",   new[] { JsonValueKind.Number } },
                        { "cellNumber",        new[] { JsonValueKind.Number } },
                        { "cellNumberMasked",  new[] { JsonValueKind.Number } },
                        { "homeNumber",        new[] { JsonValueKind.Number } },
                        { "workNumber",        new[] { JsonValueKind.Number } },
                        { "emailAddress",      new[] { JsonValueKind.Number } },
                        { "fullname",          new[] { JsonValueKind.Number } },
                        { "totalPercentageAvailable", new[] { JsonValueKind.Number } },
                        { "role",              new[] { JsonValueKind.Number } },
                        { "genderCd",          new[] { JsonValueKind.Number } },
                        { "auditToken",        new[] { JsonValueKind.Number } },
                        { "bankAccNo",         new[] { JsonValueKind.Number } }
                    }
                }
            };
            using var jsonDoc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(jsonDoc.RootElement, rules);
            TestContext.WriteLine("Validated: Response Property Names are valid and Data Types are valid.");
        }
    }
}
