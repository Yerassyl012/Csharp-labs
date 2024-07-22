using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace labka11
{
    class Zhugirushiler
    {

        public int[] n;
        public string name;
        public int num;
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
        public void Uaqyt()
        {
            int mare = 0;
            for(int i=0; i < n.Length; i++)
                {
                    Thread.Sleep(n[i]);
                    mare += n[i];
                }
            Console.Write(mare);
            Console.WriteLine("- мареге жеттим, реттик номерым: {0} ", num);
        }
        public void Display()
        {
            Console.Write(name + "    ");
            Massiv(10);
            Console.WriteLine("\n");
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
            //Console.ResetColor()
                Parallel.For(0, tr.Length, i => tr[i].Uaqyt());


            Console.WriteLine("\nНегізгі ағын аяқталды.\n");


            Console.WriteLine();
        }
    }

}










