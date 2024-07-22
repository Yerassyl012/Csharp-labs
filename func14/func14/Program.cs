using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace func14// func lab13 негізі
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice == 1)
            {
                Applicative._main();

            }
            if (choice == 2)
            {
                CurrencyLookup_Stateless._main();

            }
            if (choice == 3)
            {
                RetryHelper._main();
            }
        }
    }
}
