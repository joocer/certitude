using System;
using System.Text;
using System.Xml;

namespace Certitude.API
{
    public enum ErrorCodes
    {
        AUTH,       // authentication
        UNKNOWN,    // unknown/unhandled
        FORMAT      // request format
    }

    public static class Helpers
    {
        public static string TraceID()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        public static XmlDocument CreateResponse(string body, string traceId)
        {
            // set up the writer
            StringBuilder stringBuilder = new StringBuilder();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSettings);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("response");
            xmlWriter.WriteAttributeString("traceId", traceId);

            xmlWriter.WriteRaw(body);

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(stringBuilder.ToString());
            return xdoc;
        }
    }
}