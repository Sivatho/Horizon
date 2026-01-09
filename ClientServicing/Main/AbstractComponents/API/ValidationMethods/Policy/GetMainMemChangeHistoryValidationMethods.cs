using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy
{
    public class GetMainMemChangeHistoryValidationMethods : AbstractValidationMethods, IGetMainMemChangeHistoryValidationMethods
    {
        public void ValidateGetMainMemChangeHistoryRequestDataIsNotNullOrEmpty(PolicyBeneficiaryDetailsRequest getMainMemChangeHistoryRequest)
        {
            Assert.Multiple(() =>
            {
                Assert.That(getMainMemChangeHistoryRequest.policyNo, Is.Not.LessThan(0).Or.Empty, "GetMainMemChangeHistoryRequest: <policyNo> Should not be less than 0 or empty");
                Assert.That(getMainMemChangeHistoryRequest.legacyPolicyNumber, Is.Not.Null.Or.Empty, "GetMainMemChangeHistoryRequest: <legacyPolicyNumber> Should not null or empty");
            });
            TestContext.Out.WriteLine("Validated: <GetMainMemChangeHistoryRequest> is not null or empty; integers are not less than 0 or empty");
        }
        public void ValidateGetMainMemChangeHistoryResponseDataIsNotNullOrEmpty(GetMainMemChangeHistoryResponse getMainMemChangeHistoryResponse)
        {
            Assert.Multiple(() => {
                Assert.That(getMainMemChangeHistoryResponse.executionOutcome, Is.Not.Null.Or.Empty, "CheckHasProductResponse: <executionOutcome> Should not be null or empty");
                Assert.That(getMainMemChangeHistoryResponse.data.Count, Is.Not.LessThan(1), "CheckHasProductResponse: <data> Should not be null or empty");
            });
            TestContext.Out.WriteLine("Validated: <GetMainMemChangeHistoryResponse> is not be null or empty");
        }

        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
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
                    AllowedKinds = new[] { JsonValueKind.Array },
                    NestedRules = new Dictionary<string, JsonValueKind[]>
                    {
                        { "entityNo",           new[] { JsonValueKind.Number, JsonValueKind.Null } },
                        { "entityGenderCD",     new[] { JsonValueKind.Number, JsonValueKind.Null } },
                        { "entityTitleCD",      new[] { JsonValueKind.Number, JsonValueKind.Null } },
                        { "entityName",         new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "entitySurname",      new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "legalRefNo",         new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "effFrom",            new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "effTo",              new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "modifiedBy",         new[] { JsonValueKind.String, JsonValueKind.Null } }
                    }
                }
            };
            using var doc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(doc.RootElement, rules);
            TestContext.Out.WriteLine("Validated: Response Contects and data types are valid");
        }
    }
}
