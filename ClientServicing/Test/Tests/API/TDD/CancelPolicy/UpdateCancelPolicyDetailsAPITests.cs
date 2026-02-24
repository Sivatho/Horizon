using System.Text.Json;
using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.CancelPolicy;
using ClientServicing.Main.Resources.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace ClientServicing.Test.Tests.API.TDD.CancelPolicy
{
    [TestFixture]
    public class UpdateCancelPolicyDetailsAPITests
    {
        CancelPolicyAPIClient? cancelPolicyAPIClient = null;
        UtilitiesHelper utilityHelper = new();
        private IDataAccess _dataAccess = null!;

        public void SetUp()
        {
            cancelPolicyAPIClient = new CancelPolicyAPIClient(GlobalTestInfrastructureSetup.SharedRestLibrary);
            _dataAccess = GlobalTestInfrastructureSetup.ServiceProvider.GetRequiredService<IDataAccess>();
        }
         [Test]
         public async Task Given_UpdateCancelPolicyDetailsRequestIsValid_When_UpdateCancelPolicyDetailsAsync_Then_ValidateUpdateCancelPolicyDetailsResponseIsOk()
         {
            //Arrange
            var request = JsonSerializer.Deserialize<UpdateCancelPolicyDetailsRequest>(
                utilityHelper.ReadTestDataJson
                ("CancelPolicy", "UpdateCancelPolicyDetailsRequest.json"));
             //Act
             var response = await cancelPolicyAPIClient!.UpdateCancelPolicyDetailsAsync(request);
             //Assert
             Assert.That(response, Is.Not.Null, "Response should not be null");
             Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Status code should be 200 OK");
        }
    }
}
