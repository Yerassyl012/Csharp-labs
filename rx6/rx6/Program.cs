using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using Helpers;


namespace rx6
{// приложение системы онлайн игр(как steam)
    class Program
    {
        static void Main(string[] args)
        {
            PublishWithInitialValue();
            Console.WriteLine("-------");
            Reconnecting();
            Console.WriteLine("-------");
            ReplayTwo();
            Console.ReadLine();
        }

        private static void ReplayTwo()
        {
            Demo.DisplayHeader("Replay(2) - болашақ жазылған бақылаушы үшін соңғы екі элементті қайталайды");

            var observable = Observable.Interval(TimeSpan.FromSeconds(1))
                .Take(5)
                .Replay(2);//Мұнда кез келген жазылған бақылаушы үшін соңғы екі элементті ойнатуға мүмкіндік беретін әдіс берілген
            observable.Connect();
            observable.SubscribeConsole("PUBG update");
            Thread.Sleep(3000); //Келесі бақыланатынға жазылудан 3 секунд бұрын
            Console.WriteLine("subscribing the second game update");
            observable.SubscribeConsole("Outlast update");

            //waiting for the observable to complete
            observable.Wait();
        }


        private static void Reconnecting()
        {
            Demo.DisplayHeader("Қосылатын бақыланатын элементті қайта қосу");

            var connectableObservable =
                Observable.Defer(() => ChatServer.Current.ObserveMessages())//бастапқы Defer әдісі суық бақыланады, сондықтан әрбір бақылаушы біріктіру логикасын бөліседі.
                    .Publish();

            connectableObservable.SubscribeConsole("Dota 2 Screen");
            connectableObservable.SubscribeConsole("CS GO Statistics");

            var subscription = connectableObservable.Connect();//Connect қызметін шақырғанда бақылаушы хабарландыруларды ала бастайды 

            Console.WriteLine("--Ағымдағы қосылымды жою және қайта қосу--");
            subscription.Dispose();//және жазылым нысанын жойған кезде оларды қабылдауды тоқтатады.
            subscription = connectableObservable.Connect();
            //Қосылымға екінші рет қоңырау шалған кезде, жаңа серверге негізгі қосылым орнатылады және бақылаушылар соған негізделген хабарларды алады.
            Thread.Sleep(5000);
        }


        private static void PublishWithInitialValue()
        {
            Demo.DisplayHeader("Publish(initial-value) -бақылауға болатын, бірақ BehaviorSubject арқылы жарияланған");

            var coldObservable = Observable.Interval(TimeSpan.FromSeconds(1)).Take(5);// 5 секундта хабарлама жіберу 

            Console.WriteLine("Creating hot disconncted observable");
            var connectableObservable = coldObservable.Publish(-10);// -10 нан disconnected тен басталып келуі


            Console.WriteLine("Екі бақылаушыны қазір, ал үшіншісі тағы екі секундтан кейін жазылады");
            connectableObservable.SubscribeConsole("First");//кеңейту әдісі
            connectableObservable.SubscribeConsole("Second");

            Console.WriteLine("Бақыланатындарды қосу");
            connectableObservable.Connect();//Connect() әдісі басталғалы қалған 4 секундта хабарлама шығару

            Thread.Sleep(2000);
            Console.WriteLine("Үшінші бақылаушыға жазылу - ол ағымдағы мәнді алады");
            connectableObservable.SubscribeConsole("Third");//Үшінші бақылаушыға жазылу - ол ағымдағы мәнді алады

            Thread.Sleep(3000);
        }

    }
}