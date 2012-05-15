using System;

namespace Infrastructure.Resources.Validation.Attributes
{
    public abstract class ValidationAttribute : Attribute
    {
        private readonly string _errorTemplate;

        /// <summary>
        /// base error message, not very helpful so should be overridden
        /// </summary>
        protected ValidationAttribute()
        {
            _errorTemplate = "Validation failed for field: {0}";
        }

        protected ValidationAttribute(string errorTemplate)
        {
            _errorTemplate = errorTemplate;
        }

        public abstract bool Test(object subject);

        public string ErrorTemplate { get { return _errorTemplate; } }
    }
}
