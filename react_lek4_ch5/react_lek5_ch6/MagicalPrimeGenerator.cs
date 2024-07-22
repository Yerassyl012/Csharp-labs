using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace react_lek5_ch6
{
    public class MagicalPrimeGenerator
    {
        public IEnumerable<int> Generate(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                yield return GeneratePrime(i);//yield return арқылы коллекцияны қайтару
            }
        }

        public async Task<IReadOnlyCollection<int>> GenerateAsync(int amount)
        {
            return await Task.Run(() => Generate(amount).ToList().AsReadOnly());
        }
        //Осы уақытқа дейін сіз көрген барлық бақыланатын мәндерден ауытқулар бақыланатындар жазылған кезде бірден пайда болды.
        //сондықтанда бақылаушының жазылудан бас тартуға мүмкіндігі болмады.
        public IObservable<int> GeneratePrimes_ManualAsync(int amount)
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

                return new CancellationDisposable(cts);//CancellationToken арқылы қайтатын бір реттік объект жалғаймыз
                                                       //ол үшін CancellationDisposable классы арқылы CancellationTokenSource пен жалғаймыз
            });
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
