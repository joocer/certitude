using System;

namespace Infrastructure.Resources.Clock
{
    class ClockProvider : IClockService
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