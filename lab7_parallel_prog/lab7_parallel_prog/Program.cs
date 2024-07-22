using System;
using System.Threading;
using System.Threading.Tasks;

namespace lab7_parallel_prog
{
    class Zhugirushiler
    {
        public int[] n;
        public string name;
        public int num;
        public static Task tsk;
        public Zhugirushiler(string c, int b, int a)
        {
            name = c;
            num = b;
            n = new int[a];
        }
        public void Massiv(int b)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < b; i++)
            {
                n[i] = rand.Next(1, 4);
                Console.Write(n[i] + "\t");
            }
        }
        public int Uaqyt()
        {
            int mare = 0;
            for (int i = 0; i < n.Length; i++)
            {
                Thread.Sleep(n[i]);
                mare += n[i];
            }
            return mare;
        }
        public void Display()
        {
            Console.Write(name + "    ");
            Massiv(10);
            Console.WriteLine("\n");
        }
        public void Display2()
        {
            Zhugirushiler.tsk = Task.Run(() =>
            {
                Console.WriteLine("Oiyn №" + Task.CurrentId + " іске қосылды");


                Console.Write(Uaqyt());
                Console.WriteLine("- мареге жеттим, реттик номерым: {0} ", num);
                Console.WriteLine("Oiyn №" + Task.CurrentId + " аяқталды");
            });
            Zhugirushiler.tsk.Wait();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Zhugirushiler[] tr =
                {
                new Zhugirushiler("Murat", 1, 10),
                new Zhugirushiler("Laura", 2, 10),
                new Zhugirushiler("Nazym", 3, 10),
                new Zhugirushiler("Aibek", 4, 10),
                new Zhugirushiler("Ademi", 5, 10),
                new Zhugirushiler("Rasul", 6, 10),
                new Zhugirushiler("Aknur", 7, 10),
                new Zhugirushiler("Islam", 8, 10),
                new Zhugirushiler("Aqjol", 9, 10),
                new Zhugirushiler("Ernar", 10, 10)
            };
            for (int i = 0; i < tr.Length; i++)
            {
                tr[i].Display();
            }
            Console.WriteLine("\nНегізгі ағын іске қосылды.");
            //Console.ResetColor();
            for (int i = 0; i < tr.Length; i++)
            {
                tr[i].Display2();
            }
            Console.WriteLine("tsk тапсырмасының идентификаторы: " + Zhugirushiler.tsk.Id);
            Console.WriteLine("\nНегізгі ағын аяқталды.\n");


            Console.WriteLine();
        }
    }

}










