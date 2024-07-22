using System;
using System.Threading;
using System.Threading.Tasks;
namespace Azamat
{
    class Program
    {
        public static void aralik_zhai_san(int a, int b)
        {
            Task.Run(() =>
            {
                Console.WriteLine("Bastaldy");
                for (int i = a; i <= b; i++)
                {
                    if (tekseru(i))
                        Console.Write(i.ToString() + ", ");
                }
                Console.WriteLine("Ayaqtaldy");
            });
        }
        private static bool tekseru(int N)
        {
            for (int i = 2; i < (int)(N / 2); i++)
            {
                if (N % i == 0)
                    return false;
            }
            return true;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Kishi san engiziniz: ");
            int a = int.Parse(Console.ReadLine());
            Console.WriteLine("Ylken sandy engiziniz: ");
            int b = int.Parse(Console.ReadLine());
            int c = (int)((b - a) / 3) + a;
            int c1 = c + (int)((b - a) / 3);
            Console.WriteLine("Birinshi aralyq:" + a + "," + c + "Ekinshi aralyq:" + c + "," + c1 + "Yshinsi aralyq:" + c1 + "," + b);  
            aralik_zhai_san(a, c);
            aralik_zhai_san(c, c1);
            aralik_zhai_san(c1, b);
            Console.ReadLine();
        }
    }
}