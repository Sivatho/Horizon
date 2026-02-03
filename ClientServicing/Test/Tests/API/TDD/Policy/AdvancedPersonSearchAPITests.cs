using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    [TestFixture]
    public class AdvancedPersonSearchAPITests : AdvancedPersonSearchValidationMethods
    {
        PolicyAPIClient policyAPIClient = new PolicyAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        [Ignore("Request Test data required results return are not correct")]
        public async Task Give_AdvancedPersonSearchRequestPayloadIsValid_When_AdvancedPersonSearchAsync_Then_ValidateResponseStatusCodeOK_And_PropertyNameIsValid_And_DataTypesIsValid_And_AdvancedPersonSearchResponseDataIsNotNull()
        {
            //Arrange
            AdvancedPersonSearchRequest? advancedPersonSearchRequest = JsonSerializer.Deserialize<AdvancedPersonSearchRequest>
                (utilitiesHelper.ReadTestDataJson("Policy/Data", "AdvancedPersonSearchRequestPayloadIsValid.json"));

            //Act
            var response = await policyAPIClient.AdvancedPersonSearchAsync(advancedPersonSearchRequest);
            var advancedPersonSearchResponse = populateAdvancedPersonSearchResponse(response);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response,HttpStatusCode.OK);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateAdvancedPersonSearchResponseDataIsNotNullOrEmpty(advancedPersonSearchResponse);

        }
    }
}
