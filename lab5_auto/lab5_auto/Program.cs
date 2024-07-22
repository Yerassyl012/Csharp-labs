using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace lab5_auto
{
    class Zhugirushiler
    {
        public int[] n;
        public string name;
        public int num;
        public Thread Thrd;  // ағындық типтегі объект
        public static AutoResetEvent are = new AutoResetEvent(true);
        public Zhugirushiler(string c, int b, int a, string d, AutoResetEvent evt)
        {
            name = c;
            num = b;
            n = new int[a];
            Thrd = new Thread(Display3); // ағынды құру
            Thrd.Name = d;
            are = evt;
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
        public void Uaqyt1()
        {
            int mare = 0;
            for (int i = 0; i < n.Length; i++)
            {
                Thread.Sleep(n[i]);
                mare += n[i];
            }
            if (mare >= 20)
            {
                are.Set();
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(1000);

                    are.WaitOne();
                    Console.WriteLine($"окига басталды, реттік номер: {num} ");
                    
                    Console.WriteLine($"окига токтатылды, реттік номер: {num} ");
                    
                }
            }
        }
        public void Display()
        {
            Console.Write(name + "    ");
            Massiv(10);
            Console.WriteLine("\n");
        }
        public void Display2()
        {
            Thread.Sleep(2000);
            Console.WriteLine(Thrd.Name + " zharys bastady.");
            Uaqyt();
            Console.WriteLine("- мареге жеттим, реттик номерым: {0} ", num);


        }
        public void Display3()
        {
            Thread.Sleep(500);
            Uaqyt1();
            
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            AutoResetEvent evtObj = new AutoResetEvent(false);
            Zhugirushiler[] tr =
                {
                new Zhugirushiler("Murat", 1, 10, "1 qatsushy", evtObj),
                new Zhugirushiler("Laura", 2, 10, "2 qatsushy", evtObj),
                new Zhugirushiler("Nazym", 3, 10, "3 qatsushy", evtObj),
                new Zhugirushiler("Aibek", 4, 10, "4 qatsushy", evtObj),
                new Zhugirushiler("Ademi", 5, 10, "5 qatsushy", evtObj),
                new Zhugirushiler("Rasul", 6, 10, "6 qatsushy", evtObj),
                new Zhugirushiler("Aknur", 7, 10, "7 qatsushy", evtObj),
                new Zhugirushiler("Islam", 8, 10, "8 qatsushy", evtObj),
                new Zhugirushiler("Aqjol", 9, 10, "9 qatsushy", evtObj),
                new Zhugirushiler("Ernar", 10, 10, "10 qatsushy", evtObj)
            };
            for (int i = 0; i < tr.Length; i++)
            {
                tr[i].Display();
            }
            Console.WriteLine("кедергиси коптер биринши жугируди бастады");
            for (int i = 0; i < tr.Length; i++)
            {
                tr[i].Display2();
            }
            evtObj.WaitOne();
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
