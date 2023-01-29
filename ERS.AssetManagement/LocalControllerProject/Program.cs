using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocalControllerProject
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n===================== LOCAL DEVICE ====================");
            LocalController l = new LocalController();

            Thread thread = new Thread(new ThreadStart(l.KomunicirajSaUredjajima));
            thread.Start();


            Thread thread2 = new Thread(new ThreadStart(l.KomunicirajSaAMS));
            thread2.Start();
        }
    }
}
