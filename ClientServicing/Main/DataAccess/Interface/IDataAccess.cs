using ClientServicing.Main.Models.Policy.DBModels;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClientServicing.Main.DataAccess.Interface
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> QueryAsync<T>(string query, SqlParameter[]? parameters = null) where T : class, new();
        Task<bool> ExecuteAsync(string query, SqlParameter[]? parameters = null);
        Task<DataTable> ExecuteDataTable<T>(string query, SqlParameter[]? parameters = null);
    
         Task<IEnumerable<T>> ExecutemodelCompareAsync<T>(string query, SqlParameter[]? parameters = null) where T : class, new();
        Task<IEnumerable<T>> ExecuteModelD3Async<T>(string query, SqlParameter[]? parameters = null) where T : class, new();
        Task<DataTable> ExecuteDataTableD3Async<T>(string d3MainMemberFromD3);
        Task<DataTable> ExecuteDataTablefromHorizonCompareAsync<T>(string mainMemberFromHorizon);
    }
}