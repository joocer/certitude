using System.Collections.Generic;

namespace Certitude.Rules
{
    public interface IRule
    {
        RuleResult Execute(string notification, Dictionary<string, string> parameters);
    }
}