using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AssetManagement
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n===================== ASM ====================");
            AMS ams = new AMS();

            Thread thread = new Thread(new ThreadStart(ams.KomunicirajSaUredjajima));
            thread.Start();
            int x;
            do
            {
                Console.WriteLine("\n\t\tIzaberite jednu od ponudjenih opcija:");
                Console.WriteLine("\t1. Ispis svih uredjaj;");
                Console.WriteLine("\t2. Ispis uredjaja koji su prekoracili dozvoljeno vreme;");
                Console.WriteLine("\t3. Prikaz radnog broja sati zadatog uredjaja;");
                Console.WriteLine("\t0. Kraj.");

                x = Convert.ToInt32(Console.ReadLine());
                if(x == 1) 
                {
                    ams.IspisiUredjaje();
                }
                else if (x == 2) 
                {
                    ams.Izlistaj();
                }
                else if (x == 3) 
                {
                    Console.WriteLine("\n\tUnesite id: ");
                    string id = Console.ReadLine();
                    Console.WriteLine("\tUnesite datum od");
                    string datumOd = Console.ReadLine();

                    Console.WriteLine("\tUnesite datum do");
                    string datumDo = Console.ReadLine();

                    DateTime dOd = DateTime.ParseExact(datumOd, "dd.MM.yyyy.", null);
                    DateTime dDo = DateTime.ParseExact(datumDo, "dd.MM.yyyy.", null);

                    double brSati = ams.IzracunajBrojRadnihSati(id, dOd, dDo);
                    Console.WriteLine("\t\tBroj radnih sati je "+brSati);

                }
                else if (x == 0) 
                {
                    break;
                }
            } while (x != 0);
        }
    }
}
