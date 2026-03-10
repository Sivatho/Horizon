using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.BeneficiaryDetails;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Resources.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace ClientServicing.Test.Tests.API.TDD.BeneficiaryDetails
{
    public class GetAndCachePolicyBeneficiaryDetailsAPITest : PolicyBeneficiaryDetailsValidationMethods
    {
        BeneficiaryDetailsAPIClient? beneficiaryDetailsAPIClient = null ;
        UtilitiesHelper utilitiesHelper = new();
        private IDataAccess _dataAccess = null!;

        [SetUp]
        public void SetUp()
        {
            beneficiaryDetailsAPIClient = new BeneficiaryDetailsAPIClient(GlobalTestInfrastructureSetup.SharedRestLibrary);
            _dataAccess = GlobalTestInfrastructureSetup.ServiceProvider.GetRequiredService<IDataAccess>();
        }

        [Test]
        public async Task Given_PolicyBeneficiaryDetailsRequest_When_GetAndCachePolicyBeneficiaryDetailsAsync_Then_ValidateResponseStatusCodeOK_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid() {
            //Arrange
            var beneficiaryDetailsRequest = JsonSerializer.Deserialize<PolicyBeneficiaryDetailsRequest>(
                utilitiesHelper.ReadTestDataJson("BeneficiaryDetails/Data", "PolicyBeneficiaryDetailsPayloadIsValid.json"))!;
            ValidatePolicyBeneficiaryDetailsRequestIsNotNullOrEmpty_And_GreaterThanOrEqualToZeroOrTypeOfString_And_IsNullOrTypeOfString(beneficiaryDetailsRequest);
            var schema = ResponseSchemasEnvelope.PolicyBeneficiaryDetailsSchema;
            //Act
            var response = await beneficiaryDetailsAPIClient!.GetAndCachePolicyBeneficiaryDetailsAsync(beneficiaryDetailsRequest);
            var getAndCachePolicyBeneficiaryDetailsResponse = PopulatePolicyBeneficiaryDetailsResponse(response);
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidatePolicyBeneficiaryDetailsResponseIsNotNullOrEmpty_And_IsTrueOrFalse_And_IsNullOrTypeOfString_And_IntergerIsNotLessThan0(getAndCachePolicyBeneficiaryDetailsResponse);            
        }
        [Test]
        public async Task Given_PolicyBeneficiaryDetailsByPolicyNoPayloadIsValid_When_GetAndCachePolicyBeneficiaryDetailsAsync_Then_ValidateResponseStatusCodeOK_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid()
        {
            //Arrange
            var beneficiaryDetailsRequest = JsonSerializer.Deserialize<PolicyBeneficiaryDetailsRequest>(
                utilitiesHelper.ReadTestDataJson("BeneficiaryDetails/Data", "PolicyBeneficiaryDetailsByPolicyNoPayloadIsValid.json"));
            ValidatePolicyBeneficiaryDetailsRequestIsNotNullOrEmpty_And_GreaterThanOrEqualToZeroOrTypeOfString_And_IsNullOrTypeOfString(beneficiaryDetailsRequest);
            var schema = ResponseSchemasEnvelope.PolicyBeneficiaryDetailsSchema;

            //Act
            var response = await beneficiaryDetailsAPIClient!.GetAndCachePolicyBeneficiaryDetailsAsync(beneficiaryDetailsRequest);
            var getAndCachePolicyBeneficiaryDetailsResponse = PopulatePolicyBeneficiaryDetailsResponse(response);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidatePolicyBeneficiaryDetailsResponseIsNotNullOrEmpty_And_IsTrueOrFalse_And_IsNullOrTypeOfString_And_IntergerIsNotLessThan0(getAndCachePolicyBeneficiaryDetailsResponse);          
        }
        [Test]
        [Ignore("Status Code: 400, Title: One or more validation errors occurred; Errors: (policyNo:The JSON value could not be converted to System.Int32.")]
        public async Task Given_PolicyBeneficiaryDetailsByLegacyPolicyNumberPayloadIsInvalid_When_GetAndCachePolicyBeneficiaryDetailsAsync_Then_ValidateResponseStatusCodeOK_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid()
        {
            //Arrange
            var beneficiaryDetailsRequest = JsonSerializer.Deserialize<PolicyBeneficiaryDetailsRequest>(
                utilitiesHelper.ReadTestDataJson("BeneficiaryDetails/Data", "PolicyBeneficiaryDetailsByLegacyPolicyNumberPayloadIsInvalid.json"));
            ValidatePolicyBeneficiaryDetailsRequestIsNotNullOrEmpty_And_GreaterThanOrEqualToZeroOrTypeOfString_And_IsNullOrTypeOfString(beneficiaryDetailsRequest);
            //Act
            var response = await beneficiaryDetailsAPIClient!.GetAndCachePolicyBeneficiaryDetailsAsync(beneficiaryDetailsRequest);
            var getAndCachePolicyBeneficiaryDetailsResponse = PopulatePolicyBeneficiaryDetailsResponse(response);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.BadRequest);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidatePolicyBeneficiaryDetailsResponseIsNotNullOrEmpty_And_IsTrueOrFalse_And_IsNullOrTypeOfString_And_IntergerIsNotLessThan0(getAndCachePolicyBeneficiaryDetailsResponse);
            ValidateResponseSchemaIsValid(response, "BeneficiaryDetails/Schema", "BeneficiaryDetailsResponseSchema.json");

        }
    }
}
