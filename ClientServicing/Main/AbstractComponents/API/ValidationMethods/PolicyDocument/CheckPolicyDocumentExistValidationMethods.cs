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
    public class CheckPolicyDocumentExistValidationMethods : AbstractValidationMethods, ICheckPolicyDocumentExistValidationMethods
    {
        public void ValidateCheckPolicyDocumentExistRequestIsNotNullOrEmptyOrLessThanZero(CheckPolicyDocumentExistRequest checkPolicyDocumentExistRequest)
        {
            Assert.Multiple(() => {
                Assert.That(checkPolicyDocumentExistRequest.sourceSystem, Is.Not.Null.Or.Empty,     "CheckPolicyDocumentExistRequest: <sourceSystem> Should not be null or empty");
                Assert.That(checkPolicyDocumentExistRequest.policyDocumentNo, Is.Not.LessThan(0),   "CheckPolicyDocumentExistRequest: <policyDocumentNo> Should be a positive integer");
                Assert.That(checkPolicyDocumentExistRequest.policyNo, Is.Not.LessThan(0),           "CheckPolicyDocumentExistRequest: <policyNo> Should be a positive integer");
                Assert.That(checkPolicyDocumentExistRequest.documentId, Is.Not.LessThan(0),         "CheckPolicyDocumentExistRequest: <documentId> Should be a positive integer");
                Assert.That(checkPolicyDocumentExistRequest.processCd, Is.Not.LessThan(0),          "CheckPolicyDocumentExistRequest: <processCd> Should be a positive integer");
                Assert.That(checkPolicyDocumentExistRequest.statusId, Is.Not.LessThan(0),           "CheckPolicyDocumentExistRequest: <statusId> Should be a positive integer");
                Assert.That(checkPolicyDocumentExistRequest.statusDate, Is.Not.Null.Or.Empty,       "CheckPolicyDocumentExistRequest: <statusDate> Should not be null");
                Assert.That(checkPolicyDocumentExistRequest.effFrom, Is.Not.Null.Or.Empty,          "CheckPolicyDocumentExistRequest: <effFrom> Should not be null");
                Assert.That(checkPolicyDocumentExistRequest.effTo, Is.Not.Null.Or.Empty,            "CheckPolicyDocumentExistRequest: <effTo> Should not be null");
                Assert.That(checkPolicyDocumentExistRequest.audCreateUser, Is.Not.Null.Or.Empty,    "CheckPolicyDocumentExistRequest: <audCreateUser> Should not be null or empty");
                Assert.That(checkPolicyDocumentExistRequest.audCreateDate, Is.Not.Null.Or.Empty,    "CheckPolicyDocumentExistRequest: <audCreateDate> Should not be null");
                Assert.That(checkPolicyDocumentExistRequest.audModUser, Is.Not.Null.Or.Empty,       "CheckPolicyDocumentExistRequest: <audModUser> Should not be null or empty");
            });
            TestContext.Out.WriteLine("Validated: <CheckPolicyDocumentExistRequest> properties are not null or empty or not less than zero");
        }

        public void ValidateCheckPolicyDocumentExistResponseIsNotNullOrEmpty(CheckPolicyDocumentExistResponse checkPolicyDocumentExistResponse)
        {
            Assert.Multiple(() => {
                Assert.That(checkPolicyDocumentExistResponse.executionOutcome, Is.Not.Null.Or.Empty,    "CheckPolicyDocumentExistResponse: <executionOutcome> Should not be null or empty");
                Assert.That(checkPolicyDocumentExistResponse.data, Is.Not.Null.Or.Empty,                "CheckPolicyDocumentExistResponse: <data> Should not be null");
            });
            TestContext.Out.WriteLine("Validated: <CheckPolicyDocumentExistResponse> properties are not null or empty");
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
                    AllowedKinds = new[] { JsonValueKind.False, JsonValueKind.True }
                }
            };
            using var doc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(doc.RootElement, rules);
            TestContext.Out.WriteLine("Validated: Response Contects and data types are valid");
        }
    }
}
