using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Helpers
{
    /// <summary>
    /// Listing 4.2
    /// An observer that output to the console each time the OnNext, OnError and OnComplete occurs
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConsoleObserver<T> : IObserver<T>
    {
        private readonly string _name;

        public ConsoleObserver(string name = "")
        {
            _name = name;
        }

        public void OnNext(T value)
        {
            Console.WriteLine("{0} - OnNext({1})", _name, value);
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("{0} - OnError:", _name);
            Console.WriteLine("\t {0}", error);
        }

        public void OnCompleted()
        {
            Console.WriteLine("{0} - OnCompleted()", _name);
        }
    }
    public class Demo
    {
        public static void DisplayHeader(string header)
        {
            Console.WriteLine();
            Console.WriteLine("---- {0} ----", header);
        }
    }



    public static class Extensions
    {
        /// <summary>
        /// Subscribe an observer that prints each notificatio to the console output
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="observable"></param>
        /// <param name="name"></param>
        /// <returns>a disposable subscription object</returns>
        public static IDisposable SubscribeConsole<T>(this IObservable<T> observable, string name = "")
        {
            return observable.Subscribe(new ConsoleObserver<T>(name));
        }
        public static IObservable<T> Log<T>(this IObservable<T> observable, string msg = "")
        {
            return observable.Do(
                x => Console.WriteLine("{0} - OnNext({1})", msg, x),
                ex =>
                {
                    Console.WriteLine("{0} - OnError:", msg);
                    Console.WriteLine("\t {0}", ex);
                },
                () => Console.WriteLine("{0} - OnCompleted()", msg));
        }
    }
}