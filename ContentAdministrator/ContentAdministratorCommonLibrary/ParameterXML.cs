using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace ContentAdministratorCommonLibrary
{
    public static class ParameterXML
    {
        public const string appParametersDirectory  = @"C:\ProgramData\ContentAdministrator\";
        public const string appParametersFilename   = "ContentAdministratorParameters.xml";
        public const string appParameterXMLFileName = appParametersDirectory + appParametersFilename; 

        static ParameterXML()
        {
            Directory.CreateDirectory(appParametersDirectory);

            if (! File.Exists(appParameterXMLFileName))
            {                
                XElement created = new XElement("Created", Guid.NewGuid().ToString());
                created.SetAttributeValue("User", Environment.UserName);
                created.SetAttributeValue("Time", DateTime.Now.ToUniversalTime());

                var xml = new XDocument(
                    new XComment("Parameters and informations for ContentAdministrator system."),
                    new XElement("ContentAdministrator",
                        new XElement("Parameters"),
                        new XElement(Database.connectionStringNodeName),
                        new XElement("Logs", created),
                        new XElement("CopyRight", "(c) eMeL [www.emel.hu]")
                    )
                );

                xml.Save(appParameterXMLFileName);
            }
        }

        public static XDocument GetXml()
        {
            return XDocument.Load(appParameterXMLFileName);
        }

        public static void SetValue(string node, string name, string value)
        {
            var xml = GetXml();

            XElement nodeElement = xml.Root.Element(node);
            Debug.Assert(nodeElement != null);
            XElement nameElement = nodeElement.Element(name);

            //foreach (var element in xml.Root.Elements())
            //{
            //    if (element.Name == node)
            //    {
            //        nodeElement = element;
            //        break;
            //    }
            //}
            //Debug.Assert(nodeElement != null);

            //foreach (var element in nodeElement.Elements())
            //{
            //    if (element.Name == name)
            //    {
            //        nameElement = element;
            //        break;
            //    }
            //}

            if (nameElement == null)
            {
                nodeElement.Add(new XElement(name, value));
            }
            else
            {
                nameElement.ReplaceAll(value);
            }

            xml.Save(appParameterXMLFileName);
        }

        public static void SetValue(string node, string name, byte[] value)
        {
            SetValue(node, name, ByteArrayToString(value));
        }

        public static string GetValue(string node, string name)
        {
            var xml = GetXml();

            XElement nodeElement = xml.Root.Element(node);
            Debug.Assert(nodeElement != null);
            XElement nameElement = nodeElement.Element(name);

            //foreach (var element in xml.Root.Elements())
            //{
            //    if (element.Name == node)
            //    {
            //        nodeElement = element;
            //        break;
            //    }
            //}
            //Debug.Assert(nodeElement != null);

            //foreach (var element in nodeElement.Elements())
            //{
            //    if (element.Name == name)
            //    {
            //        return element.Value;
            //    }
            //}

            return (nameElement == null) ? null : nameElement.Value.ToString();
        }

        public static byte[] GetValueByteArray(string node, string key)
        {
            var val = GetValue(node, key);
            return StringToByteArray(val);
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);

            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }

        public static byte[] StringToByteArray(String hex)
        {
            if (hex == null)
            {
                return null;
            }

            int     numberChars = hex.Length;
            byte[]  bytes       = new byte[numberChars / 2];

            for (int i = 0; i < numberChars; i += 2)
            {
                var byteStr  = hex.Substring(i, 2);
                var byteByte = Convert.ToByte(byteStr, 16);

                bytes[i / 2] = byteByte;
            }

            return bytes;
        }
    }

    
}
