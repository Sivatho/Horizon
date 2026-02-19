using ClientServicing.Main.AbstractComponents.API.Base;
using ClientServicing.Main.DataAccess.Interface;
using ClientServicing.Main.DataAccess.SQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClientServicing.Main.Resources.Shared
{
    public static class ServiceSetup
    {
        public static ServiceProvider BuilderServiceProvider()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddUserSecrets<ServiceSetupMarker>(optional: true)  // Use marker class instead
                .AddEnvironmentVariables()                           // For CI/CD and production
                .Build();

            var conString = "horizonDbConnectionString";
            var connectionString = configuration.GetConnectionString(conString);
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = BuildWindowsAuthConnectionString();
            }

            // Validate connection string has Windows Auth
            if (!connectionString.Contains("Integrated Security", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Connection string must use Windows Authentication (Integrated Security=true). " +
                    "Remove any User Id/Password from configuration. " +
                    $"Current: {MaskConnectionString(connectionString)}");
            }

            var services = new ServiceCollection();
            services.AddSingleton<IDataAccess>(sp => new MsSqlDataAccess(connectionString));
            services.AddSingleton<IRestLibrary, RestLibrary>();

            return services.BuildServiceProvider();
        }
        /// <summary>
        /// Builds a Windows Authentication connection string.
        /// </summary>
        private static string BuildWindowsAuthConnectionString()
        {
            return "Server=TV4-POLSQLAG-01; Database=Polly_C; Integrated Security=true; " +
                   "MultipleActiveResultSets=True; TrustServerCertificate=True; Encrypt=false;";
        }

        /// <summary>
        /// Masks sensitive data in connection string for logging.
        /// </summary>
        private static string MaskConnectionString(string connectionString)
        {
            return System.Text.RegularExpressions.Regex.Replace(
                connectionString,
                @"Password\s*=\s*[^;]*",
                "Password=***MASKED***",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
    }

    /// <summary>
    /// Marker class used to identify the assembly for User Secrets configuration.
    /// This allows AddUserSecrets<T>() to work correctly (requires a non-static type).
    /// </summary>
    public class ServiceSetupMarker { }
}