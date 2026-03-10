using System.Net;
using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.BeneficiaryDetails;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.JsonValidation;
using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Resources.Helper;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.BeneficiaryDetails
{
    public class UpdatePolicyBeneficiaryCacheAPITest: UpdatePolicyBeneficiaryCacheValidationMethods
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
        public async Task Given_BeneficiaryDetailsData_When_UpdatePolicyBeneficiaryCacheAsync_Then_ValidateFetchBankResponseIsOk_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid()
        {
            //Arrange
            var json = utilitiesHelper.ReadTestDataJson("BeneficiaryDetails/Data", "UpdatePolicyBeneficiaryCacheRequestPayloadIsValid.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var beneficiaryDetailsData = JsonSerializer.Deserialize<BeneficiaryDetailsData>(json, options);
            var schema = ResponseSchemas.StandardResponseDataBoolSchema();
            //Act
            var response = await beneficiaryDetailsAPIClient!.UpdatePolicyBeneficiaryCacheAsync(beneficiaryDetailsData!);
            var updatePolicyBenefitCacheResponse = PopulateUpdatePolicyBenefitciaryResponse(response);
            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
            ValidateResponseHeadersAreValid(response);
            ValidateResponsePropertyNameIsValidAndDataTypesIsValid(response, schema);
            ValidateResponseShouldMatchSchema(response, schema);
            ValidateUpdatePolicyBenefitciaryCacheResponseDataIsNotNullOrEmpty(updatePolicyBenefitCacheResponse);
        }
    }
}
 