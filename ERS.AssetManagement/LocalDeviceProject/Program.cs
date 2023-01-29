using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocalDeviceProject
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("\n===================== LOCAL DEVICE ====================");
            LocalDevice localDevice = new LocalDevice();
            while (true)
            {
                Thread.Sleep(localDevice.WaitTime*1000);
                localDevice.Startup();
                localDevice.SendData();
                Console.WriteLine("\n\tDATA SENT\n");
            }

            
       
        }
    }
}
