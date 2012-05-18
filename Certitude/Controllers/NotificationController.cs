using System.Linq;
using Certitude.Models;
using Certitude.Results;
using Certitude.Views;
using Infrastructure.Resources;

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
            ServiceResponse response = new ServiceResponse { Outcome = ServiceOutcomes.Success };
            // timestamp should be the same for each event as part of the same notification
            string timestamp = ResourceContainer.Clock.TimeStamp(ResourceContainer.Clock.Now()); 
            NotificationModel notification = (NotificationModel)model;

            if (notification.SubjectIdentifiers.Any())
            {
                foreach (string customer in notification.SubjectIdentifiers)
                {
                    ResourceContainer.Database
                       .ExecuteNonQuery(
                           "events",
                           "INSERT INTO t_events (TimeStamp, TraceID, ClientID, EventType, SubjectID, DataValue, DataType, DetectedBy) VALUES('{0}', UNHEX('{1}'), UNHEX('{2}'), '{3}', '{4}', '{5}', '{6}', UNHEX(CRC32('{7}')));",
                           timestamp,
                           traceID,
                           notification.ClientID,
                           notification.EventType,
                           customer,
                           notification.DataValue,
                           notification.DataType,
                           notification.DetectedBy
                           );
                }
            }
            else
            {
                ResourceContainer.Database
                   .ExecuteNonQuery(
                       "events",
                       "INSERT INTO t_events (TimeStamp, TraceID, ClientID, EventType, SubjectID, DataValue, DataType, DetectedBy) VALUES('{0}', UNHEX('{1}'), UNHEX('{2}'), '{3}', '{4}', '{5}', '{6}', UNHEX(CRC32('{7}')));",
                       timestamp,
                       traceID,
                       notification.ClientID,
                       notification.EventType,
                       string.Empty,
                       notification.DataValue,
                       notification.DataType,
                       notification.DetectedBy
                       );
            }

            if (response.Outcome == ServiceOutcomes.Failure)
            {
                return new ActionResultFatalError(model, new FatalErrorView(response.Errors));
            }

            // return the result
            return new ActionResultDisplayNotification(model, new NotificationView(response.Flags));

        }
    }
}