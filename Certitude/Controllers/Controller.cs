using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Certitude.Results;
using Certitude.Views;
using Certitude.Models;
using Certitude.Services;
using Certitude.Services.Identity;

namespace Certitude.Controllers
{
    public abstract class Controller
    {
        public ActionResult Execute(IModel model, string traceID, IdentityService identity)
        {
            ActionResult actionResult = null;

            #region killer questions
            // validate
            IEnumerable<string> validationResults;
// ReSharper disable ConditionIsAlwaysTrueOrFalse
            // TODO: this is IoC, not DI
            if (actionResult == null && !ServiceFactory.ValidationService.Validate(model, out validationResults))
// ReSharper restore ConditionIsAlwaysTrueOrFalse
            {
                actionResult = new ActionResultInvalidRequest(null, new InvalidRequestView(validationResults));
            }

            // authenticate
            if (actionResult == null && !identity.Authenticate(model.AuthenticationToken))
            {
                actionResult = new ActionResultNotAuthorized(null, new NotAuthorizedView());
            }
            #endregion

            #region do the work
            try
            {
                if (actionResult == null)
                {
                    actionResult = DoService(model, traceID);
                }
            }
            catch (Exception exception)
            {
                // unhandled exception
                // TODO: this is IoC, not DI
                ServiceFactory.LoggingService.WriteException(exception, identity, traceID);
                actionResult = new ActionResultFatalError(model, new FatalErrorView(exception));
            }
            #endregion

            #region logging - for reporting and billing
            // TODO: this is IoC, not DI
            ServiceFactory.LoggingService.WriteAudit(CreateAudit(model, this, actionResult), identity, traceID);
            #endregion

            return actionResult;
        }

        private static string CreateAudit(IModel model, Controller controller, ActionResult result)
        {
            // set up the writer
            StringBuilder stringBuilder = new StringBuilder();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false };
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSettings);

            // start the document
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("audit");

            // model information
            xmlWriter.WriteAttributeString("model", model.GetType().Name);

            // controller information
            xmlWriter.WriteAttributeString("controller", controller.GetType().Name);

            // result information
            xmlWriter.WriteAttributeString("result", result.GetType().Name);

            // end the document
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();

            return stringBuilder.ToString();
        }

        protected abstract ActionResult DoService(IModel model, string traceID);
    }
}