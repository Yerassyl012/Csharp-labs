using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab13_12
{
    class Triangle
    {
        public double KatetA;
        public double KatetB;
        public Triangle(double a, double b)
        {
            KatetA = a;
            KatetB = b;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Triangle t = new Triangle(3, 4);
            Console.WriteLine("введите c: ");
            double HypotenuseC = double.Parse(Console.ReadLine());
            
            try
            {
                if (HypotenuseC != 0)
                {
                    double a1;
                    HypotenuseC = Math.Sqrt(Math.Pow(t.KatetA, 2) + Math.Pow(t.KatetB, 2));
                    a1 = Math.Asin(t.KatetA / HypotenuseC);
                }
                else
                    throw new DivideByZeroException();
            }
            catch(DivideByZeroException)
            {
                Console.WriteLine(" c nulge ten, esep kate");
            }
            Console.ReadKey();
        }
    }
}
