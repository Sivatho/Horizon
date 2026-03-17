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
                var configuration = BuildConfiguration();

                var settings = LoadDatabaseSettings(configuration);

                ValidateConnectionString(settings.Horizon, "horizonDbConnectionString");
                ValidateConnectionString(settings.Migration, "horizonMigrationDbConnectionString");
                ValidateConnectionString(settings.D3, "D3DbConnectionString");

                var services = new ServiceCollection();

                services.AddSingleton<IConfiguration>(configuration);
                services.AddSingleton<IOptions<DatabaseSettings>>(Options.Create(settings));
                services.AddSingleton<IDataAccess, MsSqlDataAccess>();
                services.AddSingleton<IRestLibrary, RestLibrary>();

                return services.BuildServiceProvider();
            }
        }

        private static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<ServiceSetupMarker>(optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        private static DatabaseSettings LoadDatabaseSettings(IConfiguration config)
        {
            return new DatabaseSettings
            {
                Horizon = ReadRequired(config, "ConnectionStrings:horizonDbConnectionString"),
                Migration = ReadRequired(config, "ConnectionStrings:horizonMigrationDbConnectionString"),
                D3 = ReadRequired(config, "ConnectionStrings:D3DbConnectionString")
            };
        }

        private static string ReadRequired(IConfiguration config, string key)
        {
            var value = config[key];

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException(
                    $"Missing required configuration key: '{key}'.\n\n" +
                    "Ensure this key exists in one of the following:\n" +
                    "- appsettings.json\n" +
                    "- user secrets\n" +
                    "- environment variables\n\n" +
                    "To set via environment variables, use:\n" +
                    $"  {key.Replace(":", "__")}=<connection string>"
                );
            }

            return value.Trim();
        }

        private static void ValidateConnectionString(string cs, string keyName)
        {
            if (!UsesWindowsAuth(cs))
            {
                throw new InvalidOperationException(
                    $"Connection string '{keyName}' MUST use Windows Authentication.\n" +
                    $"Invalid value:\n{Mask(cs)}"
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
    }

    public class ServiceSetupMarker { }
}