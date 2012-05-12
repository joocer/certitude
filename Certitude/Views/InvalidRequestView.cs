using System.Collections.Generic;
using Certitude.API;
using Certitude.API;
using Certitude.Models;

namespace Certitude.Views
{
    public class InvalidRequestView : IView
    {
        private readonly IDictionary<string, ErrorCodes> _errors; 

        public InvalidRequestView(IEnumerable<string> errors)
        {
            _errors = new Dictionary<string, ErrorCodes>();
            // map from a list to a dic
            foreach(string error in errors)
            {
                _errors.Add(error, ErrorCodes.FORMAT);
            }
        }

        public string Serialize(IModel model)
        {
            return ViewHelpers.ErrorWriter(_errors);
        }
    }
}