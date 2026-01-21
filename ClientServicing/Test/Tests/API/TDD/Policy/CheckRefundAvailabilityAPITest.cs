using ClientServicing.Main.Controller;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    [TestFixture]
    public class CheckRefundAvailabilityAPITest
    {
        PolicyAPIClient policyAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();

        [Test]
        [Ignore("API is still in development. Could not find stored procedure 'polmaspr.spCheckRefundAvailability'.")]
        public async Task Given_CheckRefundAvailabilityRequestPayloadIsValid_When_CheckRefundAvailabilityAsync_Then_ValidateCheckRefundAvailabilityResponseIsOk()
        {
            // Arrange
            var request = utilitiesHelper.ReadTestDataJson("Policy/Data", "CheckRefundAvailabilityRequestPayloadIsValid.json");
            
            // Act
            var response = await policyAPIClient.CheckRefundAvailabilityAsync(request);
            
            // Assert
            Assert.That(response.IsSuccessStatusCode, Is.True, "Response should indicate success.");
            Console.WriteLine("Validated: Response Status Code: 200; Status Description: 'OK' as expected.");
        }
    }
}
