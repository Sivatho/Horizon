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
    public class CanChangeBankAccountAPITest : CanChangeBankAccountIdValidationMethods
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
        public async Task GivenBankAccountId_WhenCanChangeBankAccountAsync_Then_ValidateCanChangeBankAccountResponseData_IsNotNull_And_IsTrueOrFalse_And_TypeOfString()
        {
            //Arrange
            var bankAccountRequest = JsonSerializer.Deserialize<BankAccountRequest>(
                utilitiesHelper.ReadTestDataJson("General/Data", "BankAccountIDsRequestUrlSegmentIsValid.json"));
            var bankAccountId = bankAccountRequest!.bankAccountList[0];
            ValidateCanChangeBankAccountRequestDataIsNotNullOrEmpty(bankAccountRequest);
            //Act
            var response = await bankAPIClient.CanChangeBankAccountAsync(bankAccountId);
            var canChangeBankAccountResponse = PopulateCanChangeBankAccountResponse(response);
            var schema = ResponseSchemasEnvelope.CanChangeBankAccountSchema;
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateCanChangeBankAccountResponseDataIsNotNullAndIsTrueOrFalseAndTypeOfString(canChangeBankAccountResponse);
        }

        [Test]
        public async Task GivenInvalidBankAccountId_WhenCanChangeBankAccountAsync_ThenResponseStatusCodeOK_AndResponsePropertyNameIsValid_AndDataTypesIsValid()
        {
            //Arrange
            int bankAccountId = -1;
            //Act
            var response = await bankAPIClient.CanChangeBankAccountAsync(bankAccountId);
            var canChangeBankAccountResponse = PopulateCanChangeBankAccountResponse(response);
            var schema = ResponseSchemasEnvelope.CanChangeBankAccountSchema;
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateCanChangeBankAccountResponseDataIsNotNullAndIsTrueOrFalseAndTypeOfString(canChangeBankAccountResponse);
        }
    }
}