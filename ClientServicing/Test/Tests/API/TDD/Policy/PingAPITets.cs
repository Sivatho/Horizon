using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.AbstractComponents.API.ValidationMethods.Policy;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;

namespace ClientServicing.Test.Tests.API.TDD.Policy
{
    [TestFixture]
    public class PingAPITets : PingValidationMethods
    {
        PolicyAPIClient policyAPIClient = new PolicyAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        public async Task Given_PolicyApi_When_PingAsync_Then_ValidateResponseStatusCodeOK_And_ResponsePropertyNameIsValid_And_DataTypesIsValid_And_PingResponseDataIsNotNullOrEmpty() {
            //Act
            var response = await policyAPIClient.PingAsync();
            var pingResponse = populatePingResponse(response);

            //Assert
            ValidationAssertionHeading();
            ValidateResponseStatusCodeOK(response);
            ValidateResponsePropertyNameIsValid_And_DataTypesIsValid(response);
            ValidatePingResponseDataIsNotNullOrEmpty(pingResponse);
        }
        private PingResponse populatePingResponse(RestResponse response) {
            var pingResponse = new PingResponse();
            using JsonDocument jsDoc = JsonDocument.Parse(response.Content);

            var value = jsDoc.RootElement.GetBoolean();
            if (!bool.FalseString.Equals(value))
            {
                pingResponse.pingflag = value;
            }
            else {
                pingResponse.pingflag = value;
            }
            return pingResponse;
        }
    }
}
