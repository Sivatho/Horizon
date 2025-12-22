using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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
        public async Task Given_When_Then() {
            //Arrange
            Guid newGuid = Guid.NewGuid();
            EmployeeEnquiryRequest employeeEnquiryRequest = JsonSerializer.Deserialize<EmployeeEnquiryRequest>(utilitiesHelper.ReadTestDataJson("GSD/Data", "EmployeeEnquiryRequestPayloadIsValid.json"));
            employeeEnquiryRequest.requestId = newGuid.ToString();
            
            //Act
            var response = await gsdAPIClient.EmployeeEnquiryAsync(employeeEnquiryRequest);

            //Assert
        }
    }
}
