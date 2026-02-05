using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientServicing.Main.Controller;
using ClientServicing.Main.Models.General;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Test.Tests.API.TDD.Payer
{
    public class GetPayerDetailsByPolicyNumberAPITests
    {
        PayerAPIClient payerAPIClient = new();
        UtilitiesHelper utilitiesHelper = new();

        [Test]
        public async Task Given_GetPayerDetailsByPolicyNumberRequest_When_GetPayerDetailsByPolicyNumberAsync_Then() {
            var getPayerDetailsByPolicyNumberRequest = JsonSerializer.Deserialize<PolicyNoAndEffectiveDate>(
                utilitiesHelper.ReadTestDataJson("Payer/Data", "GetPayerDetailsByPolicyNumberPayloadRequestIsValid.json"));
            getPayerDetailsByPolicyNumberRequest.auditToken = Guid.NewGuid().ToString();

            var response = await payerAPIClient.GetPayerDetailsByPolicyNumberAsync(getPayerDetailsByPolicyNumberRequest);

        }
    }
}
