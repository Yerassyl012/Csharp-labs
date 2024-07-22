using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Helpers;

namespace rx5
{
    class Program
    {
        static void Main(string[] args)
        {
            DelayingSubscriptionOnlyStartsWhenSubscribing();
            TakeUntil();
            TakeUntilAStopMessage();
            SkipUntil();

            Console.ReadLine();
        }



        private static void SkipUntil()
        {
            Demo.DisplayHeader("SkipUntil(observable) -бақыланатын құрылғы белгілі бір хабарламаны шығарған кезде хабарландырулар алуды бастаңыз");

            IObservable<string> messages =
                new[] { "реклама", "Game", "Выбрать", "Купить", "Играть" }.ToObservable();//Бақыланушы ретінде жаңа мәндер жасау

            IObservable<string> controlChannel = messages;

            messages.SkipUntil(controlChannel.Where(m => m == "Game"))//Game айнымалысынан бастау
                .SubscribeConsole();//кеңейтуші әдіс
        }

        private static void TakeUntilAStopMessage()
        {
            Demo.DisplayHeader("TakeUntil(observable) - бақылаушы мән белгілі бір хабарды шығарғанда тоқтайды");


            IObservable<string> messages = Observable.Range(1, 5)
                .Select(i => "Game" + i)
                .Concat(Observable.Return("STOP"))
                .Concat(Observable.Return("After Stop")); //соңғы хабарды бақылаушы байқамайды

            IObservable<string> controlChannel = messages;//қарапайым болу үшін басқару арнасы хабарлар сияқты бақыланатын болады

            messages
                .TakeUntil(controlChannel.Where(m => m == "STOP"))
                .RunExample("TakeUnti(STOP)");
        }

        private static void TakeUntil()
        {
            Demo.DisplayHeader("TakeUntil(observable) - 5 секундтан кейін бакыланатын сауле шыгарганда токтайды");

            //TakeUntil операторы белгілі бір уақыт белдеуіндегі абсолютті күн мен уақыт болып табылатын DateTimeOffset алады
            Observable.Timer(DateTimeOffset.Now, TimeSpan.FromSeconds(1))
                .Select(t => DateTimeOffset.Now)
                .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(5)))//TakeUntil үшін салыстырмалы уақыт жүктемесі жоқ, бұл мысал 5 секундта жазылудан бас тарту туралы
                .RunExample("TakeUntil(observable)");
        }


        private static void DelayingSubscriptionOnlyStartsWhenSubscribing()
        {
            Demo.DisplayHeader("DelaySubscription - кешігу бақылаушы жазылған кезде ғана басталады");

            Console.WriteLine("Бақыланатын құбырды жасау {0}", DateTime.Now);
            var observable =
    Observable.Range(1, 5)
        .Timestamp()
        .DelaySubscription(TimeSpan.FromSeconds(5));//DelaySubscription мәлімдемесі жазылым орындалатын нүктені белгілейтін TimeSpan немесе DateTimeOffset алады.
                                                    //Жазылымды 5 секундқа кейінге қалдыру жолы

            Console.WriteLine("2 секунд ұйықтау");
            Thread.Sleep(TimeSpan.FromSeconds(2));//ағын арқылы 2 секундқа шегеру

            Console.WriteLine("Консольге жазылу, бірақ нақты жазылым 5 секундтан кейін ғана болады");
            Console.WriteLine("Жазылым жасау {0}", DateTime.Now);//Консольге жазылу, бірақ нақты жазылым 5 секундтан кейін ғана болады

            observable.SubscribeConsole();

            Thread.Sleep(6000);
            Console.WriteLine("Done");
        }
    }
}