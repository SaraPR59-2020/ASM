using LocalDeviceProject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace LocalControllerProject
{
    public class LocalController : ILocalController
    {
        public MyTcpListener MyServer { get; set; }
        public MyNetworkStream MyStream { get; set; }

        public MyNetworkStream MyAMSStream { get; set; }

        public XmlWritter XmlWriter { get; set; }

        public XmlReader XmlReader { get; set; }

        public MyTcpClient MyClient { get; set; }

        private string absolutePath;

        public LocalController() 
        {
            MyClient = new MyTcpClient();
            XmlWriter = new XmlWritter();
            XmlReader = new XmlReader();
            MyAMSStream = new MyNetworkStream();
            string path = "ControllerData.xml";
            string dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            absolutePath = Path.Combine(dir, path);
        }



        public void Startup()
        {
            MyClient.TcpClient = new TcpClient("127.0.0.1", 4160);
            MyAMSStream.Stream = MyClient.TcpClient.GetStream();
        }

        public bool SendToAMS() 
        {
            List<LocalDevice> devices = XmlReader.ReadData(absolutePath);
            BinaryFormatter bf = new BinaryFormatter();
            byte[] objectBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, devices);
                objectBytes = ms.ToArray();
            }
            try
            {
                MyAMSStream.Write(objectBytes, 0, objectBytes.Length);
                MyAMSStream.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public void StartServer() 
        {
            MyStream = new MyNetworkStream();
            MyAMSStream = new MyNetworkStream();
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            MyServer = new MyTcpListener(localAddr, 3560);
            MyServer.Start();

        }

        public void KomunicirajSaUredjajima() 
        {
            StartServer();
            while (true)
            {
                ReceiveData();
                Console.WriteLine("Data received");
            }
        }

        public void KomunicirajSaAMS() 
        {
            while (true) 
            {
                Startup();
                SendToAMS();
                Thread.Sleep(3000);
            
            }
        }
      
          
        public bool ReceiveData() 
        {
            try 
            {
                MyClient.TcpClient = MyServer.AcceptTcpClient();
                MyStream.Stream = MyClient.GetStream();
                Console.WriteLine("Konekcija uspensa");

                // citanje upita od klijenta
                byte[] data = new byte[8192];
                int bytes = MyStream.Read(data, 0, data.Length);

                LocalDevice local;

                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(data, 0, data.Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    local = (LocalDevice)bf.Deserialize(ms);
                    XmlWriter.WriteData(local, absolutePath);
                }

                return true;
            }
            catch(Exception e) 
            {

                return false;
            }
           
        }
    }
}
