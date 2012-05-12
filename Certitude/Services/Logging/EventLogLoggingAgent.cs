using System;
using System.Diagnostics;
using Certitude.Services.Identity;

namespace Certitude.Services.Logging
{
    /// <summary>
    /// logs errors to the EventLog
    /// </summary>
    public class EventLogLoggingAgent : ILoggingService
    {
        const string AppName = "ConfidenceEngine";

        public void WriteAudit(string auditData, IdentityService identityService, string traceID)
        {
            throw new System.NotImplementedException();
        }

        public void WriteException(Exception exception, IdentityService identityService, string traceID)
        {
            throw new System.NotImplementedException();
        }

        //public static void Record(Exception exception)
        //{
        //    StringBuilder stringBuilder = new StringBuilder();

        //    stringBuilder.AppendLine("Trace: " + request.Guid);
        //    stringBuilder.AppendLine("Source: " + exception.Source);
        //    stringBuilder.AppendLine("Message: " + exception.Message);

        //    if (request.Client != null)
        //    {
        //        stringBuilder.AppendLine("Client: " + request.Client.Guid);
        //    }
        //    if (request.Notification != null)
        //    {
        //        stringBuilder.AppendLine("Notification: " + request.Notification.Guid);
        //        stringBuilder.AppendLine("EventID: " + request.Notification.EventID);
        //    }

        //    if (exception.InnerException != null)
        //    {
        //        stringBuilder.AppendLine("Inner Exception");
        //        stringBuilder.AppendLine("Source: " + exception.InnerException.Source);
        //        stringBuilder.AppendLine("Message: " + exception.InnerException.Message);
        //    }
        //    if (exception.StackTrace != null)
        //    {
        //        stringBuilder.AppendLine();
        //        stringBuilder.AppendLine("Stack Trace");
        //        stringBuilder.AppendLine(exception.StackTrace);
        //    }

        //    // log it
        //    InnerLog(stringBuilder.ToString(),
        //        AppName,
        //        EventLogEntryType.Error);
        //}

        private static void InnerLog(string message, string appName, EventLogEntryType level)
        {
            if (!EventLog.SourceExists(appName))
            {
                EventLog.CreateEventSource(appName, "Application");
            }
            EventLog.WriteEntry(appName, message, level);
        }
    }
}
