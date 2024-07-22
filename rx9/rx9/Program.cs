using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using Helpers;
using System.Reactive;


namespace rx9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CombiningLatestValues();
            Console.WriteLine("---------------");
            MergingTwoAsyncOperations();
            Console.WriteLine("---------------");
            Switch();
            Console.WriteLine("---------------");
            ConcatTwoAsyncOperations();
            Console.WriteLine("---------------");
            GroupBy();
            Console.WriteLine("---------------");
            GroupJoin();
            Console.WriteLine("---------------");
            UsingBufferWithAmount();
            Console.WriteLine("---------------");
            AggreagateResultInAWindow();
        }

        private static void CombiningLatestValues()
        {
            Demo.DisplayHeader("The CombineLatest operator - combines values from the observables whenever any of the observable sequences produces an element");

            Subject<int> heartRate = new Subject<int>();
            Subject<int> speed = new Subject<int>();

            speed
                .CombineLatest(heartRate,
                               (s, h) => String.Format("Heart:{0} Speed:{1}", h, s))
                .SubscribeConsole("Metrics");

            heartRate.OnNext(200);
            heartRate.OnNext(201);
            heartRate.OnNext(202);
            speed.OnNext(30);
            speed.OnNext(31);
            heartRate.OnNext(203);
            heartRate.OnNext(204);
        }

        private static void MergingTwoAsyncOperations()
        {
            Demo.DisplayHeader("The Merge operator - merges the notifications from the source observables into a single observable sequence");

            Task<string[]> facebookMessages = Task.Delay(10).ContinueWith(_ => new[] { "Facebook1", "Facebook2" });//this will finish after 10 milis
            Task<string[]> twitterStatuses = Task.FromResult(new[] { "Twitter1", "Twitter2" }); //this will finish immidiatly

            Observable.Merge(
                    facebookMessages.ToObservable(),
                    twitterStatuses.ToObservable())
                .SelectMany(messages => messages)
                .RunExample("Merged Messages");

        }
        private static void Switch()
        {
            Demo.DisplayHeader("The Switch operator - takes an observable that emits observables and creates a single observable that emits the notification from the most recent observable");


            var textsSubject = new Subject<string>();
            IObservable<string> texts = textsSubject.AsObservable();
            texts
                .Select(txt => Observable.Return(txt + "-Result").Delay(TimeSpan.FromMilliseconds(txt == "R" ? 10 : 0)))//adding delay to R results
                .Switch()
                .SubscribeConsole("Merging from observable");

            textsSubject.OnNext("R");
            textsSubject.OnNext("Rx");
            Thread.Sleep(20);//adding a short delay so the system will have time to process Rx results
            textsSubject.OnNext("RX");

            Thread.Sleep(20);//short delay so we could see the printouts before moving to the next example
        }


        private static void ConcatTwoAsyncOperations()
        {
            Demo.DisplayHeader("The Concat operator - Concatenates the second observable sequence to the first observable sequence upon successful termination of the first");

            Task<string[]> facebookMessages = Task.Delay(10).ContinueWith(_ => new[] { "Facebook1", "Facebook2" });//this will finish after 10 milis
            Task<string[]> twitterStatuses = Task.FromResult(new[] { "Twitter1", "Twitter2" }); //this will finish immidiatly

            Observable.Concat(
                facebookMessages.ToObservable(),
                twitterStatuses.ToObservable())
                .SelectMany(messages => messages)
                .RunExample("Concat Messages");


        }
        private static void GroupBy()
        {
            var people = ObservableEx.FromValues(
                new Person() { Gender = Gender.Male, Age = 21 },
                new Person() { Gender = Gender.Female, Age = 31 },
                new Person() { Gender = Gender.Male, Age = 23 },
                new Person() { Gender = Gender.Female, Age = 33 });

            var genderAge =
                from gender in people.GroupBy(p => p.Gender)
                from avg in gender.Average(p => p.Age)
                select new { Gender = gender.Key, AvgAge = avg };

            genderAge.SubscribeConsole("Gender Age");

        }


        private static void GroupJoin()
        {
            Demo.DisplayHeader("The GroupJoin operator - correlates elements from two observables based on overlapping duration windows and put them in a correlation group");

            Subject<DoorOpened> doorOpenedSubject = new Subject<DoorOpened>();
            IObservable<DoorOpened> doorOpened = doorOpenedSubject.AsObservable();

            var enterences = doorOpened.Where(o => o.Direction == OpenDirection.Entering);
            var maleEntering = enterences.Where(x => x.Gender == Gender.Male);
            var femaleEntering = enterences.Where(x => x.Gender == Gender.Female);

            var exits = doorOpened.Where(o => o.Direction == OpenDirection.Leaving);
            var maleExiting = exits.Where(x => x.Gender == Gender.Male);
            var femaleExiting = exits.Where(x => x.Gender == Gender.Female);

            var malesAcquaintances =
                maleEntering
                    .GroupJoin(femaleEntering,
                        male => maleExiting.Where(exit => exit.Name == male.Name),
                        female => femaleExiting.Where(exit => female.Name == exit.Name),
                        (m, females) => new { Male = m.Name, Females = females });

            var amountPerUser =
                from acquinteces in malesAcquaintances
                from cnt in acquinteces.Females.Scan(0, (acc, curr) => acc + 1)
                select new { acquinteces.Male, cnt };

            amountPerUser.SubscribeConsole("Amount of meetings per User");

            //
            // Using Query Syntax GroupJoin clause
            //
            var malesAcquaintances2 =
            from male in maleEntering
            join female in femaleEntering on maleExiting.Where(exit => exit.Name == male.Name) equals
                femaleExiting.Where(exit => female.Name == exit.Name)
                into females
            select new { Male = male.Name, Females = females };
            var amountPerUser2 =
               from acquinteces in malesAcquaintances2
               from cnt in acquinteces.Females.Scan(0, (acc, curr) => acc + 1)
               select new { acquinteces.Male, cnt };

            //amountPerUser2.SubscribeConsole("Amount of meetings per User (query syntax)");

            //This is the sequence you see in Figure 9.8
            doorOpenedSubject.OnNext(new DoorOpened("Bob", Gender.Male, OpenDirection.Entering));
            doorOpenedSubject.OnNext(new DoorOpened("Sara", Gender.Female, OpenDirection.Entering));
            doorOpenedSubject.OnNext(new DoorOpened("John", Gender.Male, OpenDirection.Entering));
            doorOpenedSubject.OnNext(new DoorOpened("Sara", Gender.Female, OpenDirection.Leaving));
            doorOpenedSubject.OnNext(new DoorOpened("Fibi", Gender.Female, OpenDirection.Entering));
            doorOpenedSubject.OnNext(new DoorOpened("Bob", Gender.Male, OpenDirection.Leaving));
            doorOpenedSubject.OnNext(new DoorOpened("Dan", Gender.Male, OpenDirection.Entering));
            doorOpenedSubject.OnNext(new DoorOpened("Fibi", Gender.Female, OpenDirection.Leaving));
            doorOpenedSubject.OnNext(new DoorOpened("John", Gender.Male, OpenDirection.Leaving));
            doorOpenedSubject.OnNext(new DoorOpened("Dan", Gender.Male, OpenDirection.Leaving));


        }





        private static void UsingBufferWithAmount()
        {
            Demo.DisplayHeader("The Buffer operator - gather items from an Observable into bundles.");

            IObservable<double> speedReadings = new[] { 50.0, 51.0, 51.5, 53.0, 52.0, 52.5, 53.0 } //in MPH
                .ToObservable();

            double timeDelta = 0.0002777777777777778; //1 second in hours unit

            var accelrations =
                from buffer in speedReadings.Buffer(count: 2, skip: 1)
                where buffer.Count == 2
                let speedDelta = buffer[1] - buffer[0]
                select speedDelta / timeDelta;

            accelrations.RunExample("Acceleration");
        }

        private static void AggreagateResultInAWindow()
        {
            Demo.DisplayHeader("The Window operator - each window is an observable that can be used with an aggregation function");

            var donationsWindow1 = ObservableEx.FromValues(50M, 55, 60);
            var donationsWindow2 = ObservableEx.FromValues(49M, 48, 45);

            IObservable<decimal> donations =
                donationsWindow1.Concat(donationsWindow2.DelaySubscription(TimeSpan.FromSeconds(1.5)));

            var windows = donations.Window(TimeSpan.FromSeconds(1));

            var donationsSums =
                from window in windows.Do(_ => Console.WriteLine("New Window"))
                from sum in window.Scan((prevSum, donation) => prevSum + donation)
                select sum;

            donationsSums.RunExample("donations in shift");


        }

    }
}

