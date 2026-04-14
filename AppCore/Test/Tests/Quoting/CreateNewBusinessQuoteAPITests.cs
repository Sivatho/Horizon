using System.Text.Json;
using AppCore.Main.Controller;
using AppCore.Main.Model.Quoting;
using ClientServicing.Main.Resources.Helper;

namespace AppCore.Test.Tests.Quoting
{
    [TestFixture]
    public class CreateNewBusinessQuoteAPITests
    {
        QuotingAPIClient quotingAPIClient = null;
        UtilitiesHelper utilitiesHelper = new();

        [SetUp]
        public void SetUp()
        {
            quotingAPIClient = new QuotingAPIClient(GlobalTestInfrastructureSetup.SharedRestLibrary);
        }

        [Test, Category("Positive")]
        public async Task Given_When_Then()
        {
            //Arrange
            CreateNewBusinessQuoteRequest? createNewBusinessQuoteRequest = JsonSerializer.Deserialize<CreateNewBusinessQuoteRequest>
                (utilitiesHelper.ReadTestDataJson("Quoting/Data", "CreateNewBusinessQuoteRequest.json"));

            //Act
            var response = await quotingAPIClient.CreateNewBusinessQuoteAsync(createNewBusinessQuoteRequest!);

            //Assert
        }
    }
}
