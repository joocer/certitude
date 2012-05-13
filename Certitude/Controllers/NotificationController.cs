using System;
using System.Linq;
using Certitude.Results;
using Certitude.Views;
using Certitude.Models;
using Certitude.Services;
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

            NotificationModel notification = (NotificationModel)model;
            int records = 0;

            if (notification.SubjectIdentifiers.Any())
            {
                foreach (string customer in notification.SubjectIdentifiers)
                {
                    records += ResourceContainer.Database
                       .ExecuteNonQuery(
                           "events",
                           "INSERT INTO t_events (TimeStamp, TraceID, ClientID, EventType, SubjectID, DataValue, DataType, DetectedBy) VALUES('{0}', UNHEX('{1}'), UNHEX('{2}'), '{3}', '{4}', '{5}', '{6}', UNHEX(CRC32('{7}')));",
                           ResourceContainer.Clock.TimeStamp(ResourceContainer.Clock.Now()),
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
                records += ResourceContainer.Database
                   .ExecuteNonQuery(
                       "events",
                       "INSERT INTO t_events (TimeStamp, TraceID, ClientID, EventType, SubjectID, DataValue, DataType, DetectedBy) VALUES('{0}', UNHEX('{1}'), UNHEX('{2}'), '{3}', '{4}', '{5}', '{6}', UNHEX(CRC32('{7}')));",
                       ResourceContainer.Clock.TimeStamp(ResourceContainer.Clock.Now()),
                       traceID,
                       notification.ClientID,
                       notification.EventType,
                       string.Empty,
                       notification.DataValue,
                       notification.DataType,
                       notification.DetectedBy
                       );
            }


            response.AddFlag(String.Format("{0} of {1} notifications created", records, notification.SubjectIdentifiers.Any() ? notification.SubjectIdentifiers.Count() : 1));

            if (response.Outcome == ServiceOutcomes.Failure)
            {
                return new ActionResultFatalError(model, new FatalErrorView(response.Errors));
            }

            // return the result
            return new ActionResultDisplayNotification(model, new NotificationView(response.Flags));

        }
    }
}