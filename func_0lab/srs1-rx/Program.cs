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
                MutationShouldBeAvoided.NoInPlaceUpdates();
                Console.WriteLine("-------------");
                MutationShouldBeAvoided.WithIEnumerableItWorks();
            }
            else if (userChoice == 2)
            {
                HOFs.Run();
            }
            Console.Read();
        }

    }


}