using System.Collections.Generic;

namespace Certitude.Models
{
    public interface IResponse
    {
        ServiceOutcomes Outcome { get; set; }
        IEnumerable<string> Flags { get; }
        IEnumerable<string> Errors { get; }

        void AddError(string error);
        void AddFlag(string flag);

        void AddErrors(IEnumerable<string> errors);
        void AddFlags(IEnumerable<string> flags);
    }
}