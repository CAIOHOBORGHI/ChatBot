using Microsoft.Extensions.Configuration;

namespace Chat.Core.Utils
{
    public static class Helper
    {
        public static string GetConnection(IConfiguration configuration, string connectionString)
        {
            return configuration[connectionString] ?? configuration.GetConnectionString(connectionString);
        }
    }
}