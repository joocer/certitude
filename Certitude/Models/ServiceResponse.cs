using System;
using System.Collections.Generic;

namespace Certitude.Models
{
    [Serializable]
    public class ServiceResponse : IResponse
    {
        private readonly List<string> _flags = new List<string>();
        private readonly List<string> _errors = new List<string>();
 
        public ServiceOutcomes Outcome { get; set; }

        public IEnumerable<string> Flags { get { return _flags; } }

        public IEnumerable<string> Errors { get { return _errors; } }

        public void AddError(string error)
        {
            _errors.Add(error);
        }

        public void AddFlag(string flag)
        {
            _flags.Add(flag);
        }

        public void AddErrors(IEnumerable<string> errors)
        {
            _errors.AddRange(errors);
        }

        public void AddFlags(IEnumerable<string> flags)
        {
            _flags.AddRange(flags);
        }
    }
}
