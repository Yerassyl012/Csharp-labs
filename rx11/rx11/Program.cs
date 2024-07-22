using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Subjects;
using System.IO;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading;
using Helpers;
using System.Runtime;

namespace rx11
{
    class Program
    {
        static void Main(string[] args)
        {
            int userChoice = Convert.ToInt32(Console.ReadLine());
            if (userChoice == 1)
            {
                BasicOnError();
                Console.WriteLine("_____________________");
                CatchingSpecificExceptionType();
                Console.WriteLine("_____________________");
                OnErrorResumeNext();
            }


            if (userChoice == 2)
            {
                TraditionalUsingStatement();
                Console.WriteLine("_____________________");
                TheUsingOperator();
                Console.WriteLine("_____________________");
                DeterministicDisposal();
                Console.WriteLine("_____________________");
                Finally();
                Console.WriteLine("_____________________");
                FinallyTestCases();

                Console.WriteLine("Press <Enter> to exit");
                Console.ReadLine();
            }
            if(userChoice == 3)
            {
                var subscription =
                Observable.Interval(TimeSpan.FromSeconds(1))
                    .AsWeakObservable()
                    .SubscribeConsole("Interval");

                Console.WriteLine("Collecting and sleeping for 2 seconds");
                GC.Collect();
                Thread.Sleep(2000); //2 seconds 

                GC.KeepAlive(subscription);
                Console.WriteLine("Done sleeping");
                Console.WriteLine("removing the strong reference, collecting and sleeping for 2 seconds");

                subscription = null;
                GC.Collect();
                Thread.Sleep(2000); //2 seconds 
                Console.WriteLine("Done sleeping");

                Console.ReadLine();
            }
            Console.ReadLine();
        }


        private static void OnErrorResumeNext()
        {
            Demo.DisplayHeader("OnErrorResumeNext operator - conct the second observable when the first completes or throws");

            IObservable<WeatherReport> weatherStationA =
              Observable.Throw<WeatherReport>(new OutOfMemoryException());

            IObservable<WeatherReport> weatherStationB =
              Observable.Return<WeatherReport>(new WeatherReport() { Station = "B", Temperature = 20.0 });

            weatherStationA
                .OnErrorResumeNext(weatherStationB)
                .SubscribeConsole("OnErrorResumeNext(source throws)");

            weatherStationB
                .OnErrorResumeNext(weatherStationB)
                .SubscribeConsole("OnErrorResumeNext(source completed)");

        }

        private static void CatchingSpecificExceptionType()
        {
            Demo.DisplayHeader("Catch operator");

            IObservable<WeatherSimulation> weatherSimulationResults =
              Observable.Throw<WeatherSimulation>(new OutOfMemoryException());

            weatherSimulationResults
                .Catch((OutOfMemoryException ex) =>
                {
                    Console.WriteLine("handling OOM exception");
                    return Observable.Empty<WeatherSimulation>();
                })
                .SubscribeConsole("Catch (source throws)");

            //Catch is not limited to a single exception type, it can be general to ALL exceptions 
            weatherSimulationResults
                .Catch(Observable.Empty<WeatherSimulation>())
                .SubscribeConsole("Catch (handling all exception types)");

            //of course, if the source observable completed successfully, then the Catch opertor has no effect (unlike OnErrorResumeNext) 
            Observable.Return(1)
                .Catch(Observable.Empty<int>())
                .SubscribeConsole("Catch (source completed successfully)");
        }

        private static void BasicOnError()
        {
            Demo.DisplayHeader("Basic OnError");

            // This the most basic way you would work with OnError.
            // But its not ideal, consider using the 'Catch' operator
            IObservable<WeatherSimulation> weatherSimulationResults =
                Observable.Throw<WeatherSimulation>(new OutOfMemoryException());

            weatherSimulationResults
                .Subscribe(
                    _ => { },
                    e =>
                    {
                        if (e is OutOfMemoryException)
                        {
                            //a last attampt to free some memory
                            GCSettings.LargeObjectHeapCompactionMode =
                                GCLargeObjectHeapCompactionMode.CompactOnce;
                            GC.Collect();
                            GC.WaitForPendingFinalizers();

                            Console.WriteLine("GC Done");
                        }
                    });
        }



        class DisposableType : IDisposable
        {
            public void Dispose() { /*Freeing the resource*/ }
        }
        private static void TraditionalUsingStatement()
        {
            using (var disposable = new DisposableType())
            {
                //Rest of code
            }
        }

        private static void TheUsingOperator()
        {
            Demo.DisplayHeader("The Using operator - gracefully dispose a resource when the observable terminates");

            string logFilePath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "example.log");
            IObservable<SensorData> sensorData =
                Observable.Range(1, 3)
                    .Select(x => new SensorData(x));

            var sensorDataWithLogging =
                Observable.Using(() => new StreamWriter(logFilePath),
                    writer =>
                    {
                        return sensorData.Do(x => writer.WriteLine(x.Data));
                    });

            sensorDataWithLogging.SubscribeConsole("sensor");


        }


        private static void DeterministicDisposal()
        {
            Demo.DisplayHeader("The Using operator will make sure that the resource is disposed no matter what caused the observable to stop");

            Subject<int> subject = new Subject<int>();
            var observable =
                Observable.Using(() => Disposable.Create(() => { Console.WriteLine("DISPOSED"); }),
                    _ => subject);

            Console.WriteLine();
            Console.WriteLine("Disposed when completed");
            observable.SubscribeConsole();
            subject.OnCompleted();

            Console.WriteLine();
            Console.WriteLine("Disposed when error occurs");
            subject = new Subject<int>();
            observable.SubscribeConsole();
            subject.OnError(new Exception("error"));

            Console.WriteLine();
            Console.WriteLine("Disposed when subscription disposed");
            subject = new Subject<int>();
            var subscription =
                observable.SubscribeConsole();
            subscription.Dispose();
        }


        private static void Finally()
        {
            IObservable<int> progress =
                Observable.Range(1, 3);

            progress
                .Finally(() => {/*close the window*/})
                .Subscribe(x => {/*Update the UI*/});

        }

        private static void FinallyTestCases()
        {
            Demo.DisplayHeader("The Finally operator - runs an action when the observable terminates, wither gracefully or due to an error");

            Console.WriteLine();
            Console.WriteLine("Successful complete");
            Observable.Empty<int>()
                .Finally(() => Console.WriteLine("Finally Code"))
                .SubscribeConsole();

            Console.WriteLine();
            Console.WriteLine("Error termination");
            Observable.Throw<Exception>(new Exception("error"))
                .Finally(() => Console.WriteLine("Finally Code"))
                .SubscribeConsole();

            Console.WriteLine();
            Console.WriteLine("Unsubscribing");
            Subject<int> subject = new Subject<int>();
            var subscription =
                subject.AsObservable()
                    .Finally(() => Console.WriteLine("Finally Code"))
                    .SubscribeConsole();
            subscription.Dispose();





        }
    }
}
