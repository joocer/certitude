using System;
using Certitude.Services.Identity;

namespace Certitude.Services.Logging
{
    public class DatabaseLoggingAgent : ILoggingService
    {

        public void WriteAudit(string auditData, IdentityService identityService, string traceID)
        {
            // TODO: this is IoC, not DI
            ServiceFactory.DatabaseResource
                .ExecuteNonQuery(
                    "audit",
                    "INSERT INTO t_audits (TimeStamp, SourceServer, TraceID, Identity, AuditData) VALUES('{0}', '{1}', '{2}', '{3}', '{4}');", 
                    ServiceFactory.TimeService.TimeStamp(ServiceFactory.TimeService.Now()),
                    Environment.MachineName,
                    traceID,
                    identityService.Identity,
                    auditData
                    );
        }

        public void WriteException(Exception exception, IdentityService identityService, string traceID)
        {
            // TODO: this is IoC, not DI
            ServiceFactory.DatabaseResource
                .ExecuteNonQuery(
                    "audit",
                    "INSERT INTO t_exceptions (TimeStamp, SourceServer, ExceptionType, TraceID, Identity, ExceptionData) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');",
                    ServiceFactory.TimeService.TimeStamp(ServiceFactory.TimeService.Now()),
                    Environment.MachineName,
                    exception.GetType().Name,
                    traceID,
                    identityService == null ? string.Empty : identityService.Identity,
                    exception.Message.Replace("'", "`")
                    );
        }
    }
}