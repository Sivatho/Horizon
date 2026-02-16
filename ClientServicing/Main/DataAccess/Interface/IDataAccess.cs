using Microsoft.Data.SqlClient;

namespace ClientServicing.Main.DataAccess.Interface
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> QueryAsync<T>(string query, SqlParameter[]? parameters = null) where T : class, new();
        Task<bool> ExecuteAsync(string query, SqlParameter[]? parameters = null);
    }
}