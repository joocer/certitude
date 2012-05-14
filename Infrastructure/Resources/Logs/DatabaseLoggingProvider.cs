using System;

namespace Infrastructure.Resources.Logs
{
    public class DatabaseLoggingProvider : ILogsService
    {
        public void WriteAudit(string auditData, string traceID)
        {
            ResourceContainer.Database
                .ExecuteNonQuery(
                    "audit",
                    "INSERT INTO t_audits (TimeStamp, SourceServer, TraceID, AuditData) VALUES('{0}', '{1}', '{2}', '{3}');",
                    ResourceContainer.Clock.TimeStamp(ResourceContainer.Clock.Now()),
                    Environment.MachineName,
                    traceID,
                    auditData
                    );
        }

        public void WriteException(Exception exception, string traceID)
        {
            ResourceContainer.Database
                .ExecuteNonQuery(
                    "audit",
                    "INSERT INTO t_exceptions (TimeStamp, SourceServer, ExceptionType, TraceID, ExceptionData) VALUES('{0}', '{1}', '{2}', '{3}', '{4}');",
                    ResourceContainer.Clock.TimeStamp(ResourceContainer.Clock.Now()),
                    Environment.MachineName,
                    exception.GetType().Name,
                    traceID,
                    exception.Message.Replace("'", "`")
                    );
        }
    }
}