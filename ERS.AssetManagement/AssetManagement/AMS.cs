using LocalControllerProject;
using LocalDeviceProject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement
{
    public class AMS
    {
        public List<LocalDevice> allDevices { get; set; }
        public MyNetworkStream MyStream { get; set; }
        public MyTcpListener MyServer { get; set; }

        public MySqlConnection MyConnection { get; set; }

        public DataBase DataBase { get; set; }

        public XmlWritter Writer { get; set; }

        public XmlReader Reader { get; set; }

        private string absolutePath;

        public AMS() 
        {
            MyConnection = new MySqlConnection();
            DataBase = new DataBase();
            string path = "data.xml";
            string dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            absolutePath = Path.Combine(dir, path);
            Writer = new XmlWritter();
            allDevices = new List<LocalDevice>();

        }
        public void StartServer()
        {
            MyStream = new MyNetworkStream();
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            MyServer = new MyTcpListener(localAddr, 4160);
            MyServer.Start();

        }



        public void KomunicirajSaUredjajima()
        {
            StartServer();
            while (true)
            {
                ReceiveData();
            }
        }


        public bool DaLiPostoji(List<LocalDevice> devices,LocalDevice device) 
        {
            foreach (LocalDevice d in devices)
            {
                    if(d.Id == device.Id) 
                    {
                     return true;
                    }

            }
            return false;
        }

  
        public void IspisiSveUredjaje(List<LocalDevice> devices) 
        {
            List<LocalDevice> lista = new List<LocalDevice>();
            foreach(LocalDevice device in devices) 
            {
                if (!DaLiPostoji(lista, device)) 
                {
                    lista.Add(device);
                }
            
            }

            foreach(LocalDevice l in lista) 
            {
                Console.WriteLine(l);
            }
        }

        public void Ispisi(List<LocalDevice> devices) 
        {
            foreach (LocalDevice d in devices)
            {
                Console.WriteLine(d);
            }
        }


        public void IspisiUredjaje() 
        {
            IspisiSveUredjaje(allDevices);

        }

        public void Izlistaj() 
        {
            List<LocalDevice> lista = new List<LocalDevice>();
            foreach(LocalDevice d in allDevices) 
            {
                if (d.AmmountOfWork > d.WorkTime) 
                {
                    lista.Add(d);
                }
            }

            Ispisi(lista);
        }


        public double IzracunajBrojRadnihSati(string id, DateTime datumOd, DateTime datumDo) 
        {
            double radniSati = 0;
            foreach(LocalDevice device in allDevices) 
            {
                if(device.Id == id) 
                {
                    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    dateTime = dateTime.AddSeconds(device.Timestamp).ToLocalTime();
                    if(dateTime > datumOd && dateTime < datumDo) 
                    {
                        radniSati += device.WorkTime;
                    }
                
                }
            }

            return radniSati;
        
        }

        public bool ReceiveData() 
        {
            try
            {
                TcpClient client = MyServer.AcceptTcpClient();
                MyStream.Stream = client.GetStream();

                byte[] data = new byte[8192];
                int bytes = MyStream.Read(data, 0, data.Length);

                List<LocalDevice> localDevices = new List<LocalDevice>();

                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(data, 0, data.Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    localDevices = (List<LocalDevice>)bf.Deserialize(ms);
                    foreach(LocalDevice l in localDevices) 
                    {
                        allDevices.Add(l);
                    }
                    //DataBase.SaveData(localDevices);
                    //Console.WriteLine("==SVI UREDJAJI==");
                }

                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
    }
}
