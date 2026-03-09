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
    [TestFixture]
    public class ValidateBankAccountAPITest : ValidateBankAccountResponseValidationMethods
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
        public async Task GivenBankAccountIsValid_WhenValidateBankAccountAsync_ThenValidateResponseIsOk_AndIsNotNull_AndDataTypesIsValid()
        {
            //Arrange
            var validateBankAccountRequest = JsonSerializer.Deserialize<ValidateBankAccountRequest>(
                utilitiesHelper.ReadTestDataJson("Bank/Data", "ValidateBankAccountRequest_Valid.json"));
            ValidateValidateBankAccountRequestDataIsNotNullOrEmpty(validateBankAccountRequest!);
            //Act
            var response = await bankAPIClient.ValidateBankAccountAsync(validateBankAccountRequest!);
            var validateBankAccountResponse = PopulateValidateBankAccountResponse(response);
            var schema = ResponseSchemasEnvelope.ValidateBankAccoutntStandardEnvelope();
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateValidateBankAccountResponseDataIsNotNullOrEmpty(validateBankAccountResponse);
        }
    }
}
