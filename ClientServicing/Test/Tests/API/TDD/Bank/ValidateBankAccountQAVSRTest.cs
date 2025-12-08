using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.Bank
{
    public class ValidateBankAccountQAVSRTest : ValidateBankAccountQAVSRValidationMethods
    {
        UtilitiesHelper utilitiesHelper = new();

        [Test]
        public async Task GivenBankAccountQAVSRIsValid_WhenValidateBankAccountQAVSRAsync_ThenValidateResponseIsOk_AndIsNotNull_AndDataTypesIsValid()
        {
            //Arrange
            BankAPIClient bankClient = new("https://horizon.clientele.co.za/horizon.clientservicing/");
            ValidateBankAccountQAVSRRequest validateBankAccountQAVSRRequest = JsonSerializer.Deserialize<ValidateBankAccountQAVSRRequest>(utilitiesHelper.ReadJson("Bank/Data", "ValidateBankAccountQAVSRRequestNotNull.json"));

            //Act
            var response = await bankClient.ValidateBankAccountQAVSRAsync(validateBankAccountQAVSRRequest);
            var validateBankAccountQAVSRResponse = populateValidateBankAccountQAVSRResponse(response);

            //Assert
            TestContext.Out.WriteLine("\n======================================================================\nAssertion Results:");
            ValidateHTTPResponseStatusCodeOK(response);
            ValidateResponsePropertyNameIsValid_AndDataTypesIsValid(response);
            ValidateResponseSchemaIsValid(response, "Bank/Schema", "ValidateBankAccountQAVSRResponseSchema.json");
            
        }
        private ValidateBankAccountQAVSRResponse populateValidateBankAccountQAVSRResponse(RestResponse response)
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
                    case "isvalid":
                        validateBankAccountQAVSRResponse.isValid = property.Value.GetBoolean();
                        break;
                    case "shouldupdateaccounttype":
                        validateBankAccountQAVSRResponse.shouldUpdateAccountType = property.Value.GetBoolean();
                        break;
                    case "overrideisvalid":
                        validateBankAccountQAVSRResponse.overrideIsValid = property.Value.GetBoolean();
                        break;
                    case "message":
                        validateBankAccountQAVSRResponse.message = property.Value.GetString();
                        break;
                    case "fraudsterfailure":
                        validateBankAccountQAVSRResponse.fraudsterFailure = property.Value.GetInt32();
                        break;
                    case "softycompresult":
                        var softyCompResult = new TestResult();
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name) {
                                case "wasTestResultOverridden":
                                    softyCompResult.wasTestResultOverridden = item.Value.GetBoolean();
                                    break;
                                case "wasTestPerformed":
                                    softyCompResult.wasTestPerformed = item.Value.GetBoolean();
                                    break;
                                case "isValid":
                                    softyCompResult.isValid = item.Value.GetBoolean();
                                    break;
                                case "message":
                                    softyCompResult.message = item.Value.GetString() ?? string.Empty;
                                    break;
                            }
                            validateBankAccountQAVSRResponse.softyCompResult = softyCompResult;
                        }
                        break;
                    case "fraudsterresult":
                        var fraudsterResult = new TestResult();
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch(item.Name) {
                                case "wasTestResultOverridden":
                                    fraudsterResult.wasTestResultOverridden = item.Value.GetBoolean();
                                    break;
                                case "wasTestPerformed":
                                    fraudsterResult.wasTestPerformed = item.Value.GetBoolean();
                                    break;
                                case "fraudsterFailureType":
                                    fraudsterResult.fraudsterFailureType = item.Value.GetInt32();
                                    break;
                                case "isValid":
                                    fraudsterResult.isValid = item.Value.GetBoolean();
                                    break;
                                case "message":
                                    fraudsterResult.message = item.Value.GetString() ?? string.Empty;
                                    break;
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
                                case "wasTestResultOverridden":
                                    d3BlackListResult.wasTestResultOverridden = item.Value.GetBoolean();
                                    break;
                                case "wasTestPerformed":
                                    d3BlackListResult.wasTestPerformed = item.Value.GetBoolean();
                                    break;
                                case "isValid":
                                    d3BlackListResult.isValid = item.Value.GetBoolean();
                                    break;
                                case "message":
                                    d3BlackListResult.message = item.Value.GetString() ?? string.Empty;
                                    break;
                            }
                            validateBankAccountQAVSRResponse.d3BlackListResult = d3BlackListResult;
                        }
                                break;
                    case "avsrresult":
                        var avsrResult = new AVSRResult();
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name) {
                                case "qLinkAvsrCheckId":
                                    avsrResult.qLinkAvsrCheckId = item.Value.GetString();
                                    break;
                                case "errorCode":
                                    avsrResult.errorCode = item.Value.GetString();
                                    break;
                                case "errorDescription":
                                    avsrResult.errorDescription = item.Value.GetString();
                                    break;
                                case "sessionId":
                                    avsrResult.sessionId = item.Value.GetString();
                                    break;
                                case "accountStatusId":
                                    avsrResult.accountStatusId = item.Value.GetString();
                                    break;
                                case "wasCachedResultUsed":
                                    avsrResult.wasCachedResultUsed = item.Value.GetBoolean();
                                    break;
                                case "wasBankAccountFound":
                                    avsrResult.wasBankAccountFound = item.Value.GetBoolean();
                                    break;
                                case "isBankAccountOpen":
                                    avsrResult.isBankAccountOpen = item.Value.GetBoolean();
                                    break;
                                case "doesBankAccountTypeMatch":
                                    avsrResult.doesBankAccountTypeMatch = item.Value.GetBoolean();
                                    break;
                                case "doesInitialsMatch":
                                    avsrResult.doesInitialsMatch = item.Value.GetBoolean();
                                    break;
                                case "doesIdentityNumberMatch":
                                    avsrResult.doesIdentityNumberMatch = item.Value.GetBoolean();
                                    break;
                                case "doesNameMatch":
                                    avsrResult.doesNameMatch = item.Value.GetBoolean();
                                    break;
                                case "doesAcceptsDebits":
                                    avsrResult.doesAcceptsDebits = item.Value.GetBoolean();
                                    break;
                                case "doesAcceptsCredits":
                                    avsrResult.doesAcceptsCredits = item.Value.GetBoolean();
                                    break;
                                case "didTimeout":
                                    avsrResult.didTimeout = item.Value.GetBoolean();
                                    break;
                                case "accountLengthMatch":
                                    avsrResult.accountLengthMatch = item.Value.GetBoolean();
                                    break;
                                case "missingParameter":
                                    avsrResult.missingParameter = item.Value.GetBoolean();
                                    break;
                                case "doesPhoneMatch":
                                    avsrResult.doesPhoneMatch = item.Value.GetBoolean();
                                    break;
                                case "doesEmailMatch":
                                    avsrResult.doesEmailMatch = item.Value.GetBoolean();
                                    break;
                                case "hasBankAccountBeenOpenForMoreThan3Months":
                                    avsrResult.hasBankAccountBeenOpenForMoreThan3Months = item.Value.GetBoolean();
                                    break;
                                case "responseDate":
                                    avsrResult.responseDate = item.Value.GetDateTime();
                                    break;
                                case "wasTestPerformed":
                                    avsrResult.wasTestPerformed = item.Value.GetBoolean();
                                    break;
                                case "isValid":
                                    avsrResult.isValid = item.Value.GetBoolean();
                                    break;
                                case "message":
                                    avsrResult.message = item.Value.GetString();
                                    break;
                                case "isForcedSuccessResponse":
                                    avsrResult.isForcedSuccessResponse = item.Value.GetBoolean();
                                    break;
                            }
                           validateBankAccountQAVSRResponse.avsrResult = avsrResult;
                        }
                        break;
                    case "correctbankaccounttype":
                        var correctBankAccountType = new CorrectBankAccountType();
                        foreach (var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name) {
                                case "type":
                                    correctBankAccountType.type = item.Value.GetInt32();
                                    break;
                            }                            
                        }
                        break;
                    case "correctbankname":
                        validateBankAccountQAVSRResponse.correctBankName = property.Value.GetString() ?? string.Empty;
                        break;
                    default:
                        TestContext.Out.WriteLine($"Unkown Property: {property.Name}");
                        break;
                }                
            }
            return validateBankAccountQAVSRResponse;
        }
    }
}