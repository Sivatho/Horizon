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
    public class ValidateBankAccountResponseValidationMethods : AbstractValidationMethods, IValidateBankAccountResponseValidationMethods
    {
        public override void ValidateResponsePropertyNameIsValid_AndDataTypesIsValid(RestResponse restResponse)
        {
            var rules = new List<JsonValidationRule> {
                new JsonValidationRule {
                    PropertyName = "isValid",
                    AllowedKinds = new[] { JsonValueKind.True, JsonValueKind.False }
                },
                new JsonValidationRule {
                    PropertyName = "shouldUpdateAccountType",
                    AllowedKinds = new[] { JsonValueKind.True, JsonValueKind.False }
                },
                new JsonValidationRule {
                    PropertyName = "overrideIsValid",
                    AllowedKinds = new[] { JsonValueKind.True, JsonValueKind.False }
                },
                new JsonValidationRule {
                    PropertyName = "message",
                    AllowedKinds = new[] { JsonValueKind.String }
                },
                new JsonValidationRule {
                    PropertyName = "fraudsterFailure",
                    AllowedKinds = new[] { JsonValueKind.Number }
                },
                new JsonValidationRule {
                    PropertyName = "correctBankName",
                    AllowedKinds = new[] { JsonValueKind.String, JsonValueKind.Null }
                },
                new JsonValidationRule {
                    PropertyName = "softyCompResult",
                    AllowedKinds = new[] { JsonValueKind.Object },
                    NestedRules = new Dictionary<string, JsonValueKind[]>
                    {
                        { "wasTestResultOverridden",new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "wasTestPerformed",       new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "isValid",                new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "message",                new[] {  JsonValueKind.String, JsonValueKind.Null }}
                    }
                },
                new JsonValidationRule {
                    PropertyName = "fraudsterResult",
                    AllowedKinds = new[] { JsonValueKind.Object },
                    NestedRules = new Dictionary<string, JsonValueKind[]>
                    {
                        { "wasTestResultOverridden",new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "fraudsterFailure",       new[] { JsonValueKind.Number } },
                        { "wasTestPerformed",       new[] { JsonValueKind.True, JsonValueKind.False } }, 
                        { "isValid",                new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "message",                new[] { JsonValueKind.String, JsonValueKind.Null } }
                    }
                },
                new JsonValidationRule {
                    PropertyName = "d3BlackListResult",
                    AllowedKinds = new[] { JsonValueKind.Object },
                    NestedRules = new Dictionary<string, JsonValueKind[]>
                    {
                        { "wasTestResultOverridden",new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "wasTestPerformed",       new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "isValid",                new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "message",                new[] {  JsonValueKind.String, JsonValueKind.Null }}
                    }
                },
                new JsonValidationRule {
                    PropertyName = "avsrResult",
                    AllowedKinds = new[] { JsonValueKind.Object },
                    NestedRules = new Dictionary<string, JsonValueKind[]>
                    {
                        { "qLinkAvsrCheckId",      new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "errorCode",             new[] { JsonValueKind.Number, JsonValueKind.Null } },
                        { "errorDescription",      new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "sessionId",             new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "accountStatusId",       new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "wasCachedResultUsed",   new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "wasBankAccountFound",   new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "isBankAccountOpen",     new[] { JsonValueKind.True, JsonValueKind.False } },
                        
                        { "doesBankAccountTypeMatch", new[] { JsonValueKind.True, JsonValueKind.False } },

                        { "doesInitialsMatch",     new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "doesIdentityNumberMatch", new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "doesNameMatch",         new[] { JsonValueKind.True, JsonValueKind.False } },

                        
                        { "doesAcceptsDebits",     new[] { JsonValueKind.True, JsonValueKind.False } },

                        { "doesAcceptsCredits",    new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "didTimeout",            new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "accountLengthMatch",    new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "missingParameter",      new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "doesPhoneMatch",        new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "doesEmailMatch",        new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "hasBankAccountBeenOpenForMoreThan3Months", new[] { JsonValueKind.True, JsonValueKind.False } },

                        { "responseDate",          new[] { JsonValueKind.String } },     
                        { "wasTestPerformed",      new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "isValid",               new[] { JsonValueKind.True, JsonValueKind.False } },
                        { "message",               new[] { JsonValueKind.String, JsonValueKind.Null } },
                        { "isForcedSuccessResponse", new[] { JsonValueKind.True, JsonValueKind.False } }
                    }
                }
            };
            using var doc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(doc.RootElement, rules);
            TestContext.Out.WriteLine("ValidateBankAccountResponse content and data types are valid.");
        }

        public void ValidateResponseIsNotNullOrEmpty(ValidateBankAccountResponse validateBankAccountResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(validateBankAccountResponse,                    Is.Not.Null, "ValidateBankAccountResponse is null.");
                Assert.That(validateBankAccountResponse.softyCompResult,    Is.Not.Null, "ValidateBankAccountResponse: softyCompResult is null");
                Assert.That(validateBankAccountResponse.fraudsterResult,    Is.Not.Null, "ValidateBankAccountResponse: fraudsterResult is null");
                Assert.That(validateBankAccountResponse.d3BlackListResult,  Is.Not.Null, "ValidateBankAccountResponse: d3BlackListResult is null");
                Assert.That(validateBankAccountResponse.avsrResult,         Is.Not.Null, "ValidateBankAccountResponse: avsrResult is null");

            });
            TestContext.Out.WriteLine("ValidateBankAccountResponse and its nested objects are not null.");
        }

        public void ValidateResponseIsNullOrWhiteSpace(ValidateBankAccountResponse validateBankAccountResponse)
        {
            throw new NotImplementedException();
        }

        public void ValidateValidateBankAccountResponseDataIsNotNullOrEmpty(ValidateBankAccountResponse validateBankAccountResponse)
        {
            throw new NotImplementedException();
        }
    }
}
