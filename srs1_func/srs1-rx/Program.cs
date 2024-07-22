using LaYumba.Functional;
using String = LaYumba.Functional.String;
using System.Text;
using NUnit.Framework;
using static System.Console;

namespace srs1_rx
{
    using static F;
    class Program
    {
        static void Main(string[] args)
        {
            List<Game> g = new List<Game>()
            {
                new Game(1, "CS GO", new DateTime(2010, 1, 20), "USA", 5, 0, "famous", 1),
                new Game(2, "Dota 2", new DateTime(2012, 8, 23), "UK", 5, 0, "famous", 2),
                new Game(3, "Left 4 dead", new DateTime(2015, 12, 10), "Sweden", 4, 20000, "good", 10),
                new Game(4, "Valorant", new DateTime(2020, 5, 1), "Germany", 4, 24000, "new", 7),
                new Game(5, "Warcraft 3", new DateTime(2004, 5, 2), "USA", 5, 1200, "legendary", 3),
                new Game(6, "Geometry Dash", new DateTime(2019, 4, 12), "Russia", 2, 400, "bad", 73),
            };
            int userChoice = Convert.ToInt32(Console.ReadLine());
            if (userChoice == 1)
            {
                Console.WriteLine("Using Match:");
                Game.UseMatch(g);
            }
            if (userChoice == 2)
            {

                Console.WriteLine("upper names:");
                var names = g.Select(x => x.name).ToList();
                IEnumerable<string> name = names.AsEnumerable();
                name
                   .Map(String.ToUpper)
                   .ForEach(Console.WriteLine);


            }
            if (userChoice == 3)
            {
                var contents = Instrumentation.Time("reading from file.txt"
                   , () => File.ReadAllText("C:\\Games\\file.txt"));

                // explicitly call with Func<Unit>
                Instrumentation.Time("reading from file.txt", () =>
                {
                    File.AppendAllText("file.txt", "New content!", Encoding.UTF8);
                    return Unit();
                });

                // call the overload that takes an Action
                Instrumentation.Time("reading from file.txt"
                   , () => File.AppendAllText("file.txt", "New content!", Encoding.UTF8));
            }
            if (userChoice == 4)
            {
                var shoppingList = new List<string> { "Dota 2", "CS GO", "Farcry" };
                new ListFormatter2()
                   .Format(shoppingList)
                   .ForEach(WriteLine);

                Read();
            }
        }
    }
}