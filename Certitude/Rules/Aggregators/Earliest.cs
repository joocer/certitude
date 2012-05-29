using System;

namespace Certitude.Rules.Aggregators
{
    public class Earliest : EventAggregator<DateTime>
    {
        public override DateTime Calculate()
        {
            throw new NotImplementedException();
        }
    }
}