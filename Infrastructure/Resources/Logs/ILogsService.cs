using System;

namespace Infrastructure.Resources.Logs
{
    public interface ILogsService
    {
        void WriteAudit(string auditData, string traceID);
        void WriteException(Exception exception, string traceID);
    }
}
