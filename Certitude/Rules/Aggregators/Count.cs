
namespace Certitude.Rules.Aggregators
{
    /// <summary>
    /// Counts the number of events that match a given pattern
    /// </summary>
    class Count : EventAggregator<long>
    {
        public override long Calculate()
        {
            // execute the request
            long i = long.Parse(RetrieveScalar());

            // process the result
            return i;
        }
    }
}
