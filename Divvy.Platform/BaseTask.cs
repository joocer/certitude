namespace Divvy.Platform
{
    public abstract class BaseTask
    {
        public string Execute(string input)
        {
            return DoService(input);
        }

        public abstract string DoService(string input);
    }
}
