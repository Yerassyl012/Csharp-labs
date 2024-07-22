using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using func_lab11.ReaderEx;
using func_lab11.Chapter11;
using func_lab11.Middleware.DbLogger;

namespace func_lab11
{
    class Program
    {
        static void Main(string[] args)
        {

            int userChoice = Convert.ToInt32(Console.ReadLine());
            if (userChoice == 1)
            {
                Identity_Example._main();
            }
            if (userChoice == 2)
            {
                Reader_Example._main();
                Func_As_Reader_Example._main();
            }
            if (userChoice == 3)
            {
                TryTests.ru();
            }
        }
    }
}
