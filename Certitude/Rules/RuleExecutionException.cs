using System;

namespace Certitude.Rules
{
    public class RuleExecutionException : Exception
    {
        private readonly string _ruleName;

        public RuleExecutionException(string ruleName, string message, Exception innerException) 
            : base(message, innerException)
        {
            _ruleName = ruleName;
        }

        public string ToXml()
        {
            return "TODO";
        }
    }
}