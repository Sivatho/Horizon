using ClientServicing.Main.DataAccess.Interface;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
using ClientServicing.Main.Models.Policy.DBModels;
using javax.management;
using ClientServicing.Main.Resources.Helper;
using sun.font;


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
        private UtilitiesHelper _utilitiesHelper = new UtilitiesHelper();

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
        public async Task<DataTable> ExecuteDataTableAsync(string query, SqlParameter[]? parameters = null)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Query cannot be null or empty.", nameof(query));

            var dataTable = new DataTable();

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

                    }
                }
            }
            catch (SqlException ex)
            {
                // Log or handle DB-specific errors
                throw new InvalidOperationException("Database query failed.", ex);
            }

            return dataTable;
        }
        public async Task<DataTable> ExecuteDataTableCompareAsync(string query, SqlParameter[]? parameters = null)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Query cannot be null or empty.", nameof(query));

            var dataTable = new DataTable();

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

                    }
                }
            }
            catch (SqlException ex)
            {
                // Log or handle DB-specific errors
                throw new InvalidOperationException("Database query failed.", ex);
            }

            return dataTable;
        }

        public async Task<DataTable> ExecuteDataTableCompareAsync(IEnumerable<CompareHorizonMainMemberVsD3MainMember> dbResultsHorizon)
        {
            var script = _utilitiesHelper.ReadTestScriptSQl( "HorizonScripts", "HorizonMainMember.sql");
            var HorizondataTable = new DataTable();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(script, connection))
                    {
                        command.CommandTimeout = CommandTimeoutSeconds;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(HorizondataTable);
                        }

                        var rowsAffected = await command.ExecuteNonQueryAsync();

                    }
                }
            }
            catch (SqlException ex)
            {
                // Log or handle DB-specific errors
                throw new InvalidOperationException("Database query failed.", ex);
            }

            return HorizondataTable;
        }


        public async Task<DataTable> ExecuteDataTable(IEnumerable<CompareHorizonMainMemberVsD3MainMember> dbResultsD3)
        {
        
        var script = _utilitiesHelper.ReadTestScriptSQl( "D3Scripts", "D3MainMember.sql");
            var D3dataTable = new DataTable();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(script, connection))
                    {
                        command.CommandTimeout = CommandTimeoutSeconds;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(D3dataTable);
                        }

                        var rowsAffected = await command.ExecuteNonQueryAsync();

                    }
                }
            }
            catch (SqlException ex)
            {
                // Log or handle DB-specific errors
                throw new InvalidOperationException("Database query failed.", ex);
            }

            return D3dataTable;
        }
        public Task<DataTable> ExecuteDataTable<T>(string query, SqlParameter[]? parameters = null)
        {
            throw new NotImplementedException();
        }
    }
}
