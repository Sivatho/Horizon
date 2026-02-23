using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.AccountHistory;
using ClientServicing.Main.Resources.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace ClientServicing.Test.Tests.API.TDD.AccountHistory
{
    [TestFixture]
    public class CashReceiptInfoUpsertAPITests 
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
        [Ignore("Unable to get the process the request as there is missing data with StatementLineID")]
        public async Task Given_StatementLineIdRequestPayloadIsValid_When_CashReceiptInfoUpsertAsync_Then_ValidateGetStatementLineIDResponseIsOk()
        {
            //Arrange
            var request = JsonSerializer.Deserialize<StatementLineIDRequest>(
                utilitiesHelper.ReadTestDataJson
                ("AccountHistory", "StatementLineIdRequestPayloadIsValid.json"));
            //Act
            var response = await accountHistoryAPIClient!.CashReceiptInfoUpsertAsync(request!);
            //ValidationAssertionHeading();
            //ValidateResponseStatusCode(response, HttpStatusCode.OK);
            //ValidateResponseHeadersAreValid(response);
      
            //Assert
        }
    }
}
