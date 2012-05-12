using Certitude.Models;

namespace Certitude.Services.MapReduce
{
    public interface IMapReduceService
    {
        ServiceResponse EvaluationService(IModel model, string traceID);
        ServiceResponse NotificationService(IModel model, string traceID);
    }
}
