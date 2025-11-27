using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Controller;

namespace ClientServicing.Test.Tests.API.TDD
{
    public class GetBankingDetailHistoryTest
    {
        [Test]
        public async Task GivenPolicyNumberValid_WhenGetBankingDetailHistoryAsync_Then()
        {
            // Arrange
            int policyNumber = 164535;
            BankAPIClient bankAPIClient = new("https://horizon.clientele.co.za/horizon.clientservicing/");

            // Act
            var response = await bankAPIClient.GetBankingDetailHistoryAsync(policyNumber);

        }
        [Test]
        public async Task GivenPolicyNumberIsInvalid_WhenGetBankingDetailHistoryAsync_Then()
        {
            // Arrange
            int policyNumber = 0;
            BankAPIClient bankAPIClient = new("https://horizon.clientele.co.za/horizon.clientservicing/");

            // Act
            var response = await bankAPIClient.GetBankingDetailHistoryAsync(policyNumber);
        }
    }
}
