using System.Collections.Generic;
using System.Text;
using System.Xml;
using Certitude.Models;

namespace Certitude.Views
{
    public class EvaluationView : IView
    {
        private readonly IEnumerable<string> _flags;

        public EvaluationView(IEnumerable<string> flags)
        {
            _flags = flags;
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

            xmlWriter.WriteStartElement("flags");
            xmlWriter.WriteRaw(ViewHelpers.FlagWriter(_flags));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();

            return stringBuilder.ToString();
        }
    }
}