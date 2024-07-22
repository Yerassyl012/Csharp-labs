using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace react_lab1
{
    internal class Program
    {
        private static StockTicker _stockTicker;
        static void Main(string[] args)
        {
            _stockTicker = new StockTicker();
            var stockMonitor = new StockMonitor(_stockTicker);

            ShowMenu();


        }

        private static void ShowMenu()
        {

            Console.WriteLine("Choose a simulation type (or x to exit):");
            Console.WriteLine("1) Manual     - you enter the symbol and price");
            Console.WriteLine("2) Automatic  - the system emits and updates a predefined collection of ticks");
            Console.WriteLine("3) Concurrent - tests what happens when ticks are emitted concurrently");

            var selection = Console.ReadLine();
            switch (selection)
            {
                case "1":
                    ManualSimulator(_stockTicker);
                    break;
                case "2":
                    AutomaticSimulator(_stockTicker);
                    break;
                case "3":
                    TestConcurrentTicks(_stockTicker);
                    break;
                case "x":
                    return;
                default:
                    Console.WriteLine("Unknow selection");
                    return;
            }

        }
    }
}
// system like a steam

