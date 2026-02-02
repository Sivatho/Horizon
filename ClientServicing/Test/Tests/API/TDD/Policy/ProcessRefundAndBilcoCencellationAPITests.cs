using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    [TestFixture]
    public class ProcessRefundAndBilcoCencellationAPITests : ProcessRefundAndBilcoCencellationValidationMethods
    {
        PolicyAPIClient policyAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();
        [Test]
        public async Task Given_When_ProcessRefundAndBilcoCencellationAsync_Then_() {
            //Arrange
            var request = JsonSerializer.Deserialize<ProcessRefundAndBilcoCancellationRequest>(
                utilitiesHelper.ReadTestDataJson("Policy/Data", "ProcessRefundAndBilcoCencellationRequestPayloadIsValid.json"));
            ValidateProcessRefundAndBilcoCencellationRequestPayload(request);
            
            //Act
            var response = await policyAPIClient.ProcessRefundAndBilcoCancellationAsync(request);

            //Arrange
            ValidationAssertionHeading();
            ValidateResponseStatusCode(response, HttpStatusCode.OK);
        }
    }
}
