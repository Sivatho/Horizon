using System.Text.Json;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;

using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD
{
    public class ValidateBankAccountTest
    {
        UtilitiesHelper utilitiesHelper = new();
        [Test]
        public async Task GivenBankAccountIsValid_WhenValidateBankAccountAsync_ThenValidateResponseIsOk_AndIsNotNull_AndDataTypesIsValid()
        {
            //Arrange
            BankAPIClient bankClient = new("https://horizon.clientele.co.za/horizon.clientservicing/");
            ValidateBankAccountRequest validateBankAccountRequest = JsonSerializer.Deserialize<ValidateBankAccountRequest>(utilitiesHelper.ReadJson("Bank", "ValidateBankAccountRequest_Valid.json"));

            //Act
           var json = JsonSerializer.Serialize("{\r\n    \"isValid\": false,\r\n    \"shouldUpdateAccountType\": false,\r\n    \"overrideIsValid\": false,\r\n    \"message\": \"Account Failed Check Digit Verification.\",\r\n    \"fraudsterFailure\": 1,\r\n    \"correctBankName\": null,\r\n    \"softyCompResult\": {\r\n        \"wasTestResultOverridden\": false,\r\n        \"wasTestPerformed\": true,\r\n        \"isValid\": false,\r\n        \"message\": \"Account Failed Check Digit Verification.\"\r\n    },\r\n    \"fraudsterResult\": {\r\n        \"wasTestResultOverridden\": false,\r\n        \"fraudsterFailureType\": 0,\r\n        \"wasTestPerformed\": true,\r\n        \"isValid\": true,\r\n        \"message\": null\r\n    },\r\n    \"d3BlackListResult\": {\r\n        \"wasTestResultOverridden\": false,\r\n        \"wasTestPerformed\": false,\r\n        \"isValid\": true,\r\n        \"message\": null\r\n    },\r\n    \"avsrResult\": {\r\n        \"qLinkAvsrCheckId\": \"00000000-0000-0000-0000-000000000000\",\r\n        \"errorCode\": null,\r\n        \"errorDescription\": null,\r\n        \"sessionId\": null,\r\n        \"accountStatusId\": null,\r\n        \"wasCachedResultUsed\": false,\r\n        \"wasBankAccountFound\": false,\r\n        \"isBankAccountOpen\": false,\r\n        \"doesBankAccountTypeMatch\": false,\r\n        \"doesInitialsMatch\": false,\r\n        \"doesIdentityNumberMatch\": false,\r\n        \"doesNameMatch\": false,\r\n        \"doesAcceptsDebits\": false,\r\n        \"doesAcceptsCredits\": false,\r\n        \"didTimeout\": false,\r\n        \"accountLengthMatch\": false,\r\n        \"missingParameter\": false,\r\n        \"doesPhoneMatch\": false,\r\n        \"doesEmailMatch\": false,\r\n        \"hasBankAccountBeenOpenForMoreThan3Months\": false,\r\n        \"responseDate\": \"0001-01-01T00:00:00\",\r\n        \"wasTestPerformed\": false,\r\n        \"isValid\": true,\r\n        \"message\": null,\r\n        \"isForcedSuccessResponse\": false\r\n    }\r\n}");
            var response = await bankClient.ValidateBankAccountAsync(validateBankAccountRequest);
            var validateBankAccountResponse = populateValidateBankAccountResponse(response);
            //Assert
            Then_ValidateValidateBankAccountResponseIsOk_AndIsNotNull_AndDataTypesIsValid(response, validateBankAccountResponse);
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
                        validateBankAccountResponse.isValid = property.Value.GetBoolean();
                        break;
                    case "shouldupdateaccounttype":
                        validateBankAccountResponse.shouldUpdateAccountType = property.Value.GetBoolean();
                        break;
                    case "overrideisvalid":
                        validateBankAccountResponse.overrideIsValid = property.Value.GetBoolean();
                        break;
                    case "message":
                        validateBankAccountResponse.message = property.Value.GetString();
                        break;
                    case "fraudsterfailure":
                        validateBankAccountResponse.fraudsterFailure = property.Value.GetInt32();
                        break;
                    case "correctbankname":
                        validateBankAccountResponse.correctBankName = property.Value.GetString();
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
                        result.wasTestResultOverridden = item.Value.GetBoolean();
                        break;
                    case "wastestperformed":
                        result.wasTestPerformed = item.Value.GetBoolean();
                        break;
                    case "isvalid":
                        result.isValid = item.Value.GetBoolean();
                        break;
                    case "message":
                        result.message = item.Value.GetString();
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
                        avsrResult.qLinkAvsrCheckId = item.Value.GetString();
                        break;
                    case "errorcode":
                        avsrResult.errorCode = item.Value.GetString();
                        break;
                    case "errordescription":
                        avsrResult.errorDescription = item.Value.GetString();
                        break;
                    case "sessionid":
                        avsrResult.sessionId = item.Value.GetString();
                        break;
                    case "accountstatusid":
                        avsrResult.accountStatusId = item.Value.GetString();
                        break;
                    case "wascachedresultused":
                        avsrResult.wasCachedResultUsed = item.Value.GetBoolean();
                        break;
                    case "wasbankaccountfound":
                        avsrResult.wasBankAccountFound = item.Value.GetBoolean();
                        break;
                    case "isbankaccountopen":
                        avsrResult.isBankAccountOpen = item.Value.GetBoolean();
                        break;
                    case "doesbankaccounttypematch":
                        avsrResult.doesBankAccountTypeMatch = item.Value.GetBoolean();
                        break;
                    case "doesinitialsmatch":
                        avsrResult.doesInitialsMatch = item.Value.GetBoolean();
                        break;
                    case "doesidentitynumbermatch":
                        avsrResult.doesIdentityNumberMatch = item.Value.GetBoolean();
                        break;
                    case "doesnamematch":
                        avsrResult.doesNameMatch = item.Value.GetBoolean();
                        break;
                    case "doesacceptsdebits":
                        avsrResult.doesAcceptsDebits = item.Value.GetBoolean();
                        break;
                    case "doesacceptscredits":
                        avsrResult.doesAcceptsCredits = item.Value.GetBoolean();
                        break;
                    case "didtimeout":
                        avsrResult.didTimeout = item.Value.GetBoolean();
                        break;
                    case "accountlengthmatch":
                        avsrResult.accountLengthMatch = item.Value.GetBoolean();
                        break;
                    case "missingparameter":
                        avsrResult.missingParameter = item.Value.GetBoolean();
                        break;
                    case "doesphonematch":
                        avsrResult.doesPhoneMatch = item.Value.GetBoolean();
                        break;
                    case "doesemailmatch":
                        avsrResult.doesEmailMatch = item.Value.GetBoolean();
                        break;
                    case "hasbankaccountbeenopenformorethan3months":
                        avsrResult.hasBankAccountBeenOpenForMoreThan3Months = item.Value.GetBoolean();
                        break;
                    case "responsedate":
                        avsrResult.responseDate = item.Value.GetDateTime();
                        break;
                    case "wastestperformed":
                        avsrResult.wasTestPerformed = item.Value.GetBoolean();
                        break;
                    case "isvalid":
                        avsrResult.isValid = item.Value.GetBoolean();
                        break;
                    case "message":
                        avsrResult.message = item.Value.GetString();
                        break;
                    case "isforcedsuccessresponse":
                        avsrResult.isForcedSuccessResponse = item.Value.GetBoolean();
                        break;
                    default:
                        TestContext.Out.WriteLine($"Unknown - AVSRResult property: {property.Name} + {property.Value}");
                        break;
                }
            }
            return avsrResult;
        }
        private void Then_ValidateValidateBankAccountResponseIsOk_AndIsNotNull_AndDataTypesIsValid(RestResponse response, ValidateBankAccountResponse validateBankAccountResponse)
        {
            // Http Status Code
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected HTTP 200 OK");
            //Response Content
            Assert.That(validateBankAccountResponse, Is.Not.Null, "Validate Bank Account Response should not be null");
            Assert.That(validateBankAccountResponse.isValid, Is.TypeOf<bool>(), "Validate Bank Account Response: isValid should be of type bool");
            Assert.That(validateBankAccountResponse.shouldUpdateAccountType, Is.TypeOf<bool>(), "Validate Bank Account Response: shouldUpdateAccountType should be of type bool");
            Assert.That(validateBankAccountResponse.overrideIsValid, Is.TypeOf<bool>(), "Validate Bank Account Response: overrideIsValid should be of type bool");
            Assert.That(validateBankAccountResponse.message, Is.TypeOf<string>(), "Validate Bank Account Response: message should be of type string");
            Assert.That(validateBankAccountResponse.fraudsterFailure, Is.TypeOf<int>(), "Validate Bank Account Response: fraudsterFailure should be of type int");
            Assert.That(validateBankAccountResponse.correctBankName, Is.Null.Or.TypeOf<string>(), "Validate Bank Account Response: correctBankName should be of type string");
            //SoftyCompResult
            Assert.That(validateBankAccountResponse.softyCompResult, Is.Not.Null, "Validate Bank Account Response: softyCompResult should not be null");
            //FraudsterResult
            Assert.That(validateBankAccountResponse.fraudsterResult, Is.Not.Null, "Validate Bank Account Response: fraudsterResult should not be null");
            //D3BlackListResult
            Assert.That(validateBankAccountResponse.d3BlackListResult, Is.Not.Null, "Validate Bank Account Response: d3BlackListResult should not be null");
            //AVSRResult
            Assert.That(validateBankAccountResponse.avsrResult, Is.Not.Null, "Validate Bank Account Response: avsrResult should not be null");
        }
    }
}
