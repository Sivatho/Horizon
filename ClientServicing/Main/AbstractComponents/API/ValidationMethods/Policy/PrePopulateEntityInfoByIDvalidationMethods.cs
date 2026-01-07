using System.Net.Mail;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy
{
    public class PrePopulateEntityInfoByIDvalidationMethods : AbstractValidationMethods, IPrePopulateEntityInfoByIDvalidationMethods
    {
        public void ValidatePrePopulateEntityInfoByIDRequestDataIsNotNullOrEmpty(CheckHasProductRequest prePopulateEntityInfoByIDRequest)
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(prePopulateEntityInfoByIDRequest.legalRefNo, Is.Not.Null.Or.Empty, "PrePopulateEntityInfoByIDRequest: <legalRefNo> Should not be null or empty");
                Assert.That(prePopulateEntityInfoByIDRequest.partnerCd, Is.Not.LessThan(0), "PrePopulateEntityInfoByIDRequest: <partnerCd> Should be a positive integer");
                Assert.That(prePopulateEntityInfoByIDRequest.entityNo, Is.Not.LessThan(0), "PrePopulateEntityInfoByIDRequest: <entityNo> Should be a positive integer");
                Assert.That(prePopulateEntityInfoByIDRequest.schemeCD, Is.Not.LessThan(0), "PrePopulateEntityInfoByIDRequest: <schemeCD> Should be a positive integer");
                Assert.That(prePopulateEntityInfoByIDRequest.planCD, Is.Not.LessThan(0), "PrePopulateEntityInfoByIDRequest: <planCD> Should be a positive integer");
                Assert.That(prePopulateEntityInfoByIDRequest.schemeDescr, Is.Not.Null.Or.Empty, "PrePopulateEntityInfoByIDRequest: <schemeDescr> Should not be null or empty");
            }
            TestContext.Out.WriteLine("Validated: <PrePopulateEntityInfoByIDRequest> is not be null or empty");
        }

        public void ValidatePrePopulateEntityInfoByIDResponseDataIsNotNullOrEmpty(PrePopulateEntityInfoByIDResponse prePopulateEntityInfoByIDResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(prePopulateEntityInfoByIDResponse.executionOutcome, Is.Not.Null.Or.Empty, "PrePopulateEntityInfoByIDResponse: <ExecutionOutcome> Should not be null and empty");
                Assert.That(prePopulateEntityInfoByIDResponse.data, Is.Not.Null.Or.Empty, "PrePopulateEntityInfoByIDResponse: <EntityInfo> Should not be null or empty");
            });
            TestContext.Out.WriteLine("Validated: <PrePopulateEntityInfoByIDResponse> is not be null or empty");
        }

        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            var rules = new List<JsonValidationRule> {
                new JsonValidationRule {
                    PropertyName = "succeeded",
                    AllowedKinds = new[] { JsonValueKind.False, JsonValueKind.True }
                },
                new JsonValidationRule {
                    PropertyName = "message",
                    AllowedKinds = new[] { JsonValueKind.String, JsonValueKind.Null }
                },
                new JsonValidationRule {
                    PropertyName = "errors",
                    AllowedKinds = new[] { JsonValueKind.String, JsonValueKind.Null }
                },
                new JsonValidationRule {
                    PropertyName = "data",
                    AllowedKinds = new[] { JsonValueKind.Object },
                    NestedRules = new Dictionary<string, JsonValueKind[]>{
                        { "entityNo",               new[] { JsonValueKind.Null,JsonValueKind.Number } },
                        { "titleCd",                new[] { JsonValueKind.Null,JsonValueKind.Number } },
                        { "titleDescr",             new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "firstName",              new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "surname",                new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "legalRefNo",             new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "genderCd",               new[] { JsonValueKind.Null,JsonValueKind.Number } },
                        { "genderDescr",            new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "citizenshipCd",          new[] { JsonValueKind.Null,JsonValueKind.Number } },
                        { "citizenshipDescr",       new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "smokerCd",               new[] { JsonValueKind.Null,JsonValueKind.Number } },
                        { "smokerDescr",            new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "cellNumber",             new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "homeNumber",             new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "faxNumber",              new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "workNumber",             new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "alternateNumber",        new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "whatsappNumber",         new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "emailAddress",           new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "postalAddressLine1",     new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "postalAddressLine2",     new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "postalAddressSuburb",    new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "postalAddressCity",      new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "postalAddressCode",      new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "physicalAddressLine1",   new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "physicalAddressLine2",   new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "physicalAddressSuburb",  new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "physicalAddressCity",    new[] { JsonValueKind.Null,JsonValueKind.String } },
                        { "physicalAddressCode",    new[] { JsonValueKind.Null,JsonValueKind.String } },
                    }
                }
            };
            using var doc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(doc.RootElement, rules);
            TestContext.Out.WriteLine("Validated: Response Contects and data types are valid");
        }
    }
}
