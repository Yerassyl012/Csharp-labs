using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaYumba.Functional;
using static LaYumba.Functional.F;
using g_name = System.String;
using Greeting = System.String;
using PersonalizedGreeting = System.String;
using NUnit.Framework;
namespace func_lab_7
{
    public delegate Validation<T> Validator<T>(T t);
    internal class Game
    {
        static readonly Validator<int> Success = i => Valid(i);
        static readonly Validator<int> Failure = _ => Error("Invalid");

        public int id { get; set; }
        public string g_name { get; set; }
        public DateTime Date { get; set; }
        public string country { get; set; }
        public int rate { get; set; }
        public int price { get; set; }
        public string category { get; set; }
        public int download_rate { get; set; }
        public Game(int id, string g_name, DateTime dateTime, string country, int rate, int price, string category, int download_rate)
        {
            this.id = id;
            this.g_name = g_name;
            Date = dateTime;
            this.country = country;
            this.rate = rate;
            this.price = price;
            this.category = category;
            this.download_rate = download_rate;
        }
        internal static void Run(List<Game> sp)   //methods with delegates
        {
            string separator = "! ";

            Func<Greeting, g_name, PersonalizedGreeting> greet
               = (gr, name) => $"{gr}, {name}";

            Func<Greeting, Func<g_name, PersonalizedGreeting>> greetWith  //    Функции карри: оптимизированы для частичного применения
               = gr => name => $"{gr}{separator}{name}";

            var names = new List<g_name>();
            sp.ForEach(s =>
            {
                if (!names.Contains(s.g_name))
                    names.Add(s.g_name);
            });
            var s_names = names.ToArray();

            Console.WriteLine("\n Приветствие — с «нормальным» приложением с несколькими аргументами");
            s_names.Map(g => greet("Hello", g)).ForEach(Console.WriteLine);

            Console.WriteLine("\n Приветствие - официально - с частичным применением, вручную");
            var greetFormally = greetWith("Good evening");
            names.Map(greetFormally).ForEach(Console.WriteLine);

            Console.WriteLine("\n Приветствие - неформально - с частичным применением, общим");
            var greetInformally = greet.Apply("Hey");
            names.Map(greetInformally).ForEach(Console.WriteLine);

            Console.WriteLine("\n Приветствие - ностальгически - с частичным применением, карри");
            var greetNostalgically = greet.Curry()("Arrivederci");  //italian
            names.Map(greetNostalgically).ForEach(Console.WriteLine);
        }

        static Validator<T> FailFast<T>
         (IEnumerable<Validator<T>> validators)
         => t
         => validators.Aggregate(Valid(t)
            , (acc, validator) => acc.Bind(_ => validator(t)));

        //запускает все валидаторы, накапливая все ошибки валидации
        static Validator<T> HarvestErrors<T>
           (IEnumerable<Validator<T>> validators)
           => t =>
           {
               var errors = validators
                .Map(validate => validate(t))
                .Bind(v => v.Match(
                   Invalid: errs => Some(errs),
                   Valid: _ => None))
                .ToList();

               return errors.Count == 0
                ? Valid(t)
                : Invalid(errors.Flatten());
           };

        static Action WhenAllValidatorsSucceed_ThenSucceed = () => {   //Assert.AreEgual - метод для сравнения
            Assert.AreEqual(
           actual: FailFast(List(Success, Success))(3),     //Это объект, которого ожидают тесты.
           expected: Valid(3));                             //Это объект, созданный тестируемым кодом.
            Console.WriteLine("All Validators Succeed:  " + FailFast(List(Success, Success))(3) + " == " + Valid(3));
        };

        static Action WhenNoValidators_ThenSucceed = () =>
        {
            Assert.AreEqual(
           actual: FailFast(List<Validator<string>>())("success"),
           expected: Valid("success"));
            Console.WriteLine("No Validators:  " + FailFast(List<Validator<string>>())("success") + " == " + Valid("success"));
        };

        static Action WhenOneValidatorFails_ThenFail = () =>
        {
            HarvestErrors(List(Success, Failure))(1).Match(
               Valid: (_) => Assert.Fail(),
               Invalid: (errs) => Assert.AreEqual(1, errs.Count()));
            Console.WriteLine("One Validator Fails:  " + HarvestErrors(List(Success, Failure))(2));
        };

        static Action WhenSeveralValidatorsFail_ThenFail = () =>
        {
            HarvestErrors(List(Success, Failure, Failure, Success))(4).Match(
               Valid: (_) => Assert.Fail(),
               Invalid: (errs) => Assert.AreEqual(2, errs.Count()));
            Console.WriteLine("Several Validator Fails:  " + HarvestErrors(List(Success, Failure, Failure, Success))(4));
        };

        internal static void Run2()
        {
            Console.WriteLine("\n all success are returned:");
            WhenAllValidatorsSucceed_ThenSucceed();
            WhenNoValidators_ThenSucceed();
            Console.WriteLine("\n all errors are returned:");
            WhenOneValidatorFails_ThenFail();
            WhenSeveralValidatorsFail_ThenFail();
        }

    }
}


