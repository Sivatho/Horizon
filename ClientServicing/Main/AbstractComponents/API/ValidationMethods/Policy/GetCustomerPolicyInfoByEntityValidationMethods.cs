using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Policy;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy
{
    public class GetCustomerPolicyInfoByEntityValidationMethods : AbstractValidationMethods, IGetCustomerPolicyInfoByEntityValidationMethods
    {
        public void ValidateGetCustomerPolicyInfoByEntityResquestDataIsNotNullorEmpty(GetCustomerPolicyInfoByEntityNoRequest getCustomerPolicyInfoByEntityNoRequest)
        {
            throw new NotImplementedException();
        }

        public void ValidateGetCustomerPolicyInfoByEntityResponseDataIsNotNull(GetCustomerPolicyInfoByEntityResponse getCustomerPolicyInfoByEntityResponse)
        {
            Assert.Multiple(() => {
                Assert.That(getCustomerPolicyInfoByEntityResponse.executionOutcome, Is.Not.Null.Or.Empty, "GetCustomerPolicyInfoByEntityResponse: Execution Outcome hhould not be null or empty");
                Assert.That(getCustomerPolicyInfoByEntityResponse.data, Is.Not.Null.Or.Empty, "GetCustomerPolicyInfoByEntityResponse: data should not be null or empty");
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
                    AllowedKinds = new[] { JsonValueKind.Array },
                    NestedRules = new Dictionary<string, JsonValueKind[]>
                    {
                        { "policyNo",       new[] { JsonValueKind.Number, JsonValueKind.Null } },
                        { "ifaNo",          new[] { JsonValueKind.Number, JsonValueKind.Null } },
                        { "channelDescr",   new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "productDescr",   new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "planTypeDescr",  new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "policyStatus",   new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "statusCd",       new[] { JsonValueKind.Number, JsonValueKind.Null } },
                        { "dateOfCommencement", new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "payer",          new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "policyPremium",  new[] { JsonValueKind.Number, JsonValueKind.Null } },
                        { "billedTo",       new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "paidTo",         new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "premiumCount",   new[] { JsonValueKind.Number, JsonValueKind.Null } },
                        { "premiumFrequency", new[] { JsonValueKind.Number, JsonValueKind.Null } },
                        { "salesPerson",    new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "debiCheckStatus", new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "legacyPolicyNo", new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "statusDate",     new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "partnerCD",      new[] { JsonValueKind.Number, JsonValueKind.Null } },
                        { "inspiratorNo",   new[] { JsonValueKind.Number, JsonValueKind.Null } },
                    }
                }
            };
            using var doc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(doc.RootElement, rules);
            TestContext.Out.WriteLine("Response: Contects and data types are valid");
        }

        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }
    }
}