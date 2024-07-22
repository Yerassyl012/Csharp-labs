using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_7
{
    class Bolsheksan
    {
        public double butinsan;
        public double bolsheksan;
        public Bolsheksan(double a, double b)
        {
            butinsan = a;
            bolsheksan = b;
        }
        public Bolsheksan kosu(int k, double i)
        {
            return new Bolsheksan(butinsan + k, bolsheksan + i);
        }
        public static Bolsheksan operator +(Bolsheksan a1, Bolsheksan a2)
        {
            return new Bolsheksan { bolsheksan = a1.bolsheksan + a2.bolsheksan };
        }
        public static bool operator <(Bolsheksan a1, Bolsheksan a2)
        {
            return a1.bolsheksan < a2.bolsheksan;
        }
        public static bool operator >(Bolsheksan a1, Bolsheksan a2)
        {
            return a1.bolsheksan > a2.bolsheksan;
        }
        public void Shugary()
        {
            Console.WriteLine(butinsan+ " , "+ bolsheksan);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Bolsheksan b = new Bolsheksan(1.3, 2.3);
            Console.WriteLine("Bastapqy:");
            b.Shugary();

            Bolsheksan b2 = new Bolsheksan(1, 3.1);
            b2.Shugary();

            Bolsheksan c1 = new Bolsheksan(1.5, 1.2);
            Bolsheksan c2 = new Bolsheksan(1.3, 1.4);
            bool result = c1 != c2;
            Console.WriteLine(result);

            Bolsheksan c3 = c1 + c2;
            Console.WriteLine(c3); 
            Console.ReadKey();
        }
    }
}
