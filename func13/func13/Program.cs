using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace func13//12 labka негізі 12 тарау
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int userChoice = Convert.ToInt32(Console.ReadLine());

            if (userChoice == 1)
            {
                //CurrencyLookup_Stateless._main();
                //CurrencyLookup._main();
                //CurrencyLookup_MoreTestable._main();
                CurrencyLookup_ErrorHandling._main();

            }
            if (userChoice == 2)
            {
                State_Number_List._main();

            } 
        }
    }
}
