using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank
{
    public class ValidateBankAccountQAVSRValidationMethods : AbstractValidationMethods, IValidateBankAccountQAVSRValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();
        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }
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
        public void ValidateValidateBankAccountQAVSRResponsetResponseDataIsNotNullOrEmptyOrLessThanZero(ValidateBankAccountQAVSRResponse validateBankAccountQAVSRResponse)
        {
            Assert.Multiple(() =>
            {
                //Assert.That(validateBankAccountQAVSRResponse.isValid, Is.Not.Null.Or.Empty, "ValidateBankAccountQAVSRResponse: <isValid> Should not be null or empty");
                //Assert.That(validateBankAccountQAVSRResponse.shouldUpdateAccountType, Is.Not.Null.Or.Empty, "ValidateBankAccountQAVSRResponse: <shouldUpdateAccountType> Should not be null or empty");
                //Assert.That(validateBankAccountQAVSRResponse.overrideIsValid, Is.Not.Null.Or.Empty, "ValidateBankAccountQAVSRResponse: <overrideIsValid> Should not be null or empty");
                Assert.That(validateBankAccountQAVSRResponse.message, Is.Not.Null.Or.Empty, "ValidateBankAccountQAVSRResponse: <message> Should not be null or empty");
                Assert.That(validateBankAccountQAVSRResponse.fraudsterFailure, Is.Not.LessThan(0), "ValidateBankAccountQAVSRResponse: <fraudsterFailure> Should not be less than zero");
                Assert.That(validateBankAccountQAVSRResponse.softyCompResult, Is.Not.Null, "ValidateBankAccountQAVSRResponse: <softyCompResult> Should not be null");
                Assert.That(validateBankAccountQAVSRResponse.fraudsterResult, Is.Not.Null, "ValidateBankAccountQAVSRResponse: <fraudsterResult> Should not be null");
                Assert.That(validateBankAccountQAVSRResponse.d3BlackListResult, Is.Not.Null, "ValidateBankAccountQAVSRResponse: <d3BlackListResult> Should not be null");
                Assert.That(validateBankAccountQAVSRResponse.avsrResult, Is.Not.Null, "ValidateBankAccountQAVSRResponse: <avsrResult> Should not be null");
                //Assert.That(validateBankAccountQAVSRResponse.correctBankAccountType.type, Is.Not.LessThan(0), "ValidateBankAccountQAVSRResponse: <correctBankAccountType> Should not be nullShould not be less than zero");
            });
        }
        public ValidateBankAccountQAVSRResponse populateValidateBankAccountQAVSRResponse(RestResponse response)
        {
            using JsonDocument document = JsonDocument.Parse(response.Content);
            ValidateBankAccountQAVSRResponse validateBankAccountQAVSRResponse = new()
            {
                softyCompResult = new TestResult(),
                fraudsterResult = new TestResult(),
                d3BlackListResult = new TestResult(),
                avsrResult = new AVSRResult()
            };

            foreach (var property in document.RootElement.EnumerateObject())
            {
                switch (property.Name.ToLower())
                {
                    case "isvalid":                 validateBankAccountQAVSRResponse.isValid =                  (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "shouldupdateaccounttype": validateBankAccountQAVSRResponse.shouldUpdateAccountType =  (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "overrideisvalid":         validateBankAccountQAVSRResponse.overrideIsValid =          (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message":                 validateBankAccountQAVSRResponse.message =                  utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "fraudsterfailure":        validateBankAccountQAVSRResponse.fraudsterFailure =         (int)utilitiesHelper.ReadInt32Nullable(property.Value); break;
                    case "softycompresult":
                        var softyCompResult = new TestResult();
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name)
                            {
                                case "wasTestResultOverridden": softyCompResult.wasTestResultOverridden =   (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "wasTestPerformed":        softyCompResult.wasTestPerformed =          (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "isValid":                 softyCompResult.isValid =                   (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "message":                 softyCompResult.message =                   utilitiesHelper.ReadStringNullable(item.Value)?? string.Empty; break;
                            }
                            validateBankAccountQAVSRResponse.softyCompResult = softyCompResult;
                        }
                        break;
                    case "fraudsterresult":
                        var fraudsterResult = new TestResult();
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name)
                            {
                                case "wasTestResultOverridden": fraudsterResult.wasTestResultOverridden =   (bool)utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "wasTestPerformed":        fraudsterResult.wasTestPerformed =          (bool)utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "fraudsterFailureType":    fraudsterResult.fraudsterFailureType =      (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                case "isValid":                 fraudsterResult.isValid =                   (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "message":                 fraudsterResult.message =                   utilitiesHelper.ReadStringNullable(item.Value)?? string.Empty; break;
                            }
                            validateBankAccountQAVSRResponse.fraudsterResult = fraudsterResult;
                        }
                        break;
                    case "d3blacklistresult":
                        var d3BlackListResult = new TestResult();
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name)
                            {
                                case "wasTestResultOverridden": d3BlackListResult.wasTestResultOverridden = (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "wasTestPerformed":        d3BlackListResult.wasTestPerformed =        (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "isValid":                 d3BlackListResult.isValid =                 (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "message":                 d3BlackListResult.message =                 utilitiesHelper.ReadStringNullable(item.Value)?? string.Empty; break;
                            }
                            validateBankAccountQAVSRResponse.d3BlackListResult = d3BlackListResult;
                        }
                        break;
                    case "avsrresult":
                        var avsrResult = new AVSRResult();
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name)
                            {
                                case "qLinkAvsrCheckId":            avsrResult.qLinkAvsrCheckId =           utilitiesHelper.ReadStringNullable(item.Value) ?? string.Empty;  break;
                                case "errorCode":                   avsrResult.errorCode =                  utilitiesHelper.ReadStringNullable(item.Value) ?? string.Empty; break;
                                case "errorDescription":            avsrResult.errorDescription =           utilitiesHelper.ReadStringNullable(item.Value) ?? string.Empty; break;
                                case "sessionId":                   avsrResult.sessionId =                  utilitiesHelper.ReadStringNullable(item.Value) ?? string.Empty; break;
                                case "accountStatusId":             avsrResult.accountStatusId =            utilitiesHelper.ReadStringNullable(item.Value) ?? string.Empty; break;
                                case "wasCachedResultUsed":         avsrResult.wasCachedResultUsed =        (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "wasBankAccountFound":         avsrResult.wasBankAccountFound =        (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "isBankAccountOpen":           avsrResult.isBankAccountOpen =          (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "doesBankAccountTypeMatch":    avsrResult.doesBankAccountTypeMatch =   (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "doesInitialsMatch":           avsrResult.doesInitialsMatch =          (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "doesIdentityNumberMatch":     avsrResult.doesIdentityNumberMatch =    (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "doesNameMatch":               avsrResult.doesNameMatch =              (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "doesAcceptsDebits":           avsrResult.doesAcceptsDebits =          (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "doesAcceptsCredits":          avsrResult.doesAcceptsCredits =         (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "didTimeout":                  avsrResult.didTimeout =                 (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "accountLengthMatch":          avsrResult.accountLengthMatch =         (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "missingParameter":            avsrResult.missingParameter =           (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "doesPhoneMatch":              avsrResult.doesPhoneMatch =             (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "doesEmailMatch":              avsrResult.doesEmailMatch =             (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "hasBankAccountBeenOpenForMoreThan3Months": avsrResult.hasBankAccountBeenOpenForMoreThan3Months = (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "responseDate":                avsrResult.responseDate =               (DateTime)utilitiesHelper.ReadDateTimeNullable(item.Value); break;
                                case "wasTestPerformed":            avsrResult.wasTestPerformed =           (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "isValid":                     avsrResult.isValid =                    (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "message":                     avsrResult.message =                    utilitiesHelper.ReadStringNullable(item.Value); break;
                                case "isForcedSuccessResponse":     avsrResult.isForcedSuccessResponse =    (bool) utilitiesHelper.ReadBooleanNullable(item.Value); break;
                            }
                            validateBankAccountQAVSRResponse.avsrResult = avsrResult;
                        }
                        break;
                    case "correctbankaccounttype":
                        var correctBankAccountType = new CorrectBankAccountType();
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name)
                            {
                                case "type": correctBankAccountType.type = (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                            }
                        }
                        break;
                    case "correctbankname": validateBankAccountQAVSRResponse.correctBankName = utilitiesHelper.ReadStringNullable(property.Value) ?? string.Empty; break;
                    default: TestContext.Out.WriteLine($"Unkown Property: {property.Name}"); break;
                }
            }
            return validateBankAccountQAVSRResponse;
        }

    }
}
