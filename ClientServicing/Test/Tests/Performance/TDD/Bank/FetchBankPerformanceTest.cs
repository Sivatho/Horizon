using ClientServicing.Test.Tests.API.TDD;
using ClientServicing.Test.Tests.API.TDD.Bank;
using NBomber.Contracts.Stats;
using NBomber.CSharp;

namespace ClientServicing.Test.Tests.Performance.TDD.Bank
{
    
    public class FetchBankPerformanceTest
    {
    /*
        [Test]
        public async Task GivenSingleBankRequestIsNotNull_WhenFetchBanksAsync_ThenValidateFetchBankResponseIsOk_AndIsNotNull_AndDataTypesIsValid()
        {
            // Create instance of your test class
            var fetchBankAPITests = new FetchBankAPITests();

            // Define NBomber scenario
            var scenario = Scenario.Create("Single - Fetch Bank Request Performance Test", async context =>
            {
                // Call your existing NUnit-style test method
                await fetchBankAPITests.GivenBankRequestIsNotNull_WhenFetchBanksAsync_ThenValidateFetchBankResponseIsOk_AndIsNotNull_AndDataTypesIsValid();

                // Return success for NBomber metrics
                return Response.Ok();
            })
            .WithLoadSimulations(
                 Simulation.KeepConstant(copies: 1, during: TimeSpan.FromSeconds(1)) // 1 user for 5 seconds
            );

            // Run NBomber
            NBomberRunner
                .RegisterScenarios(scenario)
                .WithReportFileName("Single_Fetch_Bank_Request_Performance_Test")
                .WithReportFolder("Reports\\Single_Fetch_Bank_Request")
                .WithReportFormats(ReportFormat.Html, ReportFormat.Txt, ReportFormat.Csv)
                .Run();
        }
        [Test]
        public async Task GivenMultipleBankRequestIsNotNull_WhenFetchBanksAsync_ThenValidateFetchBankResponseIsOk_AndIsNotNull_AndDataTypesIsValid()
        {
            // Create instance of your test class
            var fetchBankAPITests = new FetchBankAPITests();

            // Define NBomber scenario
            var scenario = Scenario.Create("Multiple - Fetch Bank Request Performance Test", async context =>
            {
                // Call your existing NUnit-style test method
                await fetchBankAPITests.GivenBankRequestIsNotNull_WhenFetchBanksAsync_ThenValidateFetchBankResponseIsOk_AndIsNotNull_AndDataTypesIsValid();

                // Return success for NBomber metrics
                return Response.Ok();
            })
            .WithWarmUpDuration(TimeSpan.FromSeconds(5))
            .WithLoadSimulations(
                Simulation.Inject(rate: 30, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(30)) // 10 requests/sec for 30s
            );

            // Run NBomber
            NBomberRunner
                .RegisterScenarios(scenario)
                .WithReportFileName("Multiple_Fetch_Bank_Request_Performance_Test")
                .WithReportFolder("Reports\\Multiple_Fetch_Bank_Request_Performance_Test")
                .WithReportFormats(ReportFormat.Html, ReportFormat.Txt, ReportFormat.Csv)
                .Run();
        }
    */
    }
}
