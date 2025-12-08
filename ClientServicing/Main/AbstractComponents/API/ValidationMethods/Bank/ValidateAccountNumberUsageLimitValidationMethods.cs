using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank
{
    public class ValidateAccountNumberUsageLimitValidationMethods : AbstractValidationMethods, IValidateAccountNumberUsageLimitValidationMethods
    {
        public void ValidateValidateAccountNumberUsageLimitResponseDataIsNotNullOrEmpty(ValidateAccountNumberUsageLimitResponse validateAccountNumberUsageLimitResponse)
        {
            throw new NotImplementedException();
        }

        public override void ValidateResponsePropertyNameIsValid_AndDataTypesIsValid(RestResponse restResponse)
        {
            var rules = new List<JsonValidationRule> {
                new JsonValidationRule {
                    PropertyName = "success",
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
                    PropertyName = "totalPolicies",
                    AllowedKinds = new[] {
                        JsonValueKind.Number, JsonValueKind.Null
                    }
                },
                new JsonValidationRule {
                    PropertyName = "limitExceeded",
                    AllowedKinds = new[] {
                        JsonValueKind.True, JsonValueKind.False
                    }
                }
            };
            using var doc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(doc.RootElement, rules);
            TestContext.Out.WriteLine("ValidateAccountNumberUsageLimit: response content data types are valid.");
        }

        public void ValidateResponseIsNotNullOrEmpty(ValidateAccountNumberUsageLimitResponse validateAccountNumberUsageLimitResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(validateAccountNumberUsageLimitResponse.success,        Is.True.Or.False.And.Not.Null,                  "ValidateAccountNumberUsageLimit Response: success can be true or false but not null.");
                Assert.That(validateAccountNumberUsageLimitResponse.message,        Is.Not.Null,                                    "ValidateAccountNumberUsageLimit Response: message should not be null");
                Assert.That(validateAccountNumberUsageLimitResponse.totalPolicies,  Is.Not.LessThan(0).And.Not.Null.Or.Not.Empty,   "ValidateAccountNumberUsageLimit Response: totalPolicies should not be less than 0.");
                Assert.That(validateAccountNumberUsageLimitResponse.limitExceeded,  Is.True.Or.False.And.Not.Null,                  "ValidateAccountNumberUsageLimit Response: limitExceeded can be true or false but not null."
                );
            });
            TestContext.Out.WriteLine("ValidateAccountNumberUsageLimit: response is not null or empty.");
        }

        public void ValidateResponseIsNullOrWhiteSpace(ValidateAccountNumberUsageLimitResponse validateAccountNumberUsageLimitResponse)
        {
            throw new NotImplementedException();
        }

        public override void ValidateResponseSchemaIsValid(RestResponse restResponse, string folder, string jsonfile)
        {
            throw new NotImplementedException();
        }
    }
}
