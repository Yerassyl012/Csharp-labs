using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace srs_2_steam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Game> g = new List<Game>
            {
                new Game(1, "CS GO", new DateTime(2010, 1, 20), "USA", 5, 0, "famous", 1),
                new Game(2, "Dota 2", new DateTime(2012, 8, 23), "UK", 5, 0, "famous", 2),
                new Game(3, "Left 4 dead", new DateTime(2015, 12, 10), "Sweden", 4, 20000, "good", 10),
                new Game(4, "Valorant", new DateTime(2020, 5, 1), "Germany", 4, 24000, "new", 7),
                new Game(5, "Warcraft 3", new DateTime(2004, 5, 2), "USA", 5, 1200, "legendary", 3),
                new Game(6, "Geometry Dash", new DateTime(2019, 4, 12), "Russia", 2, 400, "bad", 73),
            };
            List<Person> p = new List<Person>
            {
                new Person(1, "Zaq", 20000),
                new Person(2, "Ace", 1000),
                new Person(3, "Alem", 40000),
                new Person(4, "Bala", 0)
            };
            List<Ranking> r = new List<Ranking>
            {

            };
            Console.WriteLine("1. покупка игр" +
                "\n2. продажа игр");
            
            int userChoice = Convert.ToInt32(Console.ReadLine());
            
            if (userChoice == 1)
            {
                for (int i = 0; i < g.Count; i++)
                    {
                        g[i].Info();
                    }
                while (true)
                {
                    Console.WriteLine("If you want to finish purchase write 0. Otherwise write: 1, free games: 2");
                    
                    
                    int userChoice1 = Convert.ToInt32(Console.ReadLine());
                    if (userChoice1 == 1)
                    {
                        Console.WriteLine("Введите ваш ID: ");
                        int gamerId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Выберите игру который хотите купить (напишите его ID): ");
                        int gameId = Convert.ToInt32(Console.ReadLine());
                        Game.purchase(r, g, gamerId, gameId, p);

                    }
                    if (userChoice1 == 2)
                    {
                        Console.WriteLine("Введите ваш ID: ");
                        int gameId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("free games there: ");
                        Game.free_games(g, gameId, p);
                    }
                    if (userChoice1 == 0)
                    {
                        break;
                    }
                }
                Console.WriteLine("Спасибо за покупку");
                
            }
        }
    }
}