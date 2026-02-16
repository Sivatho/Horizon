using ClientServicing.Main.DataAccess.Interface;
using Microsoft.Data.SqlClient;

namespace ClientServicing.Main.DataAccess.SQL
{
    public class MsSqlDataAccess : IDataAccess
    {
        private readonly string _connectionString;

        public MsSqlDataAccess(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<bool> ExecuteAsync(string query, SqlParameter[]? parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = 30;
                    if (parameters?.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query, SqlParameter[]? parameters = null) where T : class, new()
        {
            var results = new List<T>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = 30;

                    if (parameters?.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var obj = new T();
                            SqlDataMapper.MapReaderToObject(reader, obj);
                            results.Add(obj);
                        }
                    }
                }
            }
            return results;
        }
    }
}
