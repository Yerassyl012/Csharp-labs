using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace lab1_2sem
{

    class Zhugirushiler
    {
        public int[] n;
        public string name;
        public int num;
        public Thread Thrd;  // ағындық типтегі объект
        public Zhugirushiler(string c, int b, int a, string d)
        {
            name = c;
            num = b;
            n = new int[a];

            Thrd = new Thread(Display2); // ағынды құру
            Thrd.Name = d;
            Thrd.Start();

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
            for (int i = 0; i < n.Length; i++)
            {
                Thread.Sleep(n[i]);
                mare += n[i];
            }
            Console.Write(mare);
        }
        public void Display()
        {
            Console.Write(name + "    ");
            Massiv(10);
            Console.WriteLine("\n");
        }
        public void Display2()
        {
            Thread.Sleep(300);

            Console.WriteLine(Thrd.Name + " zharys bastady.");
            //Thread.Sleep(750);
            Uaqyt();
            Console.WriteLine("- мареге жеттим, реттик номерым: {0} ", num);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Zhugirushiler[] tr =
                {
                new Zhugirushiler("Murat", 1, 10, "1 qatsushy"),
                new Zhugirushiler("Laura", 2, 10, "2 qatsushy"),
                new Zhugirushiler("Nazym", 3, 10, "3 qatsushy"),
                new Zhugirushiler("Aibek", 4, 10, "4 qatsushy"),
                new Zhugirushiler("Ademi", 5, 10, "5 qatsushy"),
                new Zhugirushiler("Rasul", 6, 10, "6 qatsushy"),
                new Zhugirushiler("Aknur", 7, 10, "7 qatsushy"),
                new Zhugirushiler("Islam", 8, 10, "8 qatsushy"),
                new Zhugirushiler("Aqjol", 9, 10, "9 qatsushy"),
                new Zhugirushiler("Ernar", 10, 10, "10 qatsushy")
            };
            for (int i = 0; i < tr.Length; i++)
            {
                tr[i].Display();
            }


            Console.WriteLine();

            Console.ReadKey();
        }
    }
}