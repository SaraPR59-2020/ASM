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
            LocalDevice localDevice = new LocalDevice("1","A",1,"1",10,"LK",12);
            while (true)
            {
                Thread.Sleep(localDevice.WaitTime*1000);
                localDevice.Startup();
                localDevice.SendData();
                Console.WriteLine("DATA SENT");
            }

            
           
        }
    }
}
