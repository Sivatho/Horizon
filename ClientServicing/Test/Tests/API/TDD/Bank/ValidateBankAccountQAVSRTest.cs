using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Resources.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace ClientServicing.Test.Tests.API.TDD.Bank
{
    public class ValidateBankAccountQAVSRTest : ValidateBankAccountQAVSRValidationMethods
    {
        BankAPIClient bankAPIClient = null;
        UtilitiesHelper utilitiesHelper = new();
        private IDataAccess _dataAccess = null!;

        [SetUp]
        public void SetUp()
        {
            bankAPIClient = new BankAPIClient(GlobalTestInfrastructureSetup.SharedRestLibrary);
            _dataAccess = GlobalTestInfrastructureSetup.ServiceProvider.GetRequiredService<IDataAccess>();
        }
        [Test]
        public async Task GivenBankAccountQAVSRIsValid_WhenValidateBankAccountQAVSRAsync_ThenValidateResponseIsNotNullOrEmpty_And_TypeOfstring_And_IsTrueOrfalse_And_IntegerIsNotLessThan0_And_DateTimeIsNotEqualToDefaultDateTime()
        {
            //Arrange
            ValidateBankAccountQAVSRRequest validateBankAccountQAVSRRequest = JsonSerializer.Deserialize<ValidateBankAccountQAVSRRequest>(
                utilitiesHelper.ReadTestDataJson("Bank/Data", "ValidateBankAccountQAVSRRequestNotNull.json"))!;
            //ValidateValidateBankAccountQAVSRRequestDataIsNotNullOrEmptyOrLessThanZero(validateBankAccountQAVSRRequest);
            //Act
            var response = await bankAPIClient.ValidateBankAccountQAVSRAsync(validateBankAccountQAVSRRequest);
            var validateBankAccountQAVSRResponse = PopulateValidateBankAccountQAVSRResponse(response);
            var schema = ResponseSchemasEnvelope.ValidateBankAccoutntStandardEnvelope();
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateValidateBankAccountQAVSRResponsetResponseDataIsNotNullOrEmptyOrLessThanZero(validateBankAccountQAVSRResponse);
        }
    }
}