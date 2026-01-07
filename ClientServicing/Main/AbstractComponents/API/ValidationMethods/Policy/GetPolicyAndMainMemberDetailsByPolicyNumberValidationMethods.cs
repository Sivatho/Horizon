using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Response;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy
{
    public class GetPolicyAndMainMemberDetailsByPolicyNumberValidationMethods : AbstractValidationMethods, IGetPolicyAndMainMemberDetailsByPolicyNumberValidationMethods
    {
        public void ValidateGetPolicyAndMainMemberDetailsByPolicyNumberRequestDataIsNotNullOrEmpty(PolicyBeneficiaryDetailsRequest getPolicyAndMainMemberDetailsByPolicyNumberRequest)
        {
            Assert.Multiple(() =>
            {
                Assert.That(getPolicyAndMainMemberDetailsByPolicyNumberRequest.policyNo,                Is.Not.LessThan(0).Or.Empty,    "GetPolicyAndMainMemberDetailsByPolicyNumberRequest: <policyNo> Should not be less than 0 or empty");
                Assert.That(getPolicyAndMainMemberDetailsByPolicyNumberRequest.legacyPolicyNumber,      Is.Not.Null.Or.Empty,           "GetPolicyAndMainMemberDetailsByPolicyNumberRequest: <legacyPolicyNumber> Should not null or empty");
            });
            TestContext.Out.WriteLine("Validated: <GetPolicyAndMainMemberDetailsByPolicyNumberRequest> is not null or empty; integers are not less than 0 or empty");
        }
        public void ValidateGetPolicyAndMainMemberDetailsByPolicyNumberResponseDataIsNotNullOrEmpty(GetPolicyAndMainMemberDetailsByPolicyNumberResponse getPolicyAndMainMemberDetailsByPolicyNumberResponse)
        {
            Assert.Multiple(() => {
                Assert.That(getPolicyAndMainMemberDetailsByPolicyNumberResponse.executionOutcome, Is.Not.Null.Or.Empty, "GetPolicyAndMainMemberDetailsByPolicyNumberResponse: <executionOutcome> Should not be null or empty");
                Assert.That(getPolicyAndMainMemberDetailsByPolicyNumberResponse.data, Is.Not.Null.Or.Empty, "GetPolicyAndMainMemberDetailsByPolicyNumberResponse: <data> Should not be null or empty");
            });
        }
        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            var rules = new List<JsonValidationRule> {
                new JsonValidationRule {
                    PropertyName = "succeeded",
                    AllowedKinds = new[] {
                        JsonValueKind.False, JsonValueKind.True
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
                    AllowedKinds = new[] { JsonValueKind.Object },
                    NestedRules = new Dictionary<string, JsonValueKind[]>
                    {
                        { "policy_NO",                      new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "entityNo",                       new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "legacy_Pol_No",                  new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "annualIncrease",                 new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "dateOfCommencement",             new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "reInstatedDate",                 new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "lapsedDate",                     new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "venue",                          new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "salesPerson",                    new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "campaignCode",                   new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "policyFee",                      new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "DateTimecaptureDate",            new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "preferedCommunicationMethod",    new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "masterContract",                 new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "title",                          new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "titleID",                        new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "firstname",                      new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "surname",                        new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "legalRefNo",                     new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "legalNumberType",                new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "DateTimedateOfBirth",            new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "preferredTelTypeCd",             new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "faxNumber",                      new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "homeNumber",                     new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "emailAddress",                   new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "cellNumber",                     new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "workNumber",                     new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "alternateNumber",                new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "whatsappNumber",                 new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "physicalAddress1",               new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "physicalAddress2",               new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "physicalSuburb",                 new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "physicalTown",                   new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "physicalPostalCode",             new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "postalAddress1",                 new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "postalAddress2",                 new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "postalSuburb",                   new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "postalTown",                     new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "postalPostalCode",               new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "genderCD",                       new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "smokerCd",                       new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "smokerDescr",                    new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "lastBillingDate",                new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "lastPaidDate",                   new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "nextBillingDate",                new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "policyPremiumAmount",            new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "premiumCount",                   new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "paymentFrequency",               new[] { JsonValueKind.Null, JsonValueKind.String } }
                    }
                }
            };
            using var doc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(doc.RootElement, rules);
            TestContext.Out.WriteLine("Validated: Response Contects and data types are valid");
        }
    }
}
