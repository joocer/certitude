using Infrastructure.Resources.Clock;
using Infrastructure.Resources.Compression;
using Infrastructure.Resources.Configuration;
using Infrastructure.Resources.Database;
using Infrastructure.Resources.Logs;
using Infrastructure.Resources.Monitoring;

namespace Infrastructure.Resources
{
    public static class ResourceContainer
    {
        static ResourceContainer()
        {
            Clock = new ClockProvider();
            Compression = new LZ4CompressionProvider();
            Configuration = new RegistryConfigurationProvider();
            Database = new MySqlDatabaseResourceAgent();
            Logs = new DatabaseLoggingProvider();
            Monitor = new MonitorProvider();
        }

        public static IClockService Clock { get; set; }

        public static ICompressionService Compression { get; set; }

        public static IConfigurationService Configuration { get; set; }

        public static IDatabaseService Database { get; set; }

        public static ILogsService Logs { get; set; }

        public static IMonitorService Monitor { get; set; }

    }
}
