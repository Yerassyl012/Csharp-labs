using System;
using LaYumba.Functional;
using String = LaYumba.Functional.String;
namespace srs1_rx
{
    using System.Collections.Generic;
    using System.Linq;
    using static F;
    using static Console;

    public class Examples
    {
        public static void List_Map()
        {
            Func<int, int> plus3 = x => x + 3;

            var a = new[] { 2, 4, 6 };
            // => [2, 4, 6]

            var b = a.Map(plus3);
            // => [5, 7, 9]
        }

        public static void List_ForEach()
        {
            Enumerable.Range(1, 5).ForEach(Console.Write);
        }

        internal static void _main()
        {
            Option<string> name = Some("Enrico");

            name
               .Map(String.ToUpper)
               .ForEach(WriteLine);

            IEnumerable<string> names = new[] { "Constance", "Brunhilde" };

            names
               .Map(String.ToUpper)
               .ForEach(WriteLine);
        }
    }
    public struct Age
    {
        private int Value { get; }
        private Age(int value)
        {
            if (!IsValid(value))
                throw new ArgumentException($"{value} is not a valid age");

            Value = value;
        }

        private static bool IsValid(int age)
           => 0 <= age && age < 120;

        public static Option<Age> Of(int age)
           => IsValid(age) ? Some(new Age(age)) : None;

    }

    public static class AskForValidAgeAndPrintFlatteringMessage
    {

        public static void _main()
           => WriteLine($"Only {ReadAge()}! That's young!");

        static Option<Age> ParseAge(string s)//Функция parseAge использует Bind для объединения Int.Bind и Age.Of.
                                             //В результате parseAge объединяет проверку того, что строка представляет допустимое целое число,
                                             //и проверку того, что целое число является допустимым значением возраста.
           => Int.Parse(s).Bind(Age.Of);

        static Age ReadAge()
           => ParseAge(Prompt("Please enter your age")).Match(
              () => ReadAge(),
              (age) => age);

        static string Prompt(string prompt)
        {
            WriteLine(prompt);
            return ReadLine();
        }
    }

    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }

        public decimal Earnings { get; set; }
        public Option<int> Age { get; set; }

        public Person() { }

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
    public static class FunctionComposition
    {

        static string AbbreviateName(Person p)
           => Abbreviate(p.FirstName) + Abbreviate(p.LastName);

        static string AppendDomain(string localPart)
           => $"{localPart}@manning.com";

        static string Abbreviate(string s)
           => s.Substring(0, 2).ToLower();

        internal static void _main()
        {
            Func<Person, string> emailFor =
               p => AppendDomain(AbbreviateName(p));

            var joe = new Person("Joe", "Bloggs");
            var email = emailFor(joe); // => jobl@manning.com

            WriteLine(email);
            ReadKey();
        }
    }
    class Program
    {
        static void Main(string[] args)//Map принимает структуру и функцию и применяет функцию к внутреннему значению (ям) структуры
        {
            int userChoice = Convert.ToInt32(Console.ReadLine());
            if (userChoice == 1)
            {
                Examples._main();
            }
            if (userChoice == 2)
            {
                FunctionComposition._main();
            }
            Console.Read();
        }
    }
}