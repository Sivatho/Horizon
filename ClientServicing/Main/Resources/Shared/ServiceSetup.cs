
using System;
using ClientServicing.Main.AbstractComponents.API.Base;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.DataAccess.SQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ClientServicing.Main.Resources.Shared
{
    public static class ServiceSetup
    {


        public class DatabaseSettings
        {
            public string Horizon { get; set; }
            public string Migration { get; set; }
            public string D3 { get; set; }
        }

        public static ServiceProvider BuilderServiceProvider
        {
            get
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddUserSecrets<ServiceSetupMarker>(optional: true)
                    .AddEnvironmentVariables()
                    .Build();

                // Load all connection strings directly
                var settings = new DatabaseSettings
                {
                    Horizon = configuration.GetConnectionString("horizonDbConnectionString")
                               ?? BuildWindowsAuthConnectionString(),

                    Migration = configuration.GetConnectionString("horizonMigrationDbConnectionString")
                               ?? BuildWindowsAuthConnectionString(),

                    D3 = configuration.GetConnectionString("D3DbConnectionString")
                               ?? BuildWindowsAuthConnectionString()
                };

                // Validate all connection strings use Windows Auth
                ValidateWindowsAuth(settings.Horizon);
                ValidateWindowsAuth(settings.Migration);
                ValidateWindowsAuth(settings.D3);

                // Build service collection
                var services = new ServiceCollection();

                services.AddSingleton<IConfiguration>(configuration);

                // Register settings strongly typed
                services.AddSingleton<IOptions<DatabaseSettings>>(
                    Options.Create(settings));

                // Register primary data access
                services.AddSingleton<IDataAccess, MsSqlDataAccess>();

                services.AddSingleton<IRestLibrary, RestLibrary>();

                return services.BuildServiceProvider();
            }
        }

        private static void ValidateWindowsAuth(string connectionString)
        {
            if (!UsesWindowsAuth(connectionString))
            {
                throw new InvalidOperationException(
                    "Connection string must use Windows Authentication. " +
                    $"Invalid: {Mask(connectionString)}"
                );
            }
        }

        private static bool UsesWindowsAuth(string cs)
        {
            if (string.IsNullOrWhiteSpace(cs)) return false;

            return cs.Contains("Integrated Security=true", StringComparison.OrdinalIgnoreCase)
                || cs.Contains("Integrated Security=SSPI", StringComparison.OrdinalIgnoreCase)
                || cs.Contains("Trusted_Connection=true", StringComparison.OrdinalIgnoreCase);
        }

        private static string Mask(string cs)
        {
            return System.Text.RegularExpressions.Regex.Replace(
                cs,
                @"Password\s*=\s*[^;]*",
                "Password=***MASKED***",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase
            );
        }

        private static string BuildWindowsAuthConnectionString()
        {
            return "Server=TV4-POLSQLAG-01; Database=Polly_C; Integrated Security=true; " +
                   "MultipleActiveResultSets=True; TrustServerCertificate=True; Encrypt=false;";
        }
    }

    public class ServiceSetupMarker { }
}
