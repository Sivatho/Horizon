using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.PolicyDocument;
using ClientServicing.Main.Models.PolicyDocument;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.PolicyDocument
{
    public class RetrievePolicyDocumentDetailsValidationMethods : AbstractValidationMethods, IRetrievePolicyDocumentDetailsValidationMethods
    {
        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
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
                        { "sourceSystem",       new [] { JsonValueKind.Null, JsonValueKind.String } },
                        { "policyDocumentNo",   new [] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "policyNo",           new [] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "documentId",         new [] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "documentTypeCD",     new [] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "statusId",           new [] { JsonValueKind.Null, JsonValueKind.Number } },
                        { "statusDate",         new [] { JsonValueKind.Null, JsonValueKind.String } },
                        { "effFrom",            new [] { JsonValueKind.Null, JsonValueKind.String } },
                        { "effTo",              new [] { JsonValueKind.Null, JsonValueKind.String } },
                        { "audCreateUser",      new [] { JsonValueKind.Null, JsonValueKind.String } },
                        { "audCreateDate",      new [] { JsonValueKind.Null, JsonValueKind.String } },
                        { "audModUser",         new [] { JsonValueKind.Null, JsonValueKind.String } },
                        { "audModDate",         new [] { JsonValueKind.Null, JsonValueKind.String } },
                        { "fileDetails",        new [] { JsonValueKind.Null, JsonValueKind.String } }
                    }
                }
            };
            using var jsonDoc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(jsonDoc.RootElement, rules);
            TestContext.Out.WriteLine("Validated: Response Property Names are valid and Data Types are valid.");
        }

        public void ValidateRetrievePolicyDocumentDetailsRequestIsNotNullOrEmptyOrLessThanZero(CheckPolicyDocumentExistRequest checkPolicyDocumentExistRequest)
        {
            Assert.Multiple(() => {
                Assert.That(checkPolicyDocumentExistRequest.sourceSystem, Is.Not.Null.Or.Empty,     "RetrievePolicyDocumentDetailsRequest: <sourceSystem> Should not be null or empty");
                Assert.That(checkPolicyDocumentExistRequest.policyDocumentNo, Is.Not.LessThan(0),   "RetrievePolicyDocumentDetailsRequest: <policyDocumentNo> Should be a positive integer");
                Assert.That(checkPolicyDocumentExistRequest.policyNo, Is.Not.LessThan(0),           "RetrievePolicyDocumentDetailsRequest: <policyNo> Should be a positive integer");
                Assert.That(checkPolicyDocumentExistRequest.documentId, Is.Not.LessThan(0),         "RetrievePolicyDocumentDetailsRequest: <documentId> Should be a positive integer");
                Assert.That(checkPolicyDocumentExistRequest.statusId, Is.Not.LessThan(0),           "RetrievePolicyDocumentDetailsRequest: <statusId> Should be a positive integer");
                Assert.That(checkPolicyDocumentExistRequest.statusDate, Is.Not.Null.Or.Empty,       "RetrievePolicyDocumentDetailsRequest: <statusDate> Should not be null");
                Assert.That(checkPolicyDocumentExistRequest.effFrom, Is.Not.Null.Or.Empty,          "RetrievePolicyDocumentDetailsRequest: <effFrom> Should not be null");
                Assert.That(checkPolicyDocumentExistRequest.effTo, Is.Not.Null.Or.Empty,            "RetrievePolicyDocumentDetailsRequest: <effTo> Should not be null");
                Assert.That(checkPolicyDocumentExistRequest.audCreateUser, Is.Not.Null.Or.Empty,    "RetrievePolicyDocumentDetailsRequest: <audCreateUser> Should not be null or empty");
                Assert.That(checkPolicyDocumentExistRequest.audCreateDate, Is.Not.Null.Or.Empty,    "RetrievePolicyDocumentDetailsRequest: <audCreateDate> Should not be null");
                Assert.That(checkPolicyDocumentExistRequest.audModUser, Is.Not.Null.Or.Empty,       "RetrievePolicyDocumentDetailsRequest: <audModUser> Should not be null or empty");
            });
            TestContext.Out.WriteLine("Validated: <RetrievePolicyDocumentDetailsRequest> properties are not null or empty or not less than zero");
        }

        public void ValidateRetrievePolicyDocumentDetailsResponseIsNotNullOrEmptyOrLessThanZero(RetrievePolicyDocumentDetailsResponse retrievePolicyDocumentDetailsResponse)
        {
            Assert.Multiple(() => {
                Assert.That(retrievePolicyDocumentDetailsResponse.executionOutcome, Is.Not.Null.Or.Empty,                                   "RetrievePolicyDocumentDetailsResponse: <executionOutcome> Should not be null or empty");
                Assert.That(retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.sourceSystem, Is.Not.Null.Or.Empty,       "RetrievePolicyDocumentDetailsResponse: <sourceSystem> Should not be null or empty");
                Assert.That(retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.policyDocumentNo, Is.Not.LessThan(0),     "RetrievePolicyDocumentDetailsResponse: <policyDocumentNo> Should be a positive integer");
                Assert.That(retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.policyNo, Is.Not.LessThan(0),             "RetrievePolicyDocumentDetailsResponse: <policyNo> Should be a positive integer");
                Assert.That(retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.documentId, Is.Not.LessThan(0),           "RetrievePolicyDocumentDetailsResponse: <documentId> Should be a positive integer");
                Assert.That(retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.statusId, Is.Not.LessThan(0),             "RetrievePolicyDocumentDetailsResponse: <statusId> Should be a positive integer");
                Assert.That(retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.statusDate, Is.Not.Null.Or.Empty,         "RetrievePolicyDocumentDetailsResponse: <statusDate> Should not be null");
                Assert.That(retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.effFrom, Is.Not.Null.Or.Empty,            "RetrievePolicyDocumentDetailsResponse: <effFrom> Should not be null");
                Assert.That(retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.effTo, Is.Not.Null.Or.Empty,              "RetrievePolicyDocumentDetailsResponse: <effTo> Should not be null");
                Assert.That(retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.audCreateUser, Is.Not.Null.Or.Empty,      "RetrievePolicyDocumentDetailsResponse: <audCreateUser> Should not be null or empty");
                Assert.That(retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.audCreateDate, Is.Not.Null.Or.Empty,      "RetrievePolicyDocumentDetailsResp onse: <audCreateDate> Should not be null");
                Assert.That(retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.audModUser, Is.Not.Null.Or.Empty,         "RetrievePolicyDocumentDetailsResponse: <audModUser> Should not be null or empty");
                Assert.That(retrievePolicyDocumentDetailsResponse.checkPolicyDocumentExistRequest.audModDate, Is.Not.Null.Or.Empty,         "RetrievePolicyDocumentDetailsResponse: <audModDate> Should not be null");
            });
            TestContext.Out.WriteLine("Validated: <RetrievePolicyDocumentDetailsResponse> properties are not null or empty or not less than zero");
        }
    }
}
