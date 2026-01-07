using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    [TestFixture]
    public class GetMainMemChangeHistoryAPITests : GetMainMemChangeHistoryValidationMethods
    {
        PolicyAPIClient policyAPIClient = new PolicyAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        public async Task Given_GetMainMemChangeHistoryRequestPayloadIsValid_When_GetMainMemChangeHistoryAsync_Then_ValidateResponseStatusCodeOK_And_PropertyNameIsValid_And_DataTypesIsValid_And_GetMainMemChangeHistoryResponseDataIsNotNullOrEmpty()
        {
            // Arrange
            var getMainMemChangeHistoryRequest = JsonSerializer.Deserialize<PolicyBeneficiaryDetailsRequest>
                (utilitiesHelper.ReadTestDataJson("Policy/Data", "GetPolicyAndMainMemberDetailsByPolicyNumberRequestPayload.json"));
            
            // Act
            var response = await policyAPIClient.GetMainMemChangeHistoryAsync(getMainMemChangeHistoryRequest);
            var getMainMemChangeHistoryResponse = populateGetMainMemChangeHistoryResponse(response);

            // Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCodeOK(response);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateGetMainMemChangeHistoryResponseDataIsNotNullOrEmpty(getMainMemChangeHistoryResponse);
        }
        private GetMainMemChangeHistoryResponse populateGetMainMemChangeHistoryResponse(RestResponse response)
        {
            using JsonDocument jsDoc = JsonDocument.Parse(response.Content);
            var getMainMemChangeHistoryResponse = new GetMainMemChangeHistoryResponse
            {
                executionOutcome = new ExecutionOutcome(),
                data = new List<MainMemChangeHistoryDetail>()
            };
            foreach (var property in jsDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded": getMainMemChangeHistoryResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message": getMainMemChangeHistoryResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors": getMainMemChangeHistoryResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        foreach (var item in property.Value.EnumerateArray())
                        { 
                            var mainMemChangeHistoryDetail = JsonSerializer.Deserialize<MainMemChangeHistoryDetail>(item.GetRawText());
                            getMainMemChangeHistoryResponse.data.Add(mainMemChangeHistoryDetail);
                        }
                        break;
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;
                }
            }
            return getMainMemChangeHistoryResponse;
        }
    }
}
