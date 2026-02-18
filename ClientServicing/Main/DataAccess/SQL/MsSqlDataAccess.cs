using ClientServicing.Main.DataAccess.Interface;
using Microsoft.Data.SqlClient;

namespace ClientServicing.Main.DataAccess.SQL
{
    /// <summary>
    /// SQL Server data access implementation using Windows Authentication.
    /// Provides async query and execute operations for database interactions.
    /// </summary>
    public class MsSqlDataAccess : IDataAccess
    {
        private readonly string _connectionString;
        private const int CommandTimeoutSeconds = 30;

        public MsSqlDataAccess(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        /// <summary>
        /// Executes a query and maps results to objects of type T.
        /// </summary>
        public async Task<IEnumerable<T>> QueryAsync<T>(string query, SqlParameter[]? parameters = null)
            where T : class, new()
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Query cannot be null or empty", nameof(query));

            var results = new List<T>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandTimeout = CommandTimeoutSeconds;

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
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException(
                    $"Database query failed. Ensure Windows Authentication is enabled and your user has permissions. " +
                    $"Error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Unexpected error executing query: {ex.Message}", ex);
            }

            return results;
        }

        /// <summary>
        /// Executes a non-query command (INSERT, UPDATE, DELETE).
        /// </summary>
        public async Task<bool> ExecuteAsync(string query, SqlParameter[]? parameters = null)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Query cannot be null or empty", nameof(query));

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandTimeout = CommandTimeoutSeconds;

                        if (parameters?.Length > 0)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException(
                    $"Database execute failed. Ensure Windows Authentication is enabled and your user has permissions. " +
                    $"Error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Unexpected error executing command: {ex.Message}", ex);
            }
        }
    }
}
