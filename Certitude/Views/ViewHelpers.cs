using System.Collections.Generic;
using System.Text;
using System.Xml;
using Certitude.API;

namespace Certitude.Views
{
    public static class ViewHelpers
    {
        public static string ErrorWriter(IDictionary<string, ErrorCodes> errors)
        {
            // set up the writer
            StringBuilder stringBuilder = new StringBuilder();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSettings);

            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("errors");

            foreach (string key in errors.Keys)
            {
                xmlWriter.WriteStartElement("error");
                xmlWriter.WriteAttributeString("code", errors[key].ToString());
                xmlWriter.WriteAttributeString("description", key);
                xmlWriter.WriteEndElement();                
            }

            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();

            return stringBuilder.ToString();
        }

        public static string FlagWriter(IEnumerable<string> flags)
        {
            // set up the writer
            StringBuilder stringBuilder = new StringBuilder();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSettings);

            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("flags");

            foreach (string flag in flags)
            {
                xmlWriter.WriteStartElement("flag");
                xmlWriter.WriteAttributeString("information", flag);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();

            return stringBuilder.ToString();
        }
    }
}