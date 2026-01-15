using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.Bank
{
    public class ValidateBankAccountAPITest : ValidateBankAccountResponseValidationMethods
    {
        BankAPIClient bankAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();
        [Test]
        public async Task GivenBankAccountIsValid_WhenValidateBankAccountAsync_ThenValidateResponseIsOk_AndIsNotNull_AndDataTypesIsValid()
        {
            //Arrange
            ValidateBankAccountRequest validateBankAccountRequest = JsonSerializer.Deserialize<ValidateBankAccountRequest>(utilitiesHelper.ReadTestDataJson("Bank/Data", "ValidateBankAccountRequest_Valid.json"));

            //Act
            var response = await bankAPIClient.ValidateBankAccountAsync(validateBankAccountRequest);
            var validateBankAccountResponse = populateValidateBankAccountResponse(response);
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateValidateBankAccountResponseDataIsNotNullOrEmpty(validateBankAccountResponse);
        }
        private ValidateBankAccountResponse populateValidateBankAccountResponse(RestResponse response)
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
                        result.wasTestResultOverridden = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "wastestperformed":
                        result.wasTestPerformed = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "isvalid":
                        result.isValid = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "message":
                        result.message =  utilitiesHelper.ReadStringNullable(item.Value)?? string.Empty;
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
                        avsrResult.qLinkAvsrCheckId =  utilitiesHelper.ReadStringNullable(item.Value)?? string.Empty;
                        break;
                    case "errorcode":
                        avsrResult.errorCode =  utilitiesHelper.ReadStringNullable(item.Value)?? string.Empty;
                        break;
                    case "errordescription":
                        avsrResult.errorDescription =  utilitiesHelper.ReadStringNullable(item.Value)?? string.Empty;
                        break;
                    case "sessionid":
                        avsrResult.sessionId =  utilitiesHelper.ReadStringNullable(item.Value)?? string.Empty;
                        break;
                    case "accountstatusid":
                        avsrResult.accountStatusId =  utilitiesHelper.ReadStringNullable(item.Value)?? string.Empty;
                        break;
                    case "wascachedresultused":
                        avsrResult.wasCachedResultUsed = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "wasbankaccountfound":
                        avsrResult.wasBankAccountFound = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "isbankaccountopen":
                        avsrResult.isBankAccountOpen = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "doesbankaccounttypematch":
                        avsrResult.doesBankAccountTypeMatch = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "doesinitialsmatch":
                        avsrResult.doesInitialsMatch = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "doesidentitynumbermatch":
                        avsrResult.doesIdentityNumberMatch = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "doesnamematch":
                        avsrResult.doesNameMatch = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "doesacceptsdebits":
                        avsrResult.doesAcceptsDebits = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "doesacceptscredits":
                        avsrResult.doesAcceptsCredits = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "didtimeout":
                        avsrResult.didTimeout = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "accountlengthmatch":
                        avsrResult.accountLengthMatch = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "missingparameter":
                        avsrResult.missingParameter = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "doesphonematch":
                        avsrResult.doesPhoneMatch = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "doesemailmatch":
                        avsrResult.doesEmailMatch = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "hasbankaccountbeenopenformorethan3months":
                        avsrResult.hasBankAccountBeenOpenForMoreThan3Months = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "responsedate":
                        avsrResult.responseDate = (DateTime)utilitiesHelper.ReadDateTimeNullable(item.Value);
                        break;
                    case "wastestperformed":
                        avsrResult.wasTestPerformed = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "isvalid":
                        avsrResult.isValid = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    case "message":
                        avsrResult.message =  utilitiesHelper.ReadStringNullable(item.Value)?? string.Empty;
                        break;
                    case "isforcedsuccessresponse":
                        avsrResult.isForcedSuccessResponse = (bool) utilitiesHelper.ReadBooleanNullable(item.Value);
                        break;
                    default:
                        TestContext.Out.WriteLine($"Unknown - AVSRResult property: {property.Name} + {property.Value}");
                        break;
                }
            }
            return avsrResult;
        }
    }
}
