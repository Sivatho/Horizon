using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.AccountHistory.PolicyAccountingHistory;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.AccountHistory;
using ClientServicing.Main.Resources.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace ClientServicing.Test.Tests.API.TDD.AccountHistory
{
    [TestFixture]
    public class AccountHistoryAPITest : PolicyAccountingHistoryValidationMethods
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
        public async Task GivenAccountHistoryRequestHasPolicyNO_WhenpolicyAcountingHistoryAsync_ThenValidateAccountHistoryResponseIsOk()
        {
            //Arrange
            var request = JsonSerializer.Deserialize<PolicyAccountHistoryRequest>(
                utilitiesHelper.ReadTestDataJson
                ("AccountHistory", "PolicyAccountingHistoryRequestHasPolicy.json")
            );
            ValidatePolicyAccountingHistoryRequestDataIsNotNullOrEmpty(request);
            //Act
            var response = await accountHistoryAPIClient.policyAccountingHistoryAsync<PolicyAccountHistoryRequest>(request);
            var policyAccountHistoryResponse = populatePolicyAccountHistoryResponse(response);
            var schema = ResponseSchemasEnvelope.policyAccountHistoryResponseSchema;

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidatePolicyAccountingHistorResponseDataIsNotNullOrEmpty(policyAccountHistoryResponse);
        }
    }
}

