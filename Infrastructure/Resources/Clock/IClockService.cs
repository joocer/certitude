using System;

namespace Infrastructure.Resources.Clock
{
    public interface IClockService
    {
        DateTime Now();
        String TimeStamp(DateTime time);
    }
}
