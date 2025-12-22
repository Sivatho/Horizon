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
    public class AffordabilityEnquiryAPITests
    {
        GSDAPIClient gsdAPIClient = new GSDAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        public async Task Given_AffordabilityEnquiryRequestPayloadIsValid_When_AffordabilityEnquiryAsync_Then() {
            //Arrange
            Guid newGuid = Guid.NewGuid();
            AffordabilityEnquiryRequest affordabilityEnquiryRequest = JsonSerializer.Deserialize<AffordabilityEnquiryRequest>(utilitiesHelper.ReadTestDataJson("GSD/Data", "AffordabilityEnquiryRequestPayloadIsValid.json"));
            affordabilityEnquiryRequest.requestId = newGuid.ToString();

            //Act
            var response = await gsdAPIClient.AffordabilityEnquiryAsync(affordabilityEnquiryRequest);

            //Assert
        }
    }
}
