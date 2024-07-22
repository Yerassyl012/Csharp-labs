using System;
using System.Threading;


namespace react_lek_1
{
    class Program
    {
        private static StockTicker _stockTicker;

        private static void Main(string[] args)
        {
            _stockTicker = new StockTicker();// StockTicker жариялау

            var stockMonitor = new StockMonitor(_stockTicker);

            ShowMenu();

            GC.KeepAlive(stockMonitor);
            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
            Console.WriteLine("Bye Bye");
        }

        private static void ShowMenu() // меню (қай батырманы таңдау)
        {

            Console.WriteLine("Choose a simulation type (or x to exit):");
            Console.WriteLine("1) Manual     - you enter the symbol and price");
            Console.WriteLine("2) Concurrent - tests what happens when ticks are emitted concurrently");

            var selection = Console.ReadLine();
            switch (selection)
            {
                case "1":
                    ManualSimulator(_stockTicker);
                    break;
                case "2":
                    TestConcurrentTicks(_stockTicker);
                    break;
                case "x":
                    return;
                default:
                    Console.WriteLine("Unknown selection");
                    return;
            }

        }

        private static void ManualSimulator(StockTicker stockTicker)//мәнді қолмен енгізу және оның бағасын жаңарту
        {
            while (true)
            {
                Console.Write("enter symbol (or x to exit): ");
                var symbol = Console.ReadLine();
                if (symbol.ToLower() == "x")
                {
                    break;
                }
                Console.WriteLine("enter price: ");
                decimal price;
                if (decimal.TryParse(Console.ReadLine(), out price))
                {
                    stockTicker.Notify(new StockTick() { Price = price, QuoteSymbol = symbol });
                }
                else
                {
                    Console.WriteLine("price should be decimal");
                }
            }
        }

        private static void TestConcurrentTicks(StockTicker stockTicker)
        {
            ThreadPool.QueueUserWorkItem((_) => stockTicker.Notify(new StockTick() { Price = 100, QuoteSymbol = "CS GO" }));
            ThreadPool.QueueUserWorkItem((_) => stockTicker.Notify(new StockTick() { Price = 150, QuoteSymbol = "Dota 2" }));
            ThreadPool.QueueUserWorkItem((_) => stockTicker.Notify(new StockTick() { Price = 170, QuoteSymbol = "CS GO" }));
            ThreadPool.QueueUserWorkItem((_) => stockTicker.Notify(new StockTick() { Price = 195.5M, QuoteSymbol = "L4D" }));
        }
    }
}
