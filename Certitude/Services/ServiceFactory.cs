using Certitude.Services.Configuration;
using Certitude.Services.Database;
using Certitude.Services.Logging;
using Certitude.Services.MapReduce;
using Certitude.Services.Monitoring;
using Certitude.Services.Time;
using Certitude.Services.Validation;

namespace Certitude.Services
{
    /// <summary>
    /// Provides default implementations but these can be changed to allow testing
    /// </summary>
    public static class ServiceFactory
    {
        static ServiceFactory()
        {
            DatabaseResource = new MySqlDatabaseResourceAgent();
            ConfigurationService = new RegistryConfigurationProvider();
            MapReduceService = new MapReduceProxy();
            TimeService = new TimeAgent();
            ValidationService = new ValidationAgent();
            LoggingService = new DatabaseLoggingAgent();
            MonitorService = new MonitorAgent();
        }

        public static ILoggingService LoggingService { get; set; }

        public static IValidationService ValidationService { get; set; }

        public static ITimeService TimeService { get; set; }

        public static IMapReduceService MapReduceService { get; set; }

        public static IMonitorService MonitorService { get; set; }

        public static IConfigurationService ConfigurationService { get; set; }

        public static IDatabaseResource DatabaseResource { get; set; }
    }
}