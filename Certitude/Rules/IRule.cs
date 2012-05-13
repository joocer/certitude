namespace Certitude.Rules
{
    public interface IRule
    {
        string Execute(string notification);
    }
}