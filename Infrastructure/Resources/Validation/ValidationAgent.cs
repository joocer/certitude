using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Infrastructure.Resources.Validation.Attributes;

namespace Infrastructure.Resources.Validation
{
    /// <summary>
    /// Validation via ValidationAttributes
    /// </summary>
    public class ValidationAgent : IValidationService
    {
        public bool Validate(object subject, out IEnumerable<string> validationResults)
        {
            IEnumerable<string> results = ExecuteTests(subject);
            validationResults = results;
            return !validationResults.Any();
        }

        private IEnumerable<string> ExecuteTests(object subject)
        {
            foreach (PropertyInfo field in GetPropertiesWithAttribute(subject))
            {
                foreach (object rule in GetValidationAttributesOnProperty(field))
                {
                    ValidationAttribute validationAttribute = (ValidationAttribute) rule;
                    if (!validationAttribute.Test(field.GetValue(subject, null)))
                    {
                        yield return String.Format(validationAttribute.ErrorTemplate, field.Name);
                    }
                }
            }
        }

        /// <summary>
        /// Helper method to return the properties with validation rules
        /// </summary>
        /// <param name="subject">object under test</param>
        /// <returns></returns>
        private IEnumerable<PropertyInfo> GetPropertiesWithAttribute(object subject)
        {
            return subject.GetType().GetProperties()
                .Where(prop => prop.GetCustomAttributes(typeof(ValidationAttribute), true).Any());
        }

        /// <summary>
        /// Helper method to return the validation rules for a property
        /// </summary>
        /// <param name="property">property under test</param>
        /// <returns></returns>
        private IEnumerable<object> GetValidationAttributesOnProperty(PropertyInfo property)
        {
            return property.GetCustomAttributes(typeof(ValidationAttribute), true);
        }
    }
}