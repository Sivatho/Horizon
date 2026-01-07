using System.Text.Json;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.GSD;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.GSD
{
    [TestFixture]
    public class EmployeeEnquiryAPITests
    {
        GSDAPIClient gsdAPIClient = new GSDAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        [Ignore("HttpRequestException Bad Request : Bad Request\"\r\n    API call failed with status code: InternalServerError and message: \"HttpRequestException Bad Request : Bad Request\"")]
        public async Task Given_EmployeeEnquiryRequestPayloadIsValid_When_EmployeeEnquiryAsync_Then()
        {
            //Arrange
            Guid newGuid = Guid.NewGuid();
            EmployeeEnquiryRequest? employeeEnquiryRequest = JsonSerializer.Deserialize
                <EmployeeEnquiryRequest>
                (utilitiesHelper.ReadTestDataJson("GSD/Data",
                "EmployeeEnquiryRequestPayloadIsValid.json"));
            employeeEnquiryRequest.requestId = newGuid.ToString();

            //Act
            var response = await gsdAPIClient.EmployeeEnquiryAsync(employeeEnquiryRequest);

            //Assert
        }
    }
}
