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
    public class GetInsuredWithBenefitAPITest : GetInsuredWithBenefitValidationMethods
    {
        BeneficiaryDetailsAPIClient? beneficiaryDetailsAPIClient = null;
        UtilitiesHelper utilitiesHelper = new();
        private IDataAccess _dataAccess = null!;

        [SetUp]
        public void SetUp()
        {
            beneficiaryDetailsAPIClient = new BeneficiaryDetailsAPIClient(GlobalTestInfrastructureSetup.SharedRestLibrary);
            _dataAccess = GlobalTestInfrastructureSetup.ServiceProvider.GetRequiredService<IDataAccess>();
        }
        [Test]
        public async Task Given_GetInsuredWithBenefitRequest_When_GetInsuredWithBenefit_Then_ValidateResponseStatusCodeOK_And_IsNotNull_And_IsTrueOrFalse_And_TypeOfString_And_IsNotLessThanOrEqualTo0_And_SchemaIsValid()
        {
            //Arrange
            var getInsuredWithBenefitRequest = JsonSerializer.Deserialize<GetInsuredWithBenefitRequest>(
                utilitiesHelper.ReadTestDataJson("BeneficiaryDetails/Data", "GetInsuredWithBenefitRequestPayloadIsValid.json"))!;
            var schema = ResponseSchemasEnvelope.BenefitCoversSchema;
            ValidateGetInsuredWithBenefitRequestIsNotNullOrEmpty(getInsuredWithBenefitRequest);
            //Act
            var response = await beneficiaryDetailsAPIClient!.GetInsuredWithBenefit(getInsuredWithBenefitRequest);
            var getInsuredWithBenefitResponse = PopulateGetInsuredWithBenefitResponseData(response);
            //Assert           
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateGetInsuredWithBenefitDataIsNotNull_And_IsTrueOrFalse_And_TypeOfString_And_IsNotLessThanOrEqualTo0(getInsuredWithBenefitResponse);
        }
    }
}