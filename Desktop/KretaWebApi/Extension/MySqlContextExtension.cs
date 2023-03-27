using KretaWebApi.Context;
using Microsoft.EntityFrameworkCore;

namespace KretaWebApi.Extension
{
    public static class MySqlContextExtension
    {
        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            string connectionStringSection = "MySqlConnection:ConnectionString";

            if (IsSectionExsist(config, connectionStringSection))
            {
                var connectionString = config[connectionStringSection];
                services.AddDbContextFactory<MySqlContext>(
                    o => o.UseMySql(connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    options => options.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: System.TimeSpan.FromSeconds(3000),
                        errorNumbersToAdd: null)
                    ));
            }
        }

        private static bool IsSectionExsist(this IConfiguration config, string sectionName)
        {
            var section = config.GetSection(sectionName);
            if (section.Exists())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
