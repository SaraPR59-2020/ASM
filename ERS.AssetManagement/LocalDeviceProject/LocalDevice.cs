﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace LocalDeviceProject
{
    [Serializable()]
    public class LocalDevice : ILocalDevice
    {
        private readonly int  amsPort = 3560;
        private readonly int controllerPort = 3560;


        [NonSerialized()] public MyNetworkStream MyStream;
        public string Id { get; set; }
        public string Type { get; set; }
        public long Timestamp { get; set; }
        public string Value { get; set; }
        public double WorkTime { get; set; }
        public string Configuration { get; set; }
        public string LocalDeviceCode { get; set; }





        public string CreateHash()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string Data = this.Id + this.Type + this.Value + this.WorkTime.ToString() + this.Timestamp.ToString() ;

                byte[] value = sha256.ComputeHash(Encoding.UTF8.GetBytes(Data));
                return Encoding.UTF8.GetString(value);
            }
        }

            

        public void Startup()
        {
            TcpClient client;
            if (Configuration.ToUpper() == "AMS") 
            {
                client = new TcpClient("127.0.0.1", amsPort);
            }
            else 
            {
                client = new TcpClient("127.0.0.1", controllerPort);

            }

            MyStream.Stream = client.GetStream();
            Timestamp=Convert.ToInt32(ConfigurationManager.AppSettings["Timestamp"]);
        }

        public bool SendData() 
        {

            BinaryFormatter bf = new BinaryFormatter();
            byte[] objectBytes;
            using(MemoryStream ms = new MemoryStream()) 
            {
                bf.Serialize(ms, this);
                objectBytes = ms.ToArray();
            }
            try 
            {
                MyStream.Write(objectBytes, 0, objectBytes.Length);
                MyStream.Close();
                return true;
            }
            catch(Exception e) 
            {
                return false;
            }
        }

        public override string ToString()
        {
            return Id + "|" + Type + "|" + Timestamp + "|" + Value + "|" + WorkTime + "|" + Configuration;
        }

        public LocalDevice(string id,string type, long timeStamp,string value, double workingTime,string configuration) 
        {
            MyStream = new MyNetworkStream();
            Id = id;
            Type = type;
            Value = value;
            WorkTime = workingTime;
            Configuration = configuration;
            Timestamp = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
            LocalDeviceCode = CreateHash();
        }
        public LocalDevice() 
        {
            MyStream = new MyNetworkStream();
            Console.WriteLine("Dodavanje novog uredjaja: \n");

            Console.Write("Unesite ID uredjaja: ");
            Id = Console.ReadLine();
            do
            {
                Console.Write("Unesite tip uredjaja <A ili D>: ");
                Type = Console.ReadLine();
                if (Type.Equals("A") || Type.Equals("a"))
                {
                    Console.Write("Unesite inicijalnu vrednost za analogni uredjaj (broj): ");
                    Value = Console.ReadLine();

                    Console.Write("Unesite broj nominalnih radnih sati predvidjenih za uredjaj (broj) :");
                    WorkTime = Double.Parse(Console.ReadLine());
                    break;
                }
                else if (Type.Equals("D") || Type.Equals("d"))
                {
                    Console.Write("Unesite vrednost za digitalni uredjaj (1/0): ");
                    Value = Console.ReadLine();

                    Console.Write("Unesite broj nominalnih promena predvidjenih za uredjaj (broj) :");
                    WorkTime = Double.Parse(Console.ReadLine());
                    break;
                }
                else
                {
                    Console.WriteLine("Pogresno unet tip uredjaja... Unesite \'A\' ili \'D\' za tip uredjaja.");
                    throw new Exception("Pogresno unet tip uredjaja (unesite A ili D)");
                }
            } while (!Type.Equals("A") || !Type.Equals("a") || !Type.Equals("D") || !Type.Equals("d"));

         
            Console.WriteLine("Uneti AMS/LK.Configuration:");
            Configuration = ConfigurationManager.AppSettings["Configuration"];



            LocalDeviceCode = CreateHash();
            Timestamp = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds(); 
        }


    }
}
