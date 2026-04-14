using System.Data;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.Resources.Helper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using static ClientServicing.Main.Resources.Shared.ServiceSetup;


namespace ClientServicing.Main.DataAccess.SQL
{
    /// <summary>
    /// SQL Server data access implementation using Windows Authentication.
    /// Provides async query and execute operations for database interactions.
    /// </summary>
    public class MsSqlDataAccess : IDataAccess
    {
        private readonly string _horizon;
        private readonly string _migration;
        private readonly string _d3;

        private const int CommandTimeoutSeconds = 30;
        private readonly UtilitiesHelper _utilitiesHelper = new();

        public MsSqlDataAccess(IOptions<DatabaseSettings> options)
        {
        var settings = options.Value; // resolved once
        _horizon = settings.Horizon;
        _migration = settings.Migration;
        _d3 = settings.D3;
    }

        /// <summary>
        /// Executes a query and maps results to objects of type T.
        /// </summary>
        /// pollicy model 
        public async Task<IEnumerable<T>> QueryAsync<T>(string query, SqlParameter[]? parameters = null)
            where T : class, new()
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Query cannot be null or empty", nameof(query));

            var results = new List<T>();

            try
            {
                using var connection = new SqlConnection(_horizon);
                await connection.OpenAsync();

                using var command = new SqlCommand(query, connection);
                command.CommandTimeout = CommandTimeoutSeconds;

                if (parameters?.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var obj = new T();
                    SqlDataMapper.MapReaderToObject(reader, obj);
                    results.Add(obj);
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
        /// policy model
        public async Task<bool> ExecuteAsync(string query, SqlParameter[]? parameters = null)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Query cannot be null or empty", nameof(query));

            try
            {
                using var connection = new SqlConnection(_horizon);
                await connection.OpenAsync();

                using var command = new SqlCommand(query, connection);
                command.CommandTimeout = CommandTimeoutSeconds;

                if (parameters?.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }

                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
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

        public async Task<DataTable> ExecuteDataTable<T>(string query, SqlParameter[]? parameters = null)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Query cannot be null or empty.", nameof(query));

            var dataTable = new DataTable();

            try
            {
                using var connection = new SqlConnection(_horizon);
                await connection.OpenAsync();

                using var command = new SqlCommand(query, connection);
                command.CommandTimeout = 30;

                if (parameters?.Length > 0)
                    command.Parameters.AddRange(parameters);

                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Database query failed.", ex);
            }

            return dataTable;
        }

        public async Task<DataTable> ExecuteDataTablefromHorizonCompareAsync<T>(string mainMemberFromHorizon)
        {

            
                var script = _utilitiesHelper.ReadTestScriptSQl("HorizonScripts", "GetMainMemberbypolicynumber.sql");
            var HorizondataTable = new DataTable();

            try
            {
                using (var connection = new SqlConnection(_migration))
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
        public async Task<DataTable> ExecuteDataTableD3Async<T>(string d3MainMemberFromD3)
        {
            var script = _utilitiesHelper.ReadTestScriptSQl("D3Scripts", "D3MainMembercompare.sql");
            

            var D3dataTable = new DataTable();

            try
            {
                using (var connection = new SqlConnection(_d3))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(script, connection))
                    {
                        command.CommandTimeout = CommandTimeoutSeconds;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(D3dataTable);
                        }
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
        
        public async Task<IEnumerable<T>> ExecutemodelCompareAsync<T>(string query, SqlParameter[]? parameters = null)
             where T : class, new()
        {
            {
                if (string.IsNullOrWhiteSpace(query))
                    throw new ArgumentException("Query cannot be null or empty", nameof(query));

                var results = new List<T>();

                try
                {
                    using (var connection = new SqlConnection(_horizon))
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
        }

        public async Task<IEnumerable<T>> ExecuteModelD3Async<T>(string query, SqlParameter[]? parameters = null)
             where T : class, new()
        {


            {
                if (string.IsNullOrWhiteSpace(query))
                    throw new ArgumentException("Query cannot be null or empty", nameof(query));

                var results = new List<T>();

                try
                {
                    using (var connection = new SqlConnection(_d3))
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
        }
    }
}
