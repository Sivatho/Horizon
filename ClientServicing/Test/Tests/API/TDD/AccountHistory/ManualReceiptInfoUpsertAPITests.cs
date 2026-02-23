using System.Text.Json;
using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.AccountHistory;
using ClientServicing.Main.Resources.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace ClientServicing.Test.Tests.API.TDD.AccountHistory
{
    [TestFixture]
    public class ManualReceiptInfoUpsertAPITests
    {
        AccountingHistoryAPIClient? accountHistoryAPIClient = null;
        UtilitiesHelper utilitiesHelper = new();
        private IDataAccess _dataAccess = null!;

        [SetUp]
        public void SetUp()
        {
            accountHistoryAPIClient = new AccountingHistoryAPIClient(GlobalTestInfrastructureSetup.SharedRestLibrary);
            _dataAccess = GlobalTestInfrastructureSetup.ServiceProvider.GetRequiredService<IDataAccess>();
        }
        [Test]
        [Ignore("This test is missing validation Checks")]
        public async Task Given_ManualReceiptInfoUpsertRequestIsValid_When_ManualReceiptInfoUpsertAsync_Then_ValidateManualReceiptInfoUpsertResponseIsOk()
        {
            //Arrange
            var request = JsonSerializer.Deserialize<ManualReceiptInfoUpsertRequest>(
                utilitiesHelper.ReadTestDataJson
                ("AccountHistory", "ManualReceiptInfoUpsertRequestPayloadIsValid.json"));
            //Act
            var response = await accountHistoryAPIClient!.ManualReceiptInfoUpsertAsync(request!);
            //Assert
        }
    }
}
