using System;
using Certitude.Services.Identity;

namespace Certitude.Services.Logging
{
    public interface ILoggingService
    {
        void WriteAudit(string auditData, IdentityService identityService, string traceID);
        void WriteException(Exception exception, IdentityService identityService, string traceID);
    }
}
