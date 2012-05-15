using System.Collections.Generic;

namespace Infrastructure.Resources.Validation
{
    public interface IValidationService
    {
        bool Validate(object subject, out IEnumerable<string> validationResults);
    }
}
