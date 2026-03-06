using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Bank;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.Bank;
using ClientServicing.Main.Resources.Helper;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.Bank
{
    public class ValidateAccountNumberUsageLimitAPITest : ValidateAccountNumberUsageLimitValidationMethods
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
        public async Task Given_AccountNumberUsageLimitIsValid_When_ValidateAccountNumberUsageLimitAsync_Then_ValidateResponseStatusCodeOK_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty()
        {
            //Arrange
            var request = JsonSerializer.Deserialize<List<EntityBankAccountDetails>>(
                utilitiesHelper.ReadTestDataJson("Bank\\Data", "EntityBankAccountDetailsIsValid.json"));
            int randomIndex = new Random().Next(0, request!.Count);
            string accountNumber = request![randomIndex].BankAccNo;

            //Act
            var response = await bankAPIClient.ValidateAccountNumberUsageLimitAsync(accountNumber);
            var validateAccountNumberUsageLimitResponse = PopulateValidateAccountNumberUsageLimitResponse(response);
            var schema = PolicySchemas.PolicyStatusBody();
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateValidateAccountNumberUsageLimitResponseDataIsNotNullOrEmpty(validateAccountNumberUsageLimitResponse);
        }
    }
}
