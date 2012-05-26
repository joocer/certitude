using System;
using System.Collections.Generic;
using System.Linq;
using Certitude.API;
using Certitude.Models;

namespace Certitude.Views
{
    public class FatalErrorView : IView
    {
        private readonly Exception _exception;
        private readonly IEnumerable<string> _errors; 

        public FatalErrorView(Exception exception)
        {
            _exception = exception;
        }

        public FatalErrorView(IEnumerable<string> errors)
        {
            _errors = errors;
        }

        public string Serialize(IModel model)
        {
            Dictionary<string, ErrorCodes> errors = new Dictionary<string, ErrorCodes>();
            if (_exception != null)
            {
                errors.Add(String.Format(
                    "Unhandled fatal error of type '{0}' in '{1}'. Error Message - {2}",
                    _exception.GetType().Name,
                    _exception.Source,
                    _exception.Message), ErrorCodes.UNKNOWN
                    );
            }
            if (_errors != null && _errors.Any())
            {
                foreach (string error in _errors)
                {
                    errors.Add(error, ErrorCodes.UNKNOWN);
                }
            }
            return ViewHelpers.ErrorWriter(errors);
        }
    }
}