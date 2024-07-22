using System;
using System.Linq;
using System.Threading.Tasks;
namespace lab14
{
    class Zhugirushiler
    {
        public static Task tsk;
        public int Massiv(int b, int[] n)
        {
            int mare = 0;
            tsk = Task.Run(() =>
            {
                Random rand = new Random((int)DateTime.Now.Ticks);
                for (int i = 0; i < b; i++)
                {
                    Func<int, int> massiv = j => n[j] = rand.Next(1, 4);
                    var range = Enumerable.Range(1, 1);
                    var massive = range.Select(massiv);
                    foreach (var x in massive)
                    {
                        Console.Write(x + "\t");
                        mare += x;
                    }
                }
                Console.WriteLine("\n");
                Console.Write(mare);
            });
            tsk.Wait();
            return mare;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int[] n = new int[10];
            Zhugirushiler zh = new Zhugirushiler();
            for (int i = 0; i < n.Length; i++)
            {
                zh.Massiv(10, n);
                Console.WriteLine("- мареге жеттим, реттик номерым: {0} ", i);
            }
            Console.WriteLine("tsk тапсырмасының идентификаторы: " + Zhugirushiler.tsk.Id);
            Console.WriteLine();
        }
    }
}


//    using System;
//using System.Linq;
//using System.Threading.Tasks;
//namespace lab14
//{
//    class Zhugirushiler
//    {

//        public int[] n;
//        public int nn;
//        public Task tsk;
//        public Zhugirushiler(int nn, int a)
//        {
//            this.nn = nn;
//            n = new int[a];
//        }
//        public void Massiv()
//        {
//            int mare = 0;

//                Random rand = new Random((int)DateTime.Now.Ticks);
//            for (int i = 0; i < 10; i++)
//            {
//                tsk = Task.Run(() =>
//           {
//               Func<int, int> massiv = j => n[j] = rand.Next(1, 4);
//               var range = Enumerable.Range(1, 1);
//               var massive = range.Select(massiv);
//               foreach (var x in massive)
//               {
//                   Console.Write(x + "\t");
//                   mare += x;
//               }
//           });
//                Console.Write("\n");
//                Console.WriteLine(mare);
//                Console.WriteLine();
//            }
//        }
//    }
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            int[] n = new int[10];
//            Zhugirushiler[] tr =
//                {
//                new Zhugirushiler(1, 10),
//                new Zhugirushiler(2, 10),
//                new Zhugirushiler(3, 10),
//                new Zhugirushiler(4, 10),
//                new Zhugirushiler(5, 10),
//                new Zhugirushiler(6, 10),
//                new Zhugirushiler(7, 10),
//                new Zhugirushiler(8, 10),
//                new Zhugirushiler(9, 10),
//                new Zhugirushiler(10, 10)
//            };
//            //    Task task1 = new Task(tr[0].Massiv);
//            //Task task2 = new Task(tr[1].Massiv);
//            //Task task3 = new Task(tr[2].Massiv);
//            //Task task4 = new Task(tr[3].Massiv);

//            //task1.Start();
//            //task2.Start();
//            //task3.Start();
//            //task4.Start();

//            //Task.WaitAll(task1, task2, task3,task4);
//            Console.ReadKey();
//            Console.WriteLine();
//        }
//    }
//}
