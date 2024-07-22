using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace labka9
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
        public void Massiv(object ct)
        {
            CancellationToken cancelTok = (CancellationToken)ct;
            Random rand = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < 10; i++)
            {
                n[i] = rand.Next(1, 4);
                


                cancelTok.ThrowIfCancellationRequested();
                Console.Write(name + "    ");

                if (i == 3)
                {
                    Console.WriteLine("Тапсырманы жоюга сураныс тусти");
                    cancelTok.ThrowIfCancellationRequested();
                }
                else
                {
                    
                    Console.Write(n[i] + "\t");
                }
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
            Console.WriteLine("- мареге жеттим, реттик номерым: {0} ", num);
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
            CancellationTokenSource[] cancelTokSrc =
                {
                new CancellationTokenSource(),
                new CancellationTokenSource(),
                new CancellationTokenSource(),
                new CancellationTokenSource(),
                new CancellationTokenSource(),
                new CancellationTokenSource(),
                new CancellationTokenSource(),
                new CancellationTokenSource(),
                new CancellationTokenSource(),
                new CancellationTokenSource()
            };

            Console.WriteLine("\nНегізгі ағын іске қосылды.");

            Task[] task =
                {
                Task.Factory.StartNew(tr[0].Massiv, cancelTokSrc[0].Token, cancelTokSrc[0].Token),
                Task.Factory.StartNew(tr[1].Massiv, cancelTokSrc[1].Token, cancelTokSrc[1].Token),
                Task.Factory.StartNew(tr[2].Massiv, cancelTokSrc[2].Token, cancelTokSrc[2].Token),
                Task.Factory.StartNew(tr[3].Massiv, cancelTokSrc[3].Token, cancelTokSrc[3].Token),
                Task.Factory.StartNew(tr[4].Massiv, cancelTokSrc[4].Token, cancelTokSrc[4].Token),
                Task.Factory.StartNew(tr[5].Massiv, cancelTokSrc[5].Token, cancelTokSrc[5].Token),
                Task.Factory.StartNew(tr[6].Massiv, cancelTokSrc[6].Token, cancelTokSrc[6].Token),
                Task.Factory.StartNew(tr[7].Massiv, cancelTokSrc[7].Token, cancelTokSrc[7].Token),
                Task.Factory.StartNew(tr[8].Massiv, cancelTokSrc[8].Token, cancelTokSrc[8].Token),
                Task.Factory.StartNew(tr[9].Massiv, cancelTokSrc[9].Token, cancelTokSrc[9].Token)
        };
            for (int i = 0; i < tr.Length; i++)
            {
                try
                {
                    // Отменить задачу.
                    cancelTokSrc[i].Cancel();
                    // Приостановить выполнение метода Main() до тех пор,
                    // пока не завершится задача tsk.
                    task[i].Wait();
                }
                catch (AggregateException exc)
                {
                    if (task[i].IsCanceled)
                        Console.WriteLine("\ntsk тапсырмасынын орындалуынан бас тарту\n");
                    Console.WriteLine(exc);
                }
                finally
                {
                    task[i].Dispose();
                    cancelTokSrc[i].Dispose();
                }
            }
            Console.WriteLine("\nНегізгі ағын аяқталды.\n");


            Console.WriteLine();
        }
    }

















    //class Zhugirushiler
    //    {
    //        public int[] n;
    //        public string name;
    //        public int num;
    //        public static Task tsk;
    //        public static CancellationTokenSource cancelTokSrc = new CancellationTokenSource();
    //        public Zhugirushiler(string c, int b, int a)
    //        {
    //            name = c;
    //            num = b;
    //            n = new int[a];
    //        }
    //        public void Massiv(object ct)
    //        {
    //            Random rand = new Random((int)DateTime.Now.Ticks);
    //            CancellationToken cancelTok = (CancellationToken)ct;
    //            Zhugirushiler.tsk = Task.Factory.StartNew(() =>
    //            {
    //                cancelTok.ThrowIfCancellationRequested();
    //                for (int i = 0; i < n.Length; i++)
    //                {

    //                        n[i] = rand.Next(1, 4);
    //                        if (i == 3)
    //                        {
    //                            Console.WriteLine("Тапсырманы жоюга сураныс тусти");
    //                            cancelTok.ThrowIfCancellationRequested();

    //                        }
    //                        else
    //                        {
    //                        Console.Write(n[i] + "\t");


    //                        } 

    //                }
    //            });
    //        }
    //        public void Uaqyt()
    //        {
    //            int mare = 0;
    //            for (int i = 0; i < n.Length; i++)
    //            {
    //                Thread.Sleep(n[i]);
    //                mare += n[i];
    //            }
    //            Console.WriteLine(mare);
    //            Console.Write(mare);
    //            Console.WriteLine("- мареге жеттим, реттик номерым: {0} ", num);

    //        }
    //        public void Display(object ct)
    //        {
    //            Console.Write(name + "    ");
    //            Massiv(ct);
    //            Console.WriteLine("\n");
    //        }
    //    }
    //    class Program
    //    {
    //        static void Main(string[] args)
    //        {
    //            CancellationTokenSource cancelTokSrc = new CancellationTokenSource();
    //            Zhugirushiler[] tr =
    //                {
    //                new Zhugirushiler("Murat", 1, 10),
    //                new Zhugirushiler("Laura", 2, 10),
    //                new Zhugirushiler("Nazym", 3, 10),
    //                new Zhugirushiler("Aibek", 4, 10),
    //                new Zhugirushiler("Ademi", 5, 10),
    //                new Zhugirushiler("Rasul", 6, 10),
    //                new Zhugirushiler("Aknur", 7, 10),
    //                new Zhugirushiler("Islam", 8, 10),
    //                new Zhugirushiler("Aqjol", 9, 10),
    //                new Zhugirushiler("Ernar", 10, 10)
    //            };


    //            Console.WriteLine("\nНегізгі ағын іске қосылды.");

    //            Task task = Task.Factory.StartNew(tr[0].Massiv, cancelTokSrc.Token);
    //            Task task1 = Task.Factory.StartNew(tr[1].Massiv, cancelTokSrc.Token);
    //            Task task2 = Task.Factory.StartNew(tr[2].Massiv, cancelTokSrc.Token);
    //            //for (int i = 0; i < tr.Length; i++)
    //            //{
    //            //    tr[i].Uaqyt();
    //            //}
    //            Thread.Sleep(3000);
    //            try
    //            {
    //                // Отменить задачу.
    //                cancelTokSrc.Cancel();
    //                // Приостановить выполнение метода Main() до тех пор,
    //                // пока не завершится задача tsk.
    //                task.Wait();
    //                task1.Wait();
    //                task2.Wait();
    //            }
    //            catch (AggregateException exc)
    //            {
    //                if (task.IsCanceled && task1.IsCanceled && task2.IsCanceled)
    //                    Console.WriteLine("\ntsk тапсырмасынын орындалуынан бас тарту\n");
    //                Console.WriteLine(exc);
    //            }
    //            finally
    //            {
    //                task.Dispose();
    //                task1.Dispose();
    //                task2.Dispose();
    //                cancelTokSrc.Dispose();
    //            }
    //            Console.WriteLine("\nНегізгі ағын аяқталды.\n");


    //            Console.WriteLine();
    //        }
    //    }
}















