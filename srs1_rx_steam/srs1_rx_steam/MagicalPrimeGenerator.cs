using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace srs1_rx_steam
{
    internal class MagicalPrimeGenerator
    {
        public IEnumerable<int> Generate(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                yield return GeneratePrime(i);//yield return арқылы коллекцияны қайтару
            }
        }
        public IObservable<int> GeneratePrimes_Sync(int amount) //Әдіс әлі де генерацияланған жай сандарды параметр ретінде қабылдайды,
                                                                //бірақ қазір ол IObservable<int> түріндегі бақыланатын мәнді қайтарады.
        {
            return Observable.Create<int>(o =>
            {
                foreach (var prime in Generate(amount))
                {
                    o.OnNext(prime);
                }
                o.OnCompleted();
                return Disposable.Empty;
            });
        }

        int GeneratePrime(int index)
        {
            // Simulate the hard work 
            Thread.Sleep(2000);

            var firstNumbers = new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, };
            if (index < firstNumbers.Length)
            {
                return firstNumbers[index];
            }

            return firstNumbers.Last();
        }
    }
}
