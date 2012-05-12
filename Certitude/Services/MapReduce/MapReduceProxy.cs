using System;
using System.Linq;
//using Certitude.MapReduceServices;
using Certitude.Models;
//using RemoteNotificationModel = Certitude.MapReduceServices.NotificationModel;
using LocalNotificationModel = Certitude.Models.NotificationModel;
//using RemoteServiceResponse = Certitude.MapReduceServices.ServiceResponse;
using LocalServiceResponse = Certitude.Models.ServiceResponse;
using ServiceOutcomes = Certitude.Models.ServiceOutcomes;

namespace Certitude.Services.MapReduce
{
    public class MapReduceProxy : IMapReduceService
    {
        public LocalServiceResponse EvaluationService(IModel model, string traceID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calls the remote notification service and marshalls the request and response types 
        /// </summary>
        /// <param name="model">Notification model</param>
        /// <param name="traceID"></param>
        /// <returns></returns>
        public LocalServiceResponse NotificationService(IModel model, string traceID)
        {
            //NotificationService service = new NotificationServiceClient();
            //RemoteServiceResponse remoteResponse = new RemoteServiceResponse();
            LocalServiceResponse localResponse = new LocalServiceResponse();
            //try
            //{
            //    LocalNotificationModel notification = (LocalNotificationModel)model;
            //    RemoteNotificationModel serviceModel = new RemoteNotificationModel
            //                                               {
            //                                                   ClientID = notification.ClientID,
            //                                                   DetectedBy = notification.DetectedBy,
            //                                                   SubjectIdentifiers = notification.SubjectIdentifiers.ToArray(),
            //                                                   EventType = notification.EventType,
            //                                                   DataValue = notification.DataValue,
            //                                                   DataType = notification.DataType
            //                                               };

            //    remoteResponse = (RemoteServiceResponse)service.DoService(serviceModel, traceID);
            //    localResponse.Outcome = ServiceOutcomes.Success;
            //}
            //catch(Exception exception)
            //{
            //    localResponse.AddError(exception.Message);
            //    localResponse.Outcome = ServiceOutcomes.Failure;

            //    // report the issue
            //    ServiceFactory.LoggingService.WriteException(exception, null, traceID);
            //}

            //localResponse.AddErrors(remoteResponse._errors);
            //localResponse.AddFlags(remoteResponse._flags);

            return localResponse;
        }
    }
}
