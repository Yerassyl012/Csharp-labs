using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace labka8
{
    class Zhugirushiler
    {
        public int[] n;
        public string name;
        public int num;
        public Task<int> tsk;
        public Zhugirushiler(string c, int b, int a) 
        {
            name = c;
            num = b;
            n = new int[a];
            tsk = new Task<int>(Uaqyt);  // параметрге task беру
            tsk.Start();
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
            Console.Write(mare);
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
            Uaqyt();
            Console.WriteLine("- мареге жеттим, реттик номерым: {0} ", num);
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Task<int>> tasks = new List<Task<int>>();
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
            Console.WriteLine("\nНегізгі ағын іске қосылды.");
            for(int i = 0; i < tr.Length; i++)
            { 
                tasks.Add(tr[i].tsk);
                
            }
            for(int i = 0; i < tr.Length; i++)
            {
                tr[i].Display();
            }
            Task.Factory.ContinueWhenAll<int>(new Task<int>[] { tr[0].tsk, tr[1].tsk }, m => Console.WriteLine(m[1].Result));
            
                int min = int.MaxValue;
                //for (int j = 0; j < m.Length; j++)
                //{
                //    Console.WriteLine("---" + m[j].Result);
                //}
                for (int i = 0; i < m.Length; i++)
                    if (min > m[i].Result)
                    {
                        min = m[i].Result;

                    }
                Console.WriteLine("Minimaldy man: {0}", min);
            
            for (int i = 0; i < tr.Length; i++)
            {
                tr[i].Display2();
            }



            Console.WriteLine("\nНегізгі ағын аяқталды.\n");
            Console.ReadKey();
            Console.WriteLine();
        }
    }
}
