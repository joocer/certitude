namespace Infrastructure.Resources.Monitoring
{
    public interface IMonitorService
    {
        void IncrementCounter(string counter);
        void IncrementCounterBy(string counter, int amount);
    }
}
