using Certitude.Results;
using Certitude.Views;
using Certitude.Models;

namespace Certitude.Controllers
{
    public class EvaluationController : Controller
    {
        /// <summary>
        /// The intention is for rules to be run against the model
        /// </summary>
        /// <param name="model">NotificationModel to be evaluated</param>
        /// <returns>ActionResult which then provides output back to caller</returns>
        protected override ActionResult DoService(IModel model, string traceID)
        {
            // TODO: logic

            // return the result
            return new ActionResultDisplayEvaluation(model, new EvaluationView());
        }
    }
}