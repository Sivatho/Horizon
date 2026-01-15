using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class EntityInfoUpsertAPITests : EntityInfoUpsertValidationMethods
    {
        PolicyAPIClient policyAPIClient = new PolicyAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        public async Task Given_EntityInfoUpsertrRequestPayloadIsValid_When_EntityInfoUpsertAsync_Then_ValidateResponseStatusCodeOK_And_PropertyNameIsValid_And_DataTypesIsValid_And_EntityInfoUpsertResponseDataIsNotNullOrEmpty() {
            //Arrange
            var entityInfoUpsertRequest = JsonSerializer.Deserialize<EntityInfoUpsertRequest>
                (utilitiesHelper.ReadTestDataJson("Policy/Data", "EntityInfoUpsertrRequestPayloadIsValid.json"));
            
            //Act
            var response = await policyAPIClient.EntityInfoUpsertAsync(entityInfoUpsertRequest);
            var entityInfoUpsertResponse = populateEntityInfoUpsertResponse(response);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateEntityInfoUpsertResponseDataIsNotNullOrEmpty(entityInfoUpsertResponse);
        }
        private PolicyEntityInfoUpsertResponse populateEntityInfoUpsertResponse(RestResponse response)
        {
            using JsonDocument jsDoc = JsonDocument.Parse(response.Content);
            var entityInfoUpsertResponse = new PolicyEntityInfoUpsertResponse
            {
                executionOutcome = new ExecutionOutcome()
            };
            foreach (var property in jsDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":   entityInfoUpsertResponse.executionOutcome.succeeded =   (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message":     entityInfoUpsertResponse.executionOutcome.message =     utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors":      entityInfoUpsertResponse.executionOutcome.errors =      utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":        entityInfoUpsertResponse.data =                         (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;
                }
            }
            return entityInfoUpsertResponse;
        }

    }
}
