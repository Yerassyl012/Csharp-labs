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
        static object lockOn = new object();  // бұғаттауға қажетті жабық объект
        static object lockOn2 = new object();  // бұғаттауға қажетті жабық объект
        static object lockOn3 = new object();  // бұғаттауға қажетті жабық объект

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
            for (int i = 0; i < n.Length; i++)
            {
                Thread.Sleep(n[i]);
                mare += n[i];
            }
            Console.Write(mare);
        }
        public void Estafeta(Zhugirushiler []zh, int num1, int num2)
        {
            Thrd = new Thread(() =>
            {
                int buf = num2;
                num2 = num1;
                num1 = buf;
                lock (lockOn)
                {
                    Console.WriteLine($"{zh[num1].name} жугирип болып, эстафетаны {zh[num2].name}-ге берды");
                    Monitor.Wait(lockOn);
                }
                lock (lockOn2)
                {
                    Console.WriteLine($"{zh[num1].name} жугирип болып, эстафетаны {zh[num2].name}-ге берды");
                    Monitor.Wait(lockOn2);
                }
                lock (lockOn3)
                {
                    Console.WriteLine($"{zh[num1].name} жугирип болып, эстафетаны {zh[num2].name}-ге берды");
                    Monitor.Wait(lockOn3);
                }
            });
            Thrd.Start();
        }
        public void Display()
        {
            Console.Write(name + "    ");
            Massiv(10);
            Console.WriteLine("\n");
        }
        public void Display2(Zhugirushiler []zh)
        {
            Thread.Sleep(1000);
            Console.WriteLine();
            for (int i = 0; i < zh.Length; i++)
            {
                if (i % 2 != 0)
                {
                    zh[i].Estafeta(zh, (i - 1), i);
                }
            }
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
                new Zhugirushiler("Rasul", 6, 10)
            };
            for (int i = 0; i < tr.Length; i++)
            {
                tr[i].Display();
            }
            for (int i = 0; i < 1; i++)
            {
                tr[i].Display2(tr);
            }
            Thread.Sleep(1000);
            Console.WriteLine("Жарыс битти");

            Console.ReadKey();
        }
    }
}