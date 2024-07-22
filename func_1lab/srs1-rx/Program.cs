using System.Collections.Generic;
using System.Linq;
using LaYumba.Functional;
using NUnit.Framework;
using static System.Console;

namespace srs1_rx
{
    class Program
    {
        static void Main(string[] args)
        {

            int userChoice = Convert.ToInt32(Console.ReadLine());
            if (userChoice == 1)
            {
                var size = 100000;
                var shoppingList = Enumerable.Range(1, size).Select(i => $"item{i}");

                ListFormatter1.Format(shoppingList).ForEach(WriteLine);

                ReadKey();
            }
            else if (userChoice == 2)
            {
                var shoppingList = new List<string> { "coffee beans", "BANANAS", "Dates" };

                new ListFormatter2()
                   .Format(shoppingList)
                   .ForEach(WriteLine);

                Read();
            }
            else if (userChoice == 3)
            {
                var size = 100000;
            var shoppingList = Enumerable.Range(1, size).Select(i => $"item{i}");

            new ListFormatter3()
               .Format(shoppingList)
               .ForEach(Console.WriteLine);
            }
            Console.Read();
        }

    }


}