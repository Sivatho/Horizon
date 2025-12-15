using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank
{
    public class ValidateBankAccountQAVSRValidationMethods : AbstractValidationMethods, IValidateBankAccountQAVSRValidationMethods
    {
        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
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
                },
                new JsonValidationRule {
                    PropertyName = "correctBankAccountType",
                    AllowedKinds = new[] { JsonValueKind.Object},
                    NestedRules = new Dictionary<string, JsonValueKind[]> {
                        { "type", new[] { JsonValueKind.String, JsonValueKind.Null } }
                    }
                }
            };
            using var doc = JsonDocument.Parse(restResponse.Content);
            JsonValidationRule.ValidateJson(doc.RootElement, rules);
            TestContext.Out.WriteLine("Response: content and data types are valid.");
        }

        public void ValidateResponseIsNotNullOrEmpty(ValidateBankAccountQAVSRResponse validateBankAccountQAVSRResponse)
        {
            throw new NotImplementedException();
        }

        public void ValidateResponseIsNullOrWhiteSpace(ValidateBankAccountQAVSRResponse validateBankAccountQAVSRResponse)
        {
            throw new NotImplementedException();
        }

        public void ValidateValidateBankAccountQAVSRResponsetResponseDataIsNotNullOrEmpty(ValidateBankAccountQAVSRResponse validateBankAccountQAVSRResponse)
        {
            throw new NotImplementedException();
        }
    }
}
