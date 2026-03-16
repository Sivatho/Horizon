using ClientServicing.Main.Controller;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Models.Policy.DBModels;
using ClientServicing.Main.Resources.Helper;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace ClientServicing.Test.Tests.DataBaseComparTest
{

    [TestFixture]
    public class CampareHorizonDatatoD3DataforMainMemberTest
    {

        private IDataAccess _dataAccess = null!;
        PolicyAPIClient policyAPIClient = new PolicyAPIClient();
        UtilitiesHelper utilitiesHelper = new();

        [SetUp]
        public void SetUp()
        {
            _dataAccess = GlobalTestInfrastructureSetup.ServiceProvider.GetRequiredService<IDataAccess>();
        }   
        [Test]
        public async Task GivenHorizonMigratedDataVsD3MainMemberDataIsCorrect()


        {
            string MainMemberFromHorizon = utilitiesHelper.ReadTestScriptSQl("HorizonScripts", "GetMainMemberbypolicynumber.sql");
            string D3MainMemberFromD3 = utilitiesHelper.ReadTestScriptSQl( "D3Scripts", "D3MainMembercompare.sql");

          
            // Query database to validate API side effects or state
            var dbResultsD3 = await _dataAccess.ExecuteDataTableD3Async<CompareHorizonMainMemberVsD3MainMember>(D3MainMemberFromD3);
            var dbResultsHorizon = await _dataAccess.ExecuteDataTablefromHorizonCompareAsync<CompareHorizonMainMemberVsD3MainMember>(MainMemberFromHorizon);

            var modelD3 = await _dataAccess.ExecuteModelD3Async<CompareHorizonMainMemberVsD3MainMember>(D3MainMemberFromD3);
            var modelHorizon = await _dataAccess.ExecutemodelCompareAsync<CompareHorizonMainMemberVsD3MainMember>(MainMemberFromHorizon);

            DataTable MainMemberfromHorizonTable = (DataTable)dbResultsHorizon;
            DataTable MainMemberfromD3Table = (DataTable)dbResultsD3;

            //Assert.That(MainMemberfromHorizonTable.Rows.Count, Is.EqualTo(MainMemberfromD3Table.Rows.Count), "Row count mismatch between Horizon and D3 main member data.");

        }
    }
}
