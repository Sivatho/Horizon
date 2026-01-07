using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    [TestFixture]
    public class CheckRestartEligibilityAPITests
    {
        PolicyAPIClient policyAPIClient = new PolicyAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        public async Task Given_CheckRestartEligibilityRequestPayloadIsValid_When_CheckRestartEligibilityAsync_Then() {
            //Arrange
            var checkRestartEligibilityRequest = JsonSerializer.Deserialize<CheckRestartEligibilityRequest>
                (utilitiesHelper.ReadTestDataJson("Policy/Data", "CheckRestartEligibilityRequestPayloadIsValid.json"));
            //Act
            var response = await policyAPIClient.CheckRestartEligibilityAsync (checkRestartEligibilityRequest);
            var checkRestartEligibilityResponse = populateCheckRestartEligibilityResponse(response);
            // Assert
            /*
            ValidationAssertionHeading();
            ValidateResponseStatusCodeOK(response);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateCheckRestartEligibilityResponseDataIsNotNullOrEmpty(checkRestartEligibilityResponse);
            */
        }
        private CheckRestartEligibilityResponse populateCheckRestartEligibilityResponse(RestResponse response)
        {
            using JsonDocument jsDoc = JsonDocument.Parse(response.Content);
            var checkRestartEligibilityResponse = new CheckRestartEligibilityResponse
            {
                executionOutcome = new ExecutionOutcome(),
                data = new CheckRestartEligibilityData()
            };
            foreach (var property in jsDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded": checkRestartEligibilityResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message": checkRestartEligibilityResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors": checkRestartEligibilityResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data": 
                        foreach(var item in property.Value.EnumerateObject())
                        {
                            switch (item.Name)
                            {
                                case "isEligibile": checkRestartEligibilityResponse.data.isEligibile = (bool)utilitiesHelper.ReadBooleanNullable(item.Value); break;
                                case "message": checkRestartEligibilityResponse.data.message = utilitiesHelper.ReadStringNullable(item.Value); break;
                                default: TestContext.Out.WriteLine($"Unknown property in data: {item.Name}"); break;
                            }
                        }
                        break;
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;
                }
            }
            return checkRestartEligibilityResponse;
        }
    }
}
