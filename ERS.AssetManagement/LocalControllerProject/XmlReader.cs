﻿using LocalDeviceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LocalControllerProject
{
    public class XmlReader
    {

        public XmlReader() 
        {
            
        }

        public virtual List<LocalDevice> ReadData(string path) 
        {

            List<LocalDevice> devices = new List<LocalDevice>();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);


            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                string Id = node.ChildNodes[0].InnerText;
                string Type = node.ChildNodes[1].InnerText;
                string LocalDeviceCode = node.ChildNodes[2].InnerText;
                long Timestamp = Convert.ToInt64(node.ChildNodes[3].InnerText);
                string Value = node.ChildNodes[4].InnerText;
                double WorkTime = Convert.ToDouble(node.ChildNodes[5].InnerText);
                string Configuration = node.ChildNodes[6].InnerText;
                LocalDevice device = new LocalDevice(Id, Type, Timestamp, Value, WorkTime, Configuration);
                devices.Add(device);
            }

            return devices;
        }
    }
}
