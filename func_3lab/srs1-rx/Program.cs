using System.Collections.Generic;
using System.Linq;
using System;
using LaYumba.Functional;
using NUnit.Framework;
using static System.Console;

namespace srs1_rx
{
    using static F;
    using static System.Console;

     public class Option_Match_Example
    {
        internal static void _main()
        {
            string _ = null, john = "John";

            Greet(john); // prints: hello, John
            Greet(_); // prints: sorry, who?

            ReadKey();
        }

        static void Greet(string name)
           => WriteLine(GreetingFor(name));

        static string GreetingFor(string name)
           => Some(name).Match(
              Some: n => $"hello, {n}",
              None: () => "sorry, who?");
    }

    public class Option_Map_Example
    {
        internal static void _main()
        {
            Func<string, string> greet = name => $"hello, {name}";
            string _ = null, john = "John";

            Some(john).Map(greet); // => Some("hello, John")
            Some(_).Map(greet); // => None
        }
    }

    class Person
    {
        public string Name;
        public Option<Relationship> Relationship;
    }

    class Relationship
    {
        public string Type;
        public Person Partner;
    }

    public static class Option_Match_Example2
    {
        internal static void _main()
        {
            Person dota = new Person { Name = "Dota 2" }
               , cs = new Person { Name = "cs go" }
               , l4 = new Person { Name = "Left 4 dead" };

            dota.Relationship = new Relationship
            {
                Type = "the best",
                Partner = cs
            };

            WriteLine(dota.RelationshipStatus());

            WriteLine(l4.RelationshipStatus());

            ReadKey();
        }

        static string RelationshipStatus(this Person p)
           => p.Relationship.Match(
              Some: r => $"{p.Name} with {r.Partner.Name} is {r.Type} ",
              None: () => $"{p.Name} is good");
    }
    class Program
    {
        static void Main(string[] args)
        {
            int userChoice = Convert.ToInt32(Console.ReadLine());
            if (userChoice == 1)
            {
                Option_Match_Example2._main();

            }
            if (userChoice == 1)
            {
                Option_Match_Example._main();

            }
            Console.Read();
        }
    }
}