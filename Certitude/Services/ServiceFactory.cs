using Certitude.Services.MapReduce;
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
            MapReduceService = new MapReduceProxy();;
            ValidationService = new ValidationAgent();
        }

        public static IValidationService ValidationService { get; set; }

        public static IMapReduceService MapReduceService { get; set; }
    }
}