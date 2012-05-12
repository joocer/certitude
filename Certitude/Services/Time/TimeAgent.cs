using System;

namespace Certitude.Services.Time
{
    class TimeAgent : ITimeService
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }

        public string TimeStamp(DateTime time)
        {
            return time.ToString("s");
        }
    }
}