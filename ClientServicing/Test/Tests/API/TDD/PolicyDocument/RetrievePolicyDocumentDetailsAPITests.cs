using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.PolicyDocument
{
    [TestFixture]
    public class RetrievePolicyDocumentDetailsAPITests
    {
        PolicyDocumentAPIClient policyDocumentAPIClient = new PolicyDocumentAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        [Ignore("Test data needs to be updated output data is not as expected: Validations need to be added")]
        public async Task Given_GetPolicyDocumentRequestPayloadIsValid_When_RetrievePolicyDocumentDetailsAsync_Then_() {
            //Arrange 
            var request = JsonSerializer.Deserialize<Object>(
                utilitiesHelper.ReadTestDataJson("PolicyDocument/Data", "GetPolicyDocumentRequestPayloadIsValid.json"));
            //Act
            var response = await policyDocumentAPIClient.RetrievePolicyDocumentDetailsAsync(request);
            //Assert
        }
    }
}
