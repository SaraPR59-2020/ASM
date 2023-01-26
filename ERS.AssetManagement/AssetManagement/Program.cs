using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AssetManagement
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ASM");
            AMS ams = new AMS();

            Thread thread = new Thread(new ThreadStart(ams.KomunicirajSaUredjajima));
            thread.Start();
        }
    }
}
