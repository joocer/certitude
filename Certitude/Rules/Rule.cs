using System;
using System.Collections.Generic;

namespace Certitude.Rules
{
    public abstract class Rule : IRule
    {
        private Dictionary<string, string> _parameters; 

        public RuleResult Execute(string notification, Dictionary<string, string> parameters)
        {
            _parameters = parameters;
            return DoService(notification);
        }

        protected abstract RuleResult DoService(string notification);

        // simplifies rule code
        protected string GetParameter(string key)
        {
            string value;
            if (_parameters.TryGetValue(key, out value))
            {
                try
                {
                    return value;
                }
                catch { }
            }
            return null;
        }

    }
}