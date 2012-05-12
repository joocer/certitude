using System;

namespace Certitude.Services.Time
{
    public interface ITimeService
    {
        DateTime Now();
        String TimeStamp(DateTime time);
    }
}