//Кейбір жағдайларда аластамалардың нақты түрлерін ұстап қалып, кейбір түрлерін қайта генерациялау қажет болады. AggregateException класының Handle әдісі осы әрекетті орындауға мүмкіндік береді. 
//Flatten көмегімен  аластамалардың деңгейлерін  жойып,  аластама өңдеуді жеңілдете алады. 







//using System;
//using System.Threading;
//using System.Threading.Tasks;

//class DemoCancelTask
//{

//    // Тапсырма ретінде орындалатын әдіс
//    static void MyTask(Object ct)
//    {
//        CancellationToken cancelTok = (CancellationToken)ct;
//        // Тапсырманы іске қосудың алдында одан бас тартылғанын тексеру
//        cancelTok.ThrowIfCancellationRequested();
//        Console.WriteLine("MyTask() иске косылды");
//        for (int count = 0; count < 10; count++)
//        {
//            // Тапсырмадан бас тартылғанын бақылау үшін сұрастыру тәсілі қолданылады
//            if (cancelTok.IsCancellationRequested)
//            {
//                Console.WriteLine("Тапсырманы жоюга сураныс тусти");
//                cancelTok.ThrowIfCancellationRequested();
//            }
//            Thread.Sleep(500);
//            Console.WriteLine("MyTask() адисиндеги санауыш мани: " + count);
//        }
//        Console.WriteLine("MyTask аякталды");
//    }

//    static void Main()
//    {
//        Console.WriteLine("Негизги агын иске косылды");
//        // Тапсырмадан бас тарту белгілерінің көзін құру
//        CancellationTokenSource cancelTokSrc = new CancellationTokenSource();
//        // Тапсырманы іске қосу, тапсырмаға және делегатқа бас тарту белгісін беру
//        Task tsk = Task.Factory.StartNew(MyTask, cancelTokSrc.Token, cancelTokSrc.Token);
//        // Тапсырманың күші жойылғанға дейін орындалуына мүмкіндік беру
//        Thread.Sleep(2000);
//        try
//        {
//            // Тапсырманың орындалуынан бас тарту
//            cancelTokSrc.Cancel();
//            // tsk тапсырмасы аяқталғанға дейін Main() әдісінің орындалуын кідірту
//            tsk.Wait();
//        }
//        catch (OperationCanceledException ex)
//        {
//            Console.WriteLine(ex.Message);
//        }
//        catch (AggregateException exc)
//        {
//            if (tsk.IsCanceled)
//                Console.WriteLine("\ntsk тапсырмасынын орындалуынан бас тарту\n");
//            Console.WriteLine(exc);
//        }
//        finally
//        {
//            tsk.Dispose();
//            cancelTokSrc.Dispose();
//        }
//        Console.WriteLine("Негизги агын аякталды.");
//    }
//}




