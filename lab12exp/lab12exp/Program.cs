using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab12exp
{
    class Geom
    {
        static double Prog(double a0, double a1, double a2, int n)
        {
            double sum, r = 0;
            r = a2 / a1;
            if (r <= 1)
                sum = 0;
            else
                sum  = (a0 * (a1 * (Math.Pow(r, n-1)) * r)) / (1 - r);

            return sum;
        }
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(Geom.Prog(1, 3, 5, 3));
            }
            catch(DivideByZeroException)
            {
                Console.WriteLine("нолге болинбейды");
            }
            Console.ReadKey();
        }
    }
}
