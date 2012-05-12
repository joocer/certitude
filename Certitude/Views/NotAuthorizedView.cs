using System.Collections.Generic;
using Certitude.API;
using Certitude.API;
using Certitude.Models;

namespace Certitude.Views
{
    public class NotAuthorizedView : IView
    {
        public string Serialize(IModel model)
        {
            Dictionary<string, ErrorCodes> error = new Dictionary<string, ErrorCodes>
                                                       {{"Authentication failed", ErrorCodes.AUTH}};
            return ViewHelpers.ErrorWriter(error);
        }
    }
}