using System;

namespace Infrastructure.Resources.Validation.Attributes
{
    public class RequiredAttribute : ValidationAttribute
    {
        private readonly Boolean _required;

        public RequiredAttribute(bool required)
            : base("Validation failed for field: {0} - field cannot be empty")
        {
            _required = required;
        }

        public override bool Test(object subject)
        {
            if (!_required)
            {
                return true;
            }

            try
            {
                // if can be coaleced into a string, do that... empty strings aren't null
                return !String.IsNullOrEmpty((string)subject);
            }
            catch { }

            return (subject != null);   
        }
    }
}