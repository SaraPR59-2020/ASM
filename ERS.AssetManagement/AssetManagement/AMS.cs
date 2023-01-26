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
        public MyNetworkStream MyStream { get; set; }
        public MyTcpListener MyServer { get; set; }

        public MySqlConnection MyConnection { get; set; }

        public DataBase DataBase { get; set; }


        public AMS() 
        {
            MyConnection = new MySqlConnection();
            DataBase = new DataBase();

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
                Console.WriteLine("Data received");
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



        public bool ReceiveData() 
        {
            try
            {
                TcpClient client = MyServer.AcceptTcpClient();
                MyStream.Stream = client.GetStream();
                Console.WriteLine("Konekcija uspensa");

                byte[] data = new byte[8192];
                int bytes = MyStream.Read(data, 0, data.Length);

                List<LocalDevice> localDevices = new List<LocalDevice>();

                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(data, 0, data.Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    localDevices = (List<LocalDevice>)bf.Deserialize(ms);
                    //DataBase.SaveData(localDevices);
                    Console.WriteLine("==SVI UREDJAJI==");
                    IspisiSveUredjaje(localDevices);
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
