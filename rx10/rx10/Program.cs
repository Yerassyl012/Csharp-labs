using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Helpers;


namespace rx10
{
    class Program
    {
        static void Main(string[] args)
        {
            int userChoice = Convert.ToInt32(Console.ReadLine());
            if (userChoice == 1)
            {
                BasicScheduling();
            BasicSchedulingEveryTwoSeconds();
            ObservableIntervalOnCurrentThread();
            TypesOfSchedulersExamples.Run();

            }


            if (userChoice == 2)
            {
                AddingATimestampToNotifications();
                DelayingNotifications();
                

                OneObservableSynchronization();
                MultipleObservableSynchronization();


            }

            Console.WriteLine("Press <Enter> to continue...");
            Console.ReadLine();
        }


        private static void BasicScheduling()
        {
            Demo.DisplayHeader("Basic Scheduling - Scheduling an action to run after two seconds");

            IScheduler scheduler = NewThreadScheduler.Default;

            IDisposable scheduling =
                scheduler.Schedule(
                    Unit.Default,
                    TimeSpan.FromSeconds(2),
                    (scdlr, _) =>
                    {
                        Console.WriteLine("Hello World, Now: {0}", scdlr.Now);
                        return Disposable.Empty;
                    });

            Console.WriteLine("sleeping for 3 seconds so the scheduling will take place");
            Thread.Sleep(TimeSpan.FromSeconds(3));

        }

        private static void BasicSchedulingEveryTwoSeconds()
        {
            Demo.DisplayHeader("Basic Scheduling - Scheduling an action to run recursively every two seconds");

            IScheduler scheduler = NewThreadScheduler.Default;
            Func<IScheduler, int, IDisposable> action = null;
            action = (scdlr, callNumber) =>
            {
                Console.WriteLine("Hello {0}, Now: {1}, Thread: {2}", callNumber, scdlr.Now,
                    Thread.CurrentThread.ManagedThreadId);
                return scdlr.Schedule(callNumber + 1, TimeSpan.FromSeconds(2), action);
            };
            var scheduling =
                scheduler.Schedule(
                    0,
                    TimeSpan.FromSeconds(2),
                    action);

            Console.WriteLine("sleeping for 5 seconds and then disposing");

            Thread.Sleep(TimeSpan.FromSeconds(5));
            scheduling.Dispose();

            Console.WriteLine("scheduling disposed, Now: {0}", scheduler.Now);
        }

        private static void ObservableIntervalOnCurrentThread()
        {
            Demo.DisplayHeader(
                "Parametrizing concurrency - passing the CurrentThreadScheduler to the Interval operator so the emissions will be on the calling thread");

            Console.WriteLine("Before - Thread: {0}", Thread.CurrentThread.ManagedThreadId);
            Observable.Interval(TimeSpan.FromSeconds(1), CurrentThreadScheduler.Instance)
                .Timestamp()
                .Take(3)
                .Do(x => Console.WriteLine("Inside - {0} - Thread: {1}", x, Thread.CurrentThread.ManagedThreadId))
                .RunExample();
        }






        private static void DelayingNotifications()
        {
            Demo.DisplayHeader("Delay operator - shifts the observable sequence by a timespan");

            var observable = Observable
                .Timer(TimeSpan.FromSeconds(1))
                .Concat(Observable.Timer(TimeSpan.FromSeconds(1)))
                .Concat(Observable.Timer(TimeSpan.FromSeconds(4)))
                .Concat(Observable.Timer(TimeSpan.FromSeconds(4)));

            observable
                .Timestamp()
                .Delay(TimeSpan.FromSeconds(2))
                .Timestamp()
                .Take(5)
                .RunExample("Delay");
        }
        private static void AddingATimestampToNotifications()
        {
            Demo.DisplayHeader("Timestamp operator - adds a timestamp to every notification");
            IObservable<long> deviceHeartbeat =
                Observable.Interval(TimeSpan.FromSeconds(1));

            deviceHeartbeat
                 .Take(3)
                 .Timestamp()
                 .RunExample("Heartbeat");
        }




        private static void OneObservableSynchronization()
        {
            Demo.DisplayHeader("The Synchrnoize operator - synchronizes the notifications so they will be received in a seriazlied way");

            var messenger = new Messenger();
            var messages =
                Observable.FromEventPattern<string>(
                    h => messenger.MessageRecieved += h,
                    h => messenger.MessageRecieved -= h);

            messages
                .Select(evt => evt.EventArgs)
                .Synchronize()
                .Subscribe(msg =>
                {
                    Console.WriteLine("Message {0} arrived", msg);
                    Thread.Sleep(1000);
                    Console.WriteLine("Message {0} exit", msg);
                });

            for (int i = 0; i < 3; i++)
            {
                string msg = "msg" + i;
                ThreadPool.QueueUserWorkItem((_) =>
                {
                    messenger.Notify(msg);
                });
            }

            //waiting for all the other threads to complete before proceeding
            Thread.Sleep(2000);
        }

        private static void MultipleObservableSynchronization()
        {
            Demo.DisplayHeader("The Synchrnoize operator - can synchronizes the notifications from multiple observables by passing the gate object");

            var messenger = new Messenger();
            var messages =
                Observable.FromEventPattern<string>(
                    h => messenger.MessageRecieved += h,
                    h => messenger.MessageRecieved -= h);

            var friendRequests =
                Observable.FromEventPattern<FriendRequest>(
                    h => messenger.FriendRequestRecieved += h,
                    h => messenger.FriendRequestRecieved -= h);

            var gate = new object();

            messages
                .Select(evt => evt.EventArgs)
                .Synchronize(gate)
                .Subscribe(msg =>
                {
                    Console.WriteLine("Message {0} arrived", msg);
                    Thread.Sleep(1000);
                    Console.WriteLine("Message {0} exit", msg);
                });


            friendRequests
                .Select(evt => evt.EventArgs)
                .Synchronize(gate)
                .Subscribe(request =>
                {
                    Console.WriteLine("user {0} sent request", request.UserId);
                    Thread.Sleep(1000);
                    Console.WriteLine("user {0} approved", request.UserId);
                });
            for (int i = 0; i < 3; i++)
            {
                string msg = "msg" + i;
                string userId = "user" + i;
                ThreadPool.QueueUserWorkItem((_) =>
                {
                    messenger.Notify(msg);
                });

                ThreadPool.QueueUserWorkItem((_) =>
                {
                    messenger.Notify(new FriendRequest() { UserId = userId });
                });
            }
            //waiting for all the other threads to complete before proceeding
            Thread.Sleep(3000);
        }
    }
}