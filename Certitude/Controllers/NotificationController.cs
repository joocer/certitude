using Certitude.Results;
using Certitude.Views;
using Certitude.Models;
using Certitude.Services;

namespace Certitude.Controllers
{
    public class NotificationController : Controller
    {
        /// <summary>
        /// The intention is for the notification to be saved to the datastore
        /// </summary>
        /// <param name="model">NotificationModel to be saved</param>
        /// <param name="traceID">Request ID, ties data to the request that created it</param>
        /// <returns>ActionResult which then provides output back to caller</returns>
        protected override ActionResult DoService(IModel model, string traceID)
        {
            // execute a save on map/reduce engine, one per customer identifier
            // TODO: this is IoC, not DI
            ServiceResponse response = ServiceFactory.MapReduceService.NotificationService(model, traceID);

            if (response.Outcome == ServiceOutcomes.Failure)
            {
                return new ActionResultFatalError(model, new FatalErrorView(response.Errors));
            }

            // return the result
            return new ActionResultDisplayNotification(model, new NotificationView(response.Flags));
        }
    }
}