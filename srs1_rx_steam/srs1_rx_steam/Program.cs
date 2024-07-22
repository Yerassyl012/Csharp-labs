using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using srs1_rx_steam;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace srs1_rx_steam
{
    internal class Program
    {
        private static StockTicker _stockTicker;
        static void Main(string[] args)
        {
            _stockTicker = new StockTicker();// StockTicker жариялау

            var stockMonitor = new StockMonitor(_stockTicker);

            ShowMenu();

            Console.ReadLine();
        }

        private static void ShowMenu() // меню (қай батырманы таңдау)
        {

            Console.WriteLine("Choose a simulation type (or x to exit):");
            Console.WriteLine("1) Manual     - you enter the symbol and price");       //бағасымен атын өзі беріп, кейін акциясын және қаншаға арзандағанын көрсетеді
            Console.WriteLine("2) Query     - games created by authors");              //авторы бойынша ойындарды қарау
            Console.WriteLine("3) Created     - new games created");                   //жаңа ойын жасау(string типтен observable жасау)
            Console.WriteLine("4) Buy     - buy the game in 5 seconds");               // 5 секунд ішінде ойын сатып алу
            Console.WriteLine("5) Cancel     - cancel to subscribe the game");         //ойынға 5 секунд ішінде жазылудан бас тарту 
            Console.WriteLine("6) Item Update     - delete past items and create new");//ескіні кетіріп жаңа ойын қарау

            var selection = Console.ReadLine();
            switch (selection)
            {
                case "1":
                    ManualSimulator(_stockTicker);
                    break;
                case "2":
                    NestedQueryExample();
                    break;
                case "3":
                    EnumerableToObservable();
                    break;
                case "4":
                    GeneratingWithObservable();
                    break;
                case "5":
                    SubscribeWithCancellationInsteadOfDisposable();
                    break;
                case "6":
                    Reconnecting();
                    break;
                case "x":
                    return;
                default:
                    Console.WriteLine("Unknown selection");
                    return;
            }
        }
        private static void ManualSimulator(StockTicker stockTicker)//мәнді қолмен енгізу және оның бағасын жаңарту
        {
            while (true)
            {
                Console.Write("enter symbol (or x to exit): ");
                var symbol = Console.ReadLine();
                if (symbol.ToLower() == "x")
                {
                    break;
                }
                Console.WriteLine("enter price: ");
                decimal price;
                if (decimal.TryParse(Console.ReadLine(), out price))
                {
                    stockTicker.Notify(new StockTick() { Price = price, QuoteSymbol = symbol });
                }
                else
                {
                    Console.WriteLine("price should be decimal");
                }
            }
        }
        private static void NestedQueryExample()
        {
            var authors = new[] { new Author(1, "James Rodriguez"), new Author(2, "yuiong Chen"), };
            var games = new[] { new Game("Left 4", 1), new Game("Farcry", 1), new Game("Valorant", 2), };
            //Сұрау авторлық жинақтағы әрбір авторды декарттық өнімге ұқсас ойын жинағындағы әрбір ойынмен салыстырады.
            var authorsGames =
                from author in authors
                from game in games
                where game.AuthorID == author.ID
                select author.Name + " create the game: " + game.Name;

            foreach (var authorsgame in authorsGames)
            {
                Console.WriteLine("created new games in Asia: ");
                Console.WriteLine(authorsgame);
            }


        }
        private static void EnumerableToObservable()
        {
            Demo.DisplayHeader("Enumerable to Observable");

            IEnumerable<string> names = new[] { "YNWA", "Football", "Warcraft", "Generals" };
            IObservable<string> observable = names.ToObservable();

            observable.SubscribeConsole("names");
        }
        private static void GeneratingWithObservable()
        {
            Console.WriteLine();
            Demo.DisplayHeader("Generating primes into observable ");

            var generator = new MagicalPrimeGenerator();

            var subscription = generator
                .GeneratePrimes_Sync(5)
                .Timestamp()//Timestamp операторын әрбір шығарылған элемент үшін уақыт белгісін көрсету үшін пайдаланамыз
                .SubscribeConsole("game buy time");

            Console.WriteLine("Game bought");//генерация бітпей Subscribe әдісі қайтпайды, сондықтан Timestamp арқылы уақыт белгісін қарап отырамыз
        }
        private static void SubscribeWithCancellationInsteadOfDisposable()
        {
            Demo.DisplayHeader("Subscribing with CancellationToken to replace the IDisposable");

            var cts = new CancellationTokenSource();
            cts.Token.Register(() => Console.WriteLine("Subscription to this game cancelled"));

            Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe(x => Console.WriteLine(x), cts.Token);

            Console.WriteLine("Cancelling in five seconds");
            cts.CancelAfter(TimeSpan.FromSeconds(5));

            cts.Token.WaitHandle.WaitOne();
        }
        private static void Reconnecting()
        {
            Demo.DisplayHeader("Косылатын бакыланатын элементты кайта косу");

            var connectableObservable =
                Observable.Defer(() => ChatServer.Current.ObserveMessages())//бастапқы Defer әдісі суық бақыланады, сондықтан әрбір бақылаушы біріктіру логикасын бөліседі.
                    .Publish();

            connectableObservable.SubscribeConsole("Dota 2 Screen");
            connectableObservable.SubscribeConsole("CS GO Statistics");

            var subscription = connectableObservable.Connect();//Connect қызметін шақырғанда бақылаушы хабарландыруларды ала бастайды 

            Console.WriteLine("--Агымдагы косылымды жою және кайта косу--");
            subscription.Dispose();//және жазылым нысанын жойған кезде оларды қабылдауды тоқтатады.
            subscription = connectableObservable.Connect();

            //Қосылымға екінші рет қоңырау шалған кезде, жаңа серверге негізгі қосылым орнатылады және бақылаушылар соған негізделген хабарларды алады.
            Thread.Sleep(5000);
        }
    }
}