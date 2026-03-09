using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.IValidationMethods.Bank;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank
{
    public class ValidateBankAccountResponseValidationMethods : AbstractValidationMethods, IValidateBankAccountResponseValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();
        public void ValidateValidateBankAccountRequestDataIsNotNullOrEmpty(ValidateBankAccountRequest validateBankAccountRequest)
        {
           Assert.That(validateBankAccountRequest, Is.Not.Null, "ValidateBankAccountRequest is null.");
            Assert.Multiple(() =>
            {
                Assert.That(validateBankAccountRequest.idNumber,                    Is.Not.Null.And.Not.Empty, "Response: idNumber is null or empty.");
                Assert.That(validateBankAccountRequest.bankAccountHolderInitials,   Is.Not.Null.And.Not.Empty, "Response: bankAccountHolderInitials is null or empty.");
                Assert.That(validateBankAccountRequest.surname,                     Is.Not.Null.And.Not.Empty, "Response: surname is null or empty.");
                Assert.That(validateBankAccountRequest.bankName,                    Is.Not.Null.And.Not.Empty, "Response: bankName is null or empty.");
                Assert.That(validateBankAccountRequest.bankAccountNumber,           Is.Not.Null.And.Not.Empty, "Response: bankAccountNumber is null or empty.");
                Assert.That(validateBankAccountRequest.bankBranchCode,              Is.Not.Null.And.Not.Empty, "Response: bankBranchCode is null or empty.");
                Assert.That(validateBankAccountRequest.bankAccountType,             Is.Not.Null.And.Not.Empty, "Response: bankAccountType is null or empty.");
            });
            TestContext.Out.WriteLine("ValidateBankAccountRequest and its properties are not null or empty.");
        }
        public void ValidateValidateBankAccountResponseDataIsNotNullOrEmpty(ValidateBankAccountResponse validateBankAccountResponse)
        {
            Assert.Multiple(() =>
            {
                Assert.That(validateBankAccountResponse, Is.Not.Null, "ValidateBankAccountResponse is null.");
                Assert.That(validateBankAccountResponse.softyCompResult, Is.Not.Null, "ValidateBankAccountResponse: softyCompResult is null");
                Assert.That(validateBankAccountResponse.fraudsterResult, Is.Not.Null, "ValidateBankAccountResponse: fraudsterResult is null");
                Assert.That(validateBankAccountResponse.d3BlackListResult, Is.Not.Null, "ValidateBankAccountResponse: d3BlackListResult is null");
                Assert.That(validateBankAccountResponse.avsrResult, Is.Not.Null, "ValidateBankAccountResponse: avsrResult is null");

            });
            TestContext.Out.WriteLine("ValidateBankAccountResponse and its nested objects are not null.");
        }
        public ValidateBankAccountResponse PopulateValidateBankAccountResponse(RestResponse response)
        {
            using JsonDocument document = JsonDocument.Parse(response.Content);
            ValidateBankAccountResponse validateBankAccountResponse = new ValidateBankAccountResponse
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
                    case "isvalid":
                        validateBankAccountResponse.isValid = (bool)utilitiesHelper.ReadBooleanNullable(property.Value);
                        break;
                    case "shouldupdateaccounttype":
                        validateBankAccountResponse.shouldUpdateAccountType = (bool)utilitiesHelper.ReadBooleanNullable(property.Value);
                        break;
                    case "overrideisvalid":
                        validateBankAccountResponse.overrideIsValid = (bool)utilitiesHelper.ReadBooleanNullable(property.Value);
                        break;
                    case "message":
                        validateBankAccountResponse.message = utilitiesHelper.ReadStringNullable(property.Value);
                        break;
                    case "fraudsterfailure":
                        validateBankAccountResponse.fraudsterFailure = (int)utilitiesHelper.ReadInt32Nullable(property.Value);
                        break;
                    case "correctbankname":
                        validateBankAccountResponse.correctBankName = utilitiesHelper.ReadStringNullable(property.Value);
                        break;
                    case "softycompresult":
                        validateBankAccountResponse.softyCompResult = populateTestResult(property);
                        break;
                    case "fraudsterresult":
                        validateBankAccountResponse.fraudsterResult = populateTestResult(property);
                        break;
                    case "d3blacklistresult":
                        validateBankAccountResponse.d3BlackListResult = populateTestResult(property);
                        break;
                    case "avsrresult":
                        validateBankAccountResponse.avsrResult = populateAVSRResult(property);
                        break;
                    default:
                        TestContext.Out.WriteLine($"Unknown - ValidateBankAccountResponse property: {property.Name}");
                        break;
                }
            }
            return validateBankAccountResponse;
        }
        private TestResult populateTestResult(JsonProperty property)
        {
            var result = new TestResult();
            foreach (var item in property.Value.EnumerateObject())
            {
                switch (item.Name.ToLower())
                {
                    case "wastestresultoverridden":
                        result.wasTestResultOverridden = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "wastestperformed":
                        result.wasTestPerformed = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "isvalid":
                        result.isValid = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "message":
                        result.message = utilitiesHelper.ReadStringNullable(item.Value) ?? string.Empty;
                        break;
                    case "fraudsterfailuretype":
                        result.fraudsterFailureType = item.Value.GetInt32();
                        break;
                    default:
                        TestContext.Out.WriteLine($"Unknown - TestResult property: {property.Name} > {item.Name}");
                        break;
                }
            }
            return result;
        }
        private AVSRResult populateAVSRResult(JsonProperty property)
        {
            var avsrResult = new AVSRResult();
            foreach (var item in property.Value.EnumerateObject())
            {
                string itemName = item.Name.ToLower();
                switch (itemName)
                {
                    case "qlinkavsrcheckid":
                        avsrResult.qLinkAvsrCheckId = utilitiesHelper.ReadStringNullable(item.Value) ?? string.Empty;
                        break;
                    case "errorcode":
                        avsrResult.errorCode = utilitiesHelper.ReadStringNullable(item.Value) ?? string.Empty;
                        break;
                    case "errordescription":
                        avsrResult.errorDescription = utilitiesHelper.ReadStringNullable(item.Value) ?? string.Empty;
                        break;
                    case "sessionid":
                        avsrResult.sessionId = utilitiesHelper.ReadStringNullable(item.Value) ?? string.Empty;
                        break;
                    case "accountstatusid":
                        avsrResult.accountStatusId = utilitiesHelper.ReadStringNullable(item.Value) ?? string.Empty;
                        break;
                    case "wascachedresultused":
                        avsrResult.wasCachedResultUsed = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "wasbankaccountfound":
                        avsrResult.wasBankAccountFound = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "isbankaccountopen":
                        avsrResult.isBankAccountOpen = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "doesbankaccounttypematch":
                        avsrResult.doesBankAccountTypeMatch = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "doesinitialsmatch":
                        avsrResult.doesInitialsMatch = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "doesidentitynumbermatch":
                        avsrResult.doesIdentityNumberMatch = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "doesnamematch":
                        avsrResult.doesNameMatch = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "doesacceptsdebits":
                        avsrResult.doesAcceptsDebits = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "doesacceptscredits":
                        avsrResult.doesAcceptsCredits = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "didtimeout":
                        avsrResult.didTimeout = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "accountlengthmatch":
                        avsrResult.accountLengthMatch = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "missingparameter":
                        avsrResult.missingParameter = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "doesphonematch":
                        avsrResult.doesPhoneMatch = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "doesemailmatch":
                        avsrResult.doesEmailMatch = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "hasbankaccountbeenopenformorethan3months":
                        avsrResult.hasBankAccountBeenOpenForMoreThan3Months = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "responsedate":
                        avsrResult.responseDate = (DateTime)utilitiesHelper.ReadDateTimeNullable(item.Value);
                        break;
                    case "wastestperformed":
                        avsrResult.wasTestPerformed = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "isvalid":
                        avsrResult.isValid = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "message":
                        avsrResult.message = utilitiesHelper.ReadStringNullable(item.Value) ?? string.Empty;
                        break;
                    case "isforcedsuccessresponse":
                        avsrResult.isForcedSuccessResponse = (bool)utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    default:
                        TestContext.Out.WriteLine($"Unknown - AVSRResult property: {property.Name} + {property.Value}");
                        break;
                }
            }
            return avsrResult;
        }
        public override void ValidateResponseFieldParametersIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }

        public override void ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(RestResponse restResponse)
        {
            throw new NotImplementedException();
        }
    }
}
