using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
namespace lab13
{
    class Zhugirushiler
    {
        public static ConcurrentQueue<int> bc = new ConcurrentQueue<int>();
        public int[] n = new int[10] { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 };
        public int num;
        public static Task tsk;
        public Zhugirushiler(int b)
        {
            num = b;
        }
        public void Massiv(ConcurrentQueue<int> t)        // конкуренттік әдіс
        {
            int r;
            for (int i = 0; i < n.Length; i++)
            {
                if (n[i] == 1)
                {
                    if (t.TryDequeue(out r))
                    {
                        n[i] = r;
                    }
                }
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
            bc.Enqueue(mare);
            Console.WriteLine(mare);
            Console.WriteLine("- мареге жеттим, реттик номерым: {0} ", num);
        }
        public void Display(ConcurrentQueue<int> t)       // конкуренттілікті шығару әдісі Task классымен
        {
            tsk = Task.Run(() =>
            {
                Console.Write(num + ":qatusushi    ");
                Massiv(t);
                Console.WriteLine("\n");
            });
            tsk.Wait();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Zhugirushiler[] tr =
                {
                new Zhugirushiler(1),
                new Zhugirushiler(2),
                new Zhugirushiler(3),
                new Zhugirushiler(4),
                new Zhugirushiler(5),
                new Zhugirushiler(6),
                new Zhugirushiler(7),
                new Zhugirushiler(8),
                new Zhugirushiler(9),
                new Zhugirushiler(10)
            };
            var qu = new ConcurrentQueue<int>();  // конкуреттілік коллекциялар
            qu.Enqueue(2);
            qu.Enqueue(3);
            qu.Enqueue(1);
            qu.Enqueue(2);
            qu.Enqueue(1);
            qu.Enqueue(4);
            qu.Enqueue(1);
            qu.Enqueue(2);
            qu.Enqueue(1);
            qu.Enqueue(4);

            for (int i = 0; i < tr.Length; i++)
            {
                tr[i].Display(qu);
            }
            Thread.Sleep(1000);
            Console.WriteLine("\nНегізгі ағын іске қосылды.");
            for (int i = 0; i < tr.Length; i++)
            {
                tr[i].Uaqyt();
            }
            Console.WriteLine("\nНегізгі ағын аяқталды.\n");

            Console.WriteLine();
        }
    }
}