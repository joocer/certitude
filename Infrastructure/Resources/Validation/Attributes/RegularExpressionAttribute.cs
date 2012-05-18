using System;
using System.Text.RegularExpressions;

namespace Infrastructure.Resources.Validation.Attributes
{
    public class RegularExpressionAttribute : ValidationAttribute
    {
        private readonly string _pattern;

        public RegularExpressionAttribute(string regularExpression)
            : base("Validation failed for field: {0} - invalid format")
        {
            _pattern = regularExpression;
        }

        public override bool Test(object subject)
        {
            string testSubject = subject as string;

            if (String.IsNullOrEmpty(testSubject))
            {
                return true;
            }

            return Regex.IsMatch(testSubject, _pattern);
        }
    }
}