using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using lab3_ch4.Chat;
using Helpers;

namespace lab3_ch4
{
    class Program
    {
        static void Main(string[] args) //main да әдістерді пайдалану
        {
            UsingObservableCreate();
            Console.WriteLine("----------");
            HandcraftedObservable();
            Console.WriteLine("----------");
            ChatExample.Run();//ChatExample классындағы Run әдісіне сілтеме, ол жерде бізде connection орындалып кейін хат жазу қол жетімді болады 
            Console.ReadLine();
        }

        private static void UsingObservableCreate()// ConsoleObserver қайта қайта жаңа экземпляр жасаудың және жазыла берудің орнына,
                                                   // сіздің орныңызға жасайтын SubscribeConsole атты кеңейту әдісі пайда болады
        {
            Console.WriteLine();
            Demo.DisplayHeader("Create операторын пайдалану");

            var numbers = ObserveNumbers(5);

            numbers.SubscribeConsole("Observable.Created(new game created) - ");
        }

        public static IObservable<int> ObserveNumbers(int amount)//сандар арқылы бақылаушы жасау
        {
            return Observable.Create<int>(observer =>// Create әдісі AnonymousObservable түріндегі бақыланатын экземпляр жасайды
                                                     // және сіз бақыланатын Subscribe әдісі ретінде берген делегатты тіркейді.(ObservableBase секілді)
            {
                for (int i = 0; i < amount; i++)
                {
                    observer.OnNext(i);
                }
                observer.OnCompleted();
                return Disposable.Empty;
            });
        }

        private static void HandcraftedObservable()// 5 бақыланушы шығарып кейін тоқтату әдісі
        {
            Console.WriteLine();
            Demo.DisplayHeader("Handcrafted Observable");

            var numbers = new NumbersObservable(5);
            var subscription =
                numbers.Subscribe(new ConsoleObserver<int>("update game number "));
        }
    }
}
