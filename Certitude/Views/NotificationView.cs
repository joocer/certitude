using System.Collections.Generic;
using System.Text;
using System.Xml;
using Certitude.Models;

namespace Certitude.Views
{
    public class NotificationView : IView
    {
        private readonly IEnumerable<string> _flags; 

        public NotificationView(IEnumerable<string> flags)
        {
            _flags = flags;
        }

        public string Serialize(IModel model)
        {
            NotificationModel notificationModel = (NotificationModel)model;

            // set up the writer
            StringBuilder stringBuilder = new StringBuilder();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSettings);

            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("notification");

            xmlWriter.WriteRaw(ViewHelpers.FlagWriter(_flags));

            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();

            return stringBuilder.ToString();
        }
    }
}