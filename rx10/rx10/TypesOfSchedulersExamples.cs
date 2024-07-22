using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Helpers;

namespace rx10
{
    static class TypesOfSchedulersExamples
    {
        public static void Run()
        {
            NewThreadSchedulerExample();
            CurrentThreadSchedulerExample();
            ImmediateSchedulerExample();
            ImmediateSchedulerFutureSchedulingExample();
        }

        private static void NewThreadSchedulerExample()
        {
            Demo.DisplayHeader("NewThreadScheduler - Creates a new thread for each scheduling");

            var newThreadScheduler = NewThreadScheduler.Default;

            var countdownEvent = new CountdownEvent(2);

            newThreadScheduler.Schedule(Unit.Default,
                (s, _) =>
                {
                    Console.WriteLine("Action1 - Thread:{0}", Thread.CurrentThread.ManagedThreadId);
                    countdownEvent.Signal();
                });
            newThreadScheduler.Schedule(Unit.Default,
                (s, _) =>
                {
                    Console.WriteLine("Action2 - Thread:{0}", Thread.CurrentThread.ManagedThreadId);
                    countdownEvent.Signal();
                });

            countdownEvent.Wait();

        }
        

        private static void CurrentThreadSchedulerExample()
        {
            Demo.DisplayHeader("CurrentThreadScheduler - Uses the current thread (the caller thread) for each scheduling");


            var currentThreadScheduler = CurrentThreadScheduler.Instance;

            var countdownEvent = new CountdownEvent(2);

            Console.WriteLine("Calling thread: {0}", Thread.CurrentThread.ManagedThreadId);

            currentThreadScheduler.Schedule(Unit.Default,
                (s, _) =>
                {
                    Console.WriteLine("Action1 - Thread:{0}", Thread.CurrentThread.ManagedThreadId);
                    countdownEvent.Signal();
                });
            currentThreadScheduler.Schedule(Unit.Default,
                (s, _) =>
                {
                    Console.WriteLine("Action2 - Thread:{0}", Thread.CurrentThread.ManagedThreadId);
                    countdownEvent.Signal();
                });
            countdownEvent.Wait();

        }



        private static void ImmediateSchedulerExample()
        {
            Demo.DisplayHeader("ImmediateScheduler - Uses the current thread (the caller thread) for each scheduling but runs the scheduled action immediatly");


            var immediateScheduler = ImmediateScheduler.Instance;

            var countdownEvent = new CountdownEvent(2);

            Console.WriteLine("Calling thread: {0}", Thread.CurrentThread.ManagedThreadId);

            immediateScheduler.Schedule(Unit.Default,
                (s, _) =>
                {
                    Console.WriteLine("Action1 - Thread:{0}", Thread.CurrentThread.ManagedThreadId);
                    countdownEvent.Signal();
                });
            immediateScheduler.Schedule(Unit.Default,
                (s, _) =>
                {
                    Console.WriteLine("Action2 - Thread:{0}", Thread.CurrentThread.ManagedThreadId);
                    countdownEvent.Signal();
                });
            countdownEvent.Wait();

        }

        private static void ImmediateSchedulerFutureSchedulingExample()
        {
            Demo.DisplayHeader("ImmediateScheduler - Future dueTime will cause the ImmediateScheduler block until the dueTime");

            var immediateScheduler = ImmediateScheduler.Instance;

            var countdownEvent = new CountdownEvent(2);
            Console.WriteLine("Calling thread: {0} Current time: {1}", Thread.CurrentThread.ManagedThreadId, immediateScheduler.Now);

            immediateScheduler.Schedule(Unit.Default,
                TimeSpan.FromSeconds(2),
                (s, _) =>
                {
                    Console.WriteLine("Outer Action - Thread:{0}", Thread.CurrentThread.ManagedThreadId);
                    s.Schedule(Unit.Default,
                        (s2, __) =>
                        {
                            Console.WriteLine("Inner Action - Thread:{0}", Thread.CurrentThread.ManagedThreadId);
                            countdownEvent.Signal();
                            Console.WriteLine("Inner Action - Done:{0}", Thread.CurrentThread.ManagedThreadId);
                            return Disposable.Empty;
                        });
                    countdownEvent.Signal();
                    Console.WriteLine("Outer Action - Done");

                    return Disposable.Empty;
                });

            Console.WriteLine("After the Schedule, Time: {0}", immediateScheduler.Now);

            countdownEvent.Wait();

        }
    }
}