using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LegoIV_Power_Tool
{
    class Settings
    {
        internal protected void InitSettings()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create("Settings.xml", settings);

            writer.WriteStartDocument();
            writer.WriteComment("This file is generated automatically!");
            writer.WriteStartElement("Settings");
            
            writer.WriteStartElement("Application");
            writer.WriteElementString("Name", About.AppName);
            
            writer.WriteStartElement("Verion", "");
            writer.WriteAttributeString("Release", About.Version);
            writer.WriteAttributeString("Type", "Local");
            writer.WriteAttributeString("Channel", About.Channel);
            writer.WriteEndElement();
            
            //writer.WriteElementString("Verion", About.Version);
            //writer.WriteElementString("Verion", About.Version);
            //writer.WriteElementString("Verion", About.Version);

            writer.WriteStartElement("ActionSettings");
            writer.WriteElementString("Action", "Shut down");
            writer.WriteElementString("DelayTime", "0");
            
            writer.WriteEndElement();
            
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();

        }
    }
}
