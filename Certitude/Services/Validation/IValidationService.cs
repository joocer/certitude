using System.Collections.Generic;

namespace Certitude.Services.Validation
{
    public interface IValidationService
    {
        bool Validate(object subject, out IEnumerable<string> validationResults);
    }
}
