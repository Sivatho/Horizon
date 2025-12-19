using System.Text.Json;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.BeneficiaryDetails;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.BeneficiaryDetails;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.BeneficiaryDetails
{
    public class GetInsuredWithBenefitAPITest : GetInsuredWithBenefitValidationMethods
    {
        BeneficiaryDetailsAPIClient beneficiaryDetailsAPIClient = new BeneficiaryDetailsAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        //Review business login (benefitCover should not be allocated for beneficiary)
        public async Task Given_GetInsuredWithBenefitRequest_When_GetInsuredWithBenefit_Then_ValidateResponseStatusCodeOK_And_PropertyNameIsValid_And_DataTypesIsValid_And_IsNotNullOrEmpty_And_SchemaIsValid()
        {
            //Arrange
            GetInsuredWithBenefitRequest getInsuredWithBenefitRequest = JsonSerializer.Deserialize<GetInsuredWithBenefitRequest>(utilitiesHelper.ReadTestDataJson("BeneficiaryDetails/Data", "GetInsuredWithBenefitRequestPayloadIsValid.json"));

            //Act
            var response = await beneficiaryDetailsAPIClient.GetInsuredWithBenefit(getInsuredWithBenefitRequest);
            var getInsuredWithBenefitResponse = populateGetInsuredWithBenefitResponseData(response);

            //Assert           
            ValidationAssertionHeading();
            ValidateResponseStatusCodeOK(response);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateGetInsuredWithBenefitDataIsNotNullOrEmpty(getInsuredWithBenefitResponse);
            ValidateResponseSchemaIsValid(response, "BeneficiaryDetails/Schema", "GetInsuredWithBenefitResponseSchema.json");
        }
        public GetInsuredWithBenefitResponse populateGetInsuredWithBenefitResponseData(RestResponse restResponse)
        {
            using JsonDocument jsonDoc = JsonDocument.Parse(restResponse.Content);
            GetInsuredWithBenefitResponse getInsuredWithBenefitResponse = new GetInsuredWithBenefitResponse
            {
                executionOutcome = new ExecutionOutcome()
            };

            foreach (var property in jsonDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded":
                        getInsuredWithBenefitResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message":
                        getInsuredWithBenefitResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors":
                        getInsuredWithBenefitResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        var dataElement = property.Value;
                        var items = new List<BenefitData>();
                        switch (dataElement.ValueKind)
                        {
                            case JsonValueKind.Array:

                                foreach (var dataProperty in dataElement.EnumerateArray())
                                {
                                    var benefitData = new BenefitData();
                                    foreach (var item in dataProperty.EnumerateObject())
                                    {
                                        switch (item.Name)
                                        {
                                            case "benefitID":
                                                benefitData.benefitID = (int)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                            case "benefitCover":
                                                benefitData.benefitCover = (double)utilitiesHelper.ReadInt32Nullable(item.Value); break;
                                        }
                                    }
                                    items.Add(benefitData);
                                }
                                break;
                        }
                        getInsuredWithBenefitResponse.data = items;
                        break;
                    default:
                        TestContext.Out.WriteLine($"Unknown property: {property.Name}");
                        break;
                }
            }
            return getInsuredWithBenefitResponse;
        }
    }
}
