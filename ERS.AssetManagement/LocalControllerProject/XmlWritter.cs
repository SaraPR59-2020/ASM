using LocalDeviceProject;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LocalControllerProject
{
    public class XmlWritter
    {

        public XmlWritter() 
        {
        
        }

        public virtual void WriteData(LocalDevice localDevice,string path) 
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(path);

            XmlNode item = doc.CreateElement("LocalDevice");
            XmlNode deviceID = doc.CreateElement("deviceID");
            deviceID.InnerText = localDevice.Id.ToString();

            item.AppendChild(deviceID);
            XmlNode deviceType = doc.CreateElement("deviceType");
            deviceType.InnerText = localDevice.Type;
            item.AppendChild(deviceType);


            XmlNode deviceCode = doc.CreateElement("deviceCode");
            deviceCode.InnerText = localDevice.LocalDeviceCode;
            item.AppendChild(deviceCode);

            XmlNode time = doc.CreateElement("timeStamp");
            time.InnerText = localDevice.Timestamp.ToString();
            item.AppendChild(time);

            XmlNode valueNode = doc.CreateElement("value");
            valueNode.InnerText = localDevice.Value.ToString();
            item.AppendChild(valueNode);

            XmlNode workTime = doc.CreateElement("workTime");
            workTime.InnerText = localDevice.WorkTime.ToString();
            item.AppendChild(workTime);

            XmlNode configuration = doc.CreateElement("configuration");
            configuration.InnerText = localDevice.Configuration;
            item.AppendChild(configuration);


            XmlNode ammount = doc.CreateElement("ammountOfWork");
            ammount.InnerText = localDevice.AmmountOfWork.ToString();
            item.AppendChild(ammount);


            doc.DocumentElement.AppendChild(item);

            doc.Save(path);



        }

        public virtual void SacuvajController(LocalController localController, string path)
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(path);

            XmlNode item = doc.CreateElement("LocalController");
            XmlNode Port = doc.CreateElement("Port");
            Port.InnerText = localController.Port.ToString();
            item.AppendChild(Port);
         
            doc.DocumentElement.AppendChild(item);

            doc.Save(path);



        }
    }
}
