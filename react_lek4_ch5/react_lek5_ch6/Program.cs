using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;

namespace react_lek5_ch6
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneratingSynchronously();
            Console.WriteLine("----");
            GeneratingWithObservable();
            Console.WriteLine("----");
            GeneratingAsynchronously();
            Console.WriteLine("Done");
            Console.ReadLine();
        }



        private static void GeneratingWithObservable()
        {
            Console.WriteLine();
            Demo.DisplayHeader("Generating primes into observable ");

            var generator = new MagicalPrimeGenerator();

            var subscription = generator
                .GeneratePrimes_Sync(5)
                .Timestamp()//Timestamp операторын әрбір шығарылған элемент үшін уақыт белгісін көрсету үшін пайдаланамыз
                .SubscribeConsole("game buy time");

            Console.WriteLine("Generation is done");//генерация бітпей Subscribe әдісі қайтпайды, сондықтан Timestamp арқылы уақыт белгісін қарап отырамыз
        }

        private static void GeneratingSynchronously()
        {

            Console.WriteLine();
            Demo.DisplayHeader("Синхронды санауды пайдалану");

            var generator = new MagicalPrimeGenerator();
            foreach (var prime in generator.Generate(5))//Генератордың берілген санына байланысты бұл әдісте қарапайым сандарға 2 секундтан генерацияланып,
                                                        //басты ағын 10 секундқа блокқа түсып қалады
            {
                Console.Write("game numbers {0}, ", prime);
            }
        }

        private static void GeneratingAsynchronously()
        {
            Console.WriteLine();
            Demo.DisplayHeader("бакылаушы колдану");

            var generator = new MagicalPrimeGenerator();

            var primesObservable = generator.GeneratePrimes_ManualAsync(5);
            //primesObservable = generator.GeneratePrimes_AsyncCreate(5);
            var subscription =
                primesObservable
                .SubscribeConsole("game buy time");

            Console.WriteLine("Proving we're not blocked. you can continue typing. type X to dispose and exit");
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "X")
                {
                    subscription.Dispose();
                    return;
                }

                Console.WriteLine("\t {0}", input);
            }
        }

    }

}

