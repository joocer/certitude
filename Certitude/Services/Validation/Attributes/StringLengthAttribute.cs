using System;
using System.Collections.Generic;
using System.Linq;

namespace Certitude.Services.Validation.Attributes
{
    public class StringLengthAttribute : ValidationAttribute
    {
        private readonly int _min;
        private readonly int _max;

        public StringLengthAttribute(int fixedLength)
            : base(String.Format("Validation failed for field: {0} - field must be {1} characters", "{0}", fixedLength))
        {
            _min = fixedLength;
            _max = fixedLength;
        }

        public StringLengthAttribute(int min, int max)
            : base(String.Format("Validation failed for field: {0} - field must be between {1} and {2} characters in length", "{0}", min, max))
        {
            _min = min;
            _max = max;
        }

        public override bool Test(object subject)
        {
            // handle strings
            if (subject is String)
            {
                string testsubject = (string)subject;
                return TestValue(testsubject);
            }

            // handle collections of string
            if (subject is IEnumerable<string>)
            {
                IEnumerable<string> testsubject = (IEnumerable<string>) subject;
                return testsubject.All(TestValue);
            }

            // couldn't work out the type
            return true;
        }

        private bool TestValue(string value)
        {
            // pass if the string is empty
            if (String.IsNullOrEmpty(value))
            {
                return true;
            }

            // test if within bounds
            int len = value.Length;
            return (len <= _max && len >= _min);
        }
    }
}