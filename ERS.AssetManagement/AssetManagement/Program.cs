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
            int x;
            do
            {
                Console.WriteLine("1. Ispis svih uredjaj");
                Console.WriteLine("2. Ispis uredjaja koji su prekoracili dozvoljeno vreme");
                Console.WriteLine("3. Broj radnih sati za urejdaj: ");
                Console.WriteLine("0. Kraj");

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
                    Console.WriteLine("Unesite id: ");
                    string id = Console.ReadLine();
                    Console.WriteLine("Unesite datum od");
                    string datumOd = Console.ReadLine();

                    Console.WriteLine("Unesite datum do");
                    string datumDo = Console.ReadLine();

                    DateTime dOd = DateTime.ParseExact(datumOd, "dd.MM.yyyy.", null);
                    DateTime dDo = DateTime.ParseExact(datumDo, "dd.MM.yyyy.", null);

                    double brSati = ams.IzracunajBrojRadnihSati(id, dOd, dDo);
                    Console.WriteLine("Broj radnih sati je "+brSati);

                }
                else if (x == 0) 
                {
                    break;
                }
            } while (x != 0);
        }
    }
}
