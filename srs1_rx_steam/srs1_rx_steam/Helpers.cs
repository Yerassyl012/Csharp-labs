using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srs1_rx_steam
{
    public class Demo
    {
        public static void DisplayHeader(string header)
        {
            Console.WriteLine();
            Console.WriteLine("---- {0} ----", header);
        }
    }
    public class ConsoleObserver<T> : IObserver<T>
    {
        private readonly string _name;

        public ConsoleObserver(string name = "")
        {
            _name = name;
        }

        public void OnNext(T value)
        {
            Console.WriteLine("{0} - Game:({1})", _name, value);
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

    public static class Extensions
    {
        public static IDisposable SubscribeConsole<T>(this IObservable<T> observable, string name = "")
        {
            return observable.Subscribe(new ConsoleObserver<T>(name));
        }
    }
}
