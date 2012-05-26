using System.Web.Services;
using System.Xml;
using Certitude.Controllers;
using Certitude.Models;
using Certitude.Results;
using Certitude.Services.Identity;
using Infrastructure.Resources;

namespace Certitude.API
{
    /// <summary>
    /// Summary description for NotificationServices
    /// </summary>
    [WebService(Namespace = "http://c.ertitu.de/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class NotificationServices : WebService
    {
        [WebMethod]
        public XmlDocument Notify(
            string clientID,                // M unique per client
            string authenticationKey,       // M hashed private key and token
            string eventType,               // M client defined event type
            string detectedBy,              // M device identifier
            string subjectIdentifiers,      // O customer identifier - IP address, username (CSV)
            string dataValue,               // O transaction amount etc
            string dataType                 // O type for dataValue [int,str,geo]
            )
        {
            // monitoring
            ResourceContainer.Monitor.IncrementCounter("Notification API Requests");

            // create model
            NotificationModel notificationModel = new NotificationModel
                                                      {
                                                          ClientID = clientID,
                                                          AuthenticationToken = authenticationKey,
                                                          DetectedBy = detectedBy,
                                                          EventType = eventType,
                                                          DataValue = dataValue,
                                                          DataType = dataType
                                                      };
            if (!string.IsNullOrEmpty(subjectIdentifiers))
            {
                notificationModel.SubjectIdentifiers = subjectIdentifiers.Split(',');
            }
            else
            {
                notificationModel.SubjectIdentifiers = new string[0];
            }

            // execute
            string traceID = Helpers.TraceID();
            ActionResult result = new NotificationController().Execute(notificationModel, traceID, new IdentityAgent(clientID));

            // return result
            return Helpers.CreateResponse(result.Render(), traceID);
        }

        [WebMethod]
        public XmlDocument Evaluate(
            string clientID,
            string authenticationKey,
            string notificationID
            )
        {
            // monitoring
            ResourceContainer.Monitor.IncrementCounter("Evaluation API Requests");

            // create model
            EvaluationModel evaluationModel = new EvaluationModel
                                                  {
                                                          ClientID = clientID, 
                                                          AuthenticationToken = authenticationKey,
                                                          NotificationID = notificationID
                                                      };

            // execute
            string traceID = Helpers.TraceID();
            ActionResult result = new EvaluationController().Execute(evaluationModel, traceID, new IdentityAgent(clientID));

            // return result
            return Helpers.CreateResponse(result.Render(), traceID);
        }
    }
}
