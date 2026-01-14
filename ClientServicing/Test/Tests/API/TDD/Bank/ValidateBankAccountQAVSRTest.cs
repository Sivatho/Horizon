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
        BankAPIClient bankAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();

        [Test]
        public async Task GivenBankAccountQAVSRIsValid_WhenValidateBankAccountQAVSRAsync_ThenValidateResponseIsOk_AndIsNotNull_AndDataTypesIsValid()
        {
            //Arrange
            ValidateBankAccountQAVSRRequest validateBankAccountQAVSRRequest = JsonSerializer.Deserialize<ValidateBankAccountQAVSRRequest>(utilitiesHelper.ReadTestDataJson("Bank/Data", "ValidateBankAccountQAVSRRequestNotNull.json"));

            //Act
            var response = await bankAPIClient.ValidateBankAccountQAVSRAsync(validateBankAccountQAVSRRequest);
            var validateBankAccountQAVSRResponse = populateValidateBankAccountQAVSRResponse(response);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCodeOK(response);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateResponseSchemaIsValid(response, "Bank/Schema", "ValidateBankAccountQAVSRResponseSchema.json");
            ValidateValidateBankAccountQAVSRResponsetResponseDataIsNotNullOrEmptyOrLessThanZero(validateBankAccountQAVSRResponse);
        }
    }
}