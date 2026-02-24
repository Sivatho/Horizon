using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.AccountHistory;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.AccountHistory;
using ClientServicing.Main.Resources.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace ClientServicing.Test.Tests.API.TDD.AccountHistory
{
    [TestFixture]
    public class PolicyCashReceiptAPITests : PolicyCashReceiptValidatiomMethods
    {
        AccountingHistoryAPIClient? accountHistoryAPIClient = null;
        UtilitiesHelper utilitiesHelper = new();
        private IDataAccess _dataAccess = null!;

        [SetUp]
        public void SetUp()
        {
            accountHistoryAPIClient = new AccountingHistoryAPIClient(GlobalTestInfrastructureSetup.SharedRestLibrary);
            _dataAccess = GlobalTestInfrastructureSetup.ServiceProvider.GetRequiredService<IDataAccess>();
        }

        [Test]
        public async Task Given_PolicyCashReceiptRequestIsValid_When_PolicyCashReceiptAsync_Then_ValidatePolicyCashReceiptResponseIsOk()
        {
            //Arrange
            var request = JsonSerializer.Deserialize<PolicyAccountHistoryRequest>(
                utilitiesHelper.ReadTestDataJson
                ("AccountHistory", "PolicyAccountingHistoryRequestHasPolicy.json"));
            ValidatePolicyCashReceiptRequestPayload(request);
            //Act
            var response = await accountHistoryAPIClient.policyCashReceiptAsync(request);
            var policyCashReceiptResponse = PopulatePolicyCashReceiptResponse(response);
            var schema =  ResponseSchemasEnvelope.PolicyCashReceiptResponseSchema;
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidatePolicyCashReceiptResponsePayload(policyCashReceiptResponse);
        }
    }
}
