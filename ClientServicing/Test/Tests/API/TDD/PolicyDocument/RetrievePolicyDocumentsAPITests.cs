using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.PolicyDocument;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.PolicyDocument
{
    [TestFixture]
    public class RetrievePolicyDocumentsAPITests
    {
        PolicyDocumentAPIClient policyDocumentAPIClient = new PolicyDocumentAPIClient();
        UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        [Test]
        [Ignore("Test data needs to be updated output data is not as expected: Validations need to be added")]
        public async Task Given_GetPolicyDocumentRequestPayloadIsValid_When_RetrievePolicyDocumentsAsync_Then_()
        {
            //Arrange 
            var request = JsonSerializer.Deserialize<CheckPolicyDocumentExistRequest>(
                utilitiesHelper.ReadTestDataJson("PolicyDocument/Data", "GetPolicyDocumentRequestPayloadIsValid.json"));
            //Act
            var response = await policyDocumentAPIClient.RetrievePolicyDocumentsAsync(request);
            //Assert
        }
    }
}
