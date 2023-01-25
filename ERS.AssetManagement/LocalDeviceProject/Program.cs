using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocalDeviceProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("local device");
            LocalDevice localDevice = new LocalDevice("11", "11", 1, "11", 1, "11");
            Console.WriteLine(localDevice.Timestamp);
            while (true)
            {
                Thread.Sleep(1000);
                localDevice.Startup();
                localDevice.SendData();
                Console.WriteLine("DATA SENT");
            }

            
           
        }
    }
}
