using System.Collections.Generic;
using System.Text;
using System.Xml;
using Certitude.Models;
using Certitude.Rules;

namespace Certitude.Views
{
    public class EvaluationView : IView
    {
        private readonly IEnumerable<RuleResult> _results;

        public EvaluationView(IEnumerable<RuleResult> results)
        {
            _results = results;
        }

        public string Serialize(IModel model)
        {
            EvaluationModel localModel = (EvaluationModel) model;

            // set up the writer
            StringBuilder stringBuilder = new StringBuilder();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSettings);

            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("evaluation");
            xmlWriter.WriteAttributeString("notification", localModel.NotificationID);

            xmlWriter.WriteStartElement("results");
            foreach (RuleResult result in _results)
            {
                if (result != null)
                {
                    xmlWriter.WriteRaw(result.ToXml());
                }
            }
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();

            return stringBuilder.ToString();
        }
    }
}