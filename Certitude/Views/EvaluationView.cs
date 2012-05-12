using System.Text;
using System.Xml;
using Certitude.Models;
using Certitude.Models;

namespace Certitude.Views
{
    public class EvaluationView : IView
    {
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
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();

            return stringBuilder.ToString();
        }
    }
}