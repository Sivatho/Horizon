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
    public class GetCustomerPolicyInfoByEntityNoAPITests : GetCustomerPolicyInfoByEntityValidationMethods
    {
        PolicyAPIClient policyAPIClient = new PolicyAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        [Ignore("API call failed with status code: InternalServerError and message: Error converting data type varchar to int")]
        public async Task Given_GetCustomerPolicyInfoByEntityNoRequestPayloadIsValid_When_GetCustomerPolicyInfoByEntityNoAsync_Then_ValidateResponseStatusCodeOK_And_ResponsePropertyNameIsValid_And_DataTypesIsValid_And_GetCustomerPolicyInfoByEntityResponseDataIsNotNull()
        {
            //Arrange
            var getCustomerPolicyInfoByEntityNoRequest = JsonSerializer.Deserialize<GetCustomerPolicyInfoByEntityNoRequest>
                (utilitiesHelper.ReadTestDataJson("Policy/Data", "GetCustomerPolicyInfoByEntityNoRequestPayloadIsValid.json"));

            //Act
            var response = await policyAPIClient.GetCustomerPolicyInfoByEntityNoAsync(getCustomerPolicyInfoByEntityNoRequest);
            GetCustomerPolicyInfoByEntityResponse getCustomerPolicyInfoByEntityResponse = populateGetCustomerPolicyInfoByEntityResponse(response);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCodeOK(response);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidateGetCustomerPolicyInfoByEntityResponseDataIsNotNull(getCustomerPolicyInfoByEntityResponse);
        }
        private GetCustomerPolicyInfoByEntityResponse populateGetCustomerPolicyInfoByEntityResponse(RestResponse response)
        {
            using JsonDocument jsDoc = JsonDocument.Parse(response.Content);
            var getCustomerPolicyInfoByEntityResponse = new GetCustomerPolicyInfoByEntityResponse
            {
                executionOutcome = new ExecutionOutcome(),
                data = new List<CusomterPolicyInfo>()
            };
            foreach (var property in jsDoc.RootElement.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "succeeded": getCustomerPolicyInfoByEntityResponse.executionOutcome.succeeded = (bool)utilitiesHelper.ReadBooleanNullable(property.Value); break;
                    case "message": getCustomerPolicyInfoByEntityResponse.executionOutcome.message = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "errors": getCustomerPolicyInfoByEntityResponse.executionOutcome.errors = utilitiesHelper.ReadStringNullable(property.Value); break;
                    case "data":
                        foreach (var item in property.Value.EnumerateArray())
                        {
                            var customerPolicyInfo = new CusomterPolicyInfo
                            {
                                policyNo = (int)utilitiesHelper.ReadInt32Nullable(item.GetProperty("policyNo")),
                                ifaNo = (int?)utilitiesHelper.ReadInt32Nullable(item.GetProperty("ifaNo")),
                                channelDescr = utilitiesHelper.ReadStringNullable(item.GetProperty("ifaNo")),
                                productDescr = utilitiesHelper.ReadStringNullable(item.GetProperty("productDescr")),
                                planTypeDescr = utilitiesHelper.ReadStringNullable(item.GetProperty("planTypeDescr")),
                                policyStatus = utilitiesHelper.ReadStringNullable(item.GetProperty("policyStatus")),
                                statusCd = (int)utilitiesHelper.ReadInt32Nullable(item.GetProperty("statusCd")),
                                dateOfCommencement = (DateTime)utilitiesHelper.ReadDateTimeNullable(item.GetProperty("dateOfCommencement")),
                                payer = utilitiesHelper.ReadStringNullable(item.GetProperty("payer")),
                                policyPremium = (int)utilitiesHelper.ReadInt32Nullable(item.GetProperty("policyPremium")),
                                billedTo = (DateTime)utilitiesHelper.ReadDateTimeNullable(item.GetProperty("billedTo")),
                                paidTo = (DateTime)utilitiesHelper.ReadDateTimeNullable(item.GetProperty("paidTo")),
                                premiumCount = (int)utilitiesHelper.ReadInt32Nullable(item.GetProperty("premiumCount")),
                                premiumFrequency = (int)utilitiesHelper.ReadInt32Nullable(item.GetProperty("premiumFrequency")),
                                salesPerson = utilitiesHelper.ReadStringNullable(item.GetProperty("salesPerson")),
                                debiCheckStatus = utilitiesHelper.ReadStringNullable(item.GetProperty("debiCheckStatus")),
                                legacyPolicyNo = utilitiesHelper.ReadStringNullable(item.GetProperty("legacyPolicyNo")),
                                statusDate = (DateTime)utilitiesHelper.ReadDateTimeNullable(item.GetProperty("statusDate")),
                                partnerCD = (int)utilitiesHelper.ReadInt32Nullable(item.GetProperty("partnerCD")),
                                inspiratorNo = (int)utilitiesHelper.ReadInt32Nullable(item.GetProperty("inspiratorNo"))
                            };
                        }
                        break;
                    default: TestContext.Out.WriteLine($"Unknown property: {property.Name}"); break;
                }
            }
            return getCustomerPolicyInfoByEntityResponse;
        }
    }
}
