using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy
{
    public class PersonSearchValidationMethods : AbstractValidationMethods, IPersonSearchValidationMethods
    {
        public void ValidatePersonSearchRequestDataIsNotNullOrEmpty(PersonSearchRequest personSearchRequest)
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(personSearchRequest.policyNo,       Is.Not.Null.Or.Empty,   "PersonSearchRequest: <policyNo> Should not be null or empty");
                Assert.That(personSearchRequest.legPolNo,       Is.Not.Null.Or.Empty,   "PersonSearchRequest: <legPolNo> Should not be null or empty");
                Assert.That(personSearchRequest.clientEntityNo, Is.Not.Null.Or.Empty,   "PersonSearchRequest: <clientEntityNo> Should not be null or empty");
                Assert.That(personSearchRequest.legalRefNo,     Is.Not.Null.Or.Empty,   "PersonSearchRequest: <legalRefNo> Should not be null or empty");
                Assert.That(personSearchRequest.claimNo,        Is.Not.Null.Or.Empty,   "PersonSearchRequest: <claimNo> Should not be null or empty");
                Assert.That(personSearchRequest.cellNo,         Is.Not.Null.Or.Empty,   "PersonSearchRequest: <cellNo> Should not be null or empty");
                Assert.That(personSearchRequest.emailAddress,   Is.Not.Null.Or.Empty,   "PersonSearchRequest: <emailAddress> Should not be null or empty");
                Assert.That(personSearchRequest.fullName,       Is.Not.Null.Or.Empty,   "PersonSearchRequest: <fullName> Should not be null or empty");
                Assert.That(personSearchRequest.inspiratorNo,   Is.Not.Null.Or.Empty,   "PersonSearchRequest: <inspiratorNo> Should not be null or empty");
                Assert.That(personSearchRequest.voucherNo,      Is.Not.Null.Or.Empty,   "PersonSearchRequest: <voucherNo> Should not be null or empty");
                Assert.That(personSearchRequest.partnerCD,      Is.Not.LessThan(0),     "PersonSearchRequest: <partnerCD> Should be a positive integer");
                Assert.That(personSearchRequest.auditToken,     Is.Not.Null.Or.Empty,   "PersonSearchRequest: <auditToken> Should not be null or empty");
            }
            TestContext.Out.WriteLine("Validated: <PersonSearchRequest> is not be null or empty");
        }
        public void ValidatePersonSearchResponseDataIsNotNullOrEmpty(PersonSearchResponse personSearchResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(personSearchResponse.executionOutcome,  Is.Not.Null.Or.Empty, "PersonSearchResponse: <Execution Outcome> Should not be null or empty");
                Assert.That(personSearchResponse.personSearchDetails, Is.Not.Null.Or.Empty, "PersonSearchResponse: <Person Search Details> Should not be null or empty");
            });
            TestContext.Out.WriteLine("Validated: <PersonSearchResponse> is not be null or empty");
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
                    AllowedKinds = new[] { JsonValueKind.Array },
                    NestedRules = new Dictionary<string, JsonValueKind[]>
                    {
                        { "entityID",                    new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "ifaNo",                       new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "entityNo",                    new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "entityName",                  new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "entitySurname",               new[] { JsonValueKind.Null, JsonValueKind.String } }, 
                        { "entityDOB",                   new[] { JsonValueKind.Null, JsonValueKind.String } }, 
                        { "legalRefNo",                  new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "legalRefNoType",              new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "citizenshipCD",               new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "alpha3Code",                  new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "citizenship",                 new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "emailAddress",                new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "cellphoneNumber",             new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "physicalAddress1",            new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "legacyPolicyNo",              new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "policyNo",                    new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "roleCd",                      new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "status",                      new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "statusCD",                    new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "planTypeDescr",               new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "statusDate",                  new[] { JsonValueKind.Null, JsonValueKind.String } }, 
                        { "dateOfCommencement",          new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "premiumAmt",                  new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "salesPerson",                 new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "rewardStatus",                new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "debiCheckStatus",             new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "agency",                      new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "payor",                       new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "payorLegalReferenceNumber",   new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "payorCellphoneNumber",        new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "payorEmailAddress",           new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "beneficiaryName",             new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "paymentTypeCD",               new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "inspiratorNo",                new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "region",                      new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "partnerCD",                   new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "partnerCode",                 new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "schemeCD",                    new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "schemeDesc",                  new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "planCD",                      new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "planDesc",                    new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "channelCD",                   new[] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "channelDesc",                 new[] { JsonValueKind.Null, JsonValueKind.String } },
                        { "entityFullname",              new[] { JsonValueKind.Null, JsonValueKind.String } },
                    }
                }
            };
            using var doc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(doc.RootElement, rules);
            TestContext.Out.WriteLine("Validated: Response Contects and data types are valid");
        }
    }
}
