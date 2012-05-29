using System;

namespace Certitude.Rules.Aggregators
{
    public class Latest : EventAggregator<DateTime>
    {
        public override DateTime Calculate()
        {
            throw new NotImplementedException();
        }
    }
}