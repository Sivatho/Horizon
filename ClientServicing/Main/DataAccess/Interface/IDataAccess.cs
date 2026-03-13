using ClientServicing.Main.Models.Policy.DBModels;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClientServicing.Main.DataAccess.Interface
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> QueryAsync<T>(string query, SqlParameter[]? parameters = null) where T : class, new();
        Task<bool> ExecuteAsync(string query, SqlParameter[]? parameters = null);
        
        Task<DataTable> ExecuteDataTableCompareAsync(IEnumerable<CompareHorizonMainMemberVsD3MainMember> dbResultsHorizon);
        Task<DataTable> ExecuteDataTable(IEnumerable<CompareHorizonMainMemberVsD3MainMember> dbResultsD3);
        Task<DataTable> ExecuteDataTable<T>(string query, SqlParameter[]? parameters = null);
    }
}