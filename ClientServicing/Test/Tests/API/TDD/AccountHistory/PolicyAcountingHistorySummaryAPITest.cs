using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.AccountHistory;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.AccountHistory;
using ClientServicing.Main.Resources.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace ClientServicing.Test.Tests.API.TDD.AccountHistory
{
    public class PolicyAcountingHistorySummaryAPITest : PolicyAcountingHistorySummaryValidationMethods
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
        public async Task Given_PolicyAccountingHistorySummaryIsValid_When_PolicyAccountingHistorySummaryAsync_Then_() {
            //Arrange
            var request = JsonSerializer.Deserialize<PolicyAccountHistorySummaryRequest>(
                utilitiesHelper.ReadTestDataJson
                ("AccountHistory", "PolicyAccountingHistorySummary.json")
            );
            ValidatePolicyAccountHistorySummaryRequestPayload(request);
            //Act
            var response = await accountHistoryAPIClient.policyAccountingHistorySummaryAsync(request);
            var policyAccountHistorySummaryResponse = PopulatePolicyAccountHistorySummaryResponse(response);
            var schema = ResponseSchemasEnvelope.policyAccountHistorySummaryResponseSchema;
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidatePolicyAccountHistorySummaryResponsePayload(policyAccountHistorySummaryResponse);
        }
    }
}
