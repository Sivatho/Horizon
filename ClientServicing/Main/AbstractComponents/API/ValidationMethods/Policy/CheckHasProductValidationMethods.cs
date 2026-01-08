using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Models.PolicyDocument;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy
{
    public class CheckHasProductValidationMethods : AbstractValidationMethods, ICheckHasProductValidationMethods
    {
        public void ValidateCheckHasProductRequestDataIsNotNullOrEmpty(CheckHasProductRequest checkHasProductRequest)
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(checkHasProductRequest.legalRefNo,  Is.Not.Null.Or.Empty,   "CheckHasProductRequest: <legalRefNo> Should not be null or empty");
                Assert.That(checkHasProductRequest.partnerCd,   Is.Not.LessThan(0),     "CheckHasProductRequest: <partnerCd> Should be a positive integer");
                Assert.That(checkHasProductRequest.entityNo,    Is.Not.LessThan(0),     "CheckHasProductRequest: <entityNo> Should be a positive integer");
                Assert.That(checkHasProductRequest.schemeCD,    Is.Not.LessThan(0),     "CheckHasProductRequest: <schemeCD> Should be a positive integer");
                Assert.That(checkHasProductRequest.planCD,      Is.Not.LessThan(0),     "CheckHasProductRequest: <planCD> Should be a positive integer");
                Assert.That(checkHasProductRequest.schemeDescr, Is.Not.Null.Or.Empty,   "CheckHasProductRequest: <schemeDescr> Should not be null or empty");
            }
            TestContext.Out.WriteLine("Validated: <CheckHasProductRequest> is not be null or empty");
        }

        public void ValidateCheckHasProductResponseDataIsNotNullOrEmpty(CheckHasProductResponse checkHasProductResponse)
        {
            Assert.Multiple(() => {
                Assert.That(checkHasProductResponse.executionOutcome,   Is.Not.Null.Or.Empty,   "CheckHasProductResponse: <executionOutcome> Should not be null or empty");
                Assert.That(checkHasProductResponse.data, Is.Not.Null.Or.Empty,                 "CheckPolicyDocumentExistResponse: <data> Should not be null");
            });
            TestContext.Out.WriteLine("Validated: <CheckHasProductResponse> is not be null or empty");
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
