using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab4
{
    class MagicalPrimeGenerator
    {
        public IObservable<int> GeneratePrimes(int amount)
        {
            var cts = new CancellationTokenSource();
            return Observable.Create<int>(o =>
            {
                Task.Run(() =>
                {
                    foreach (var prime in Generate(amount))
                    {
                        cts.Token.ThrowIfCancellationRequested();
                        o.OnNext(prime);
                    }
                    o.OnCompleted();
                }, cts.Token);
                return new CancellationDisposable(cts);
            });
        }



    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var generator = new MagicalPrimeGenerator();
            var subscription = generator
             .GeneratePrimes(5)
             .Timestamp()
             .SubscribeConsole("primes observable");
            Console.WriteLine("Generation is done");
            Console.ReadLine();

        }
    }
}
