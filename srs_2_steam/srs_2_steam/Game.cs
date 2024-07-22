using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaYumba.Functional;
namespace srs_2_steam
{
    internal class Game
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime Date { get; set; }
        public string country { get; set; }
        public int rate { get; set; }
        public int price { get; set; }
        public string category { get; set; }
        public int download_rate { get; set; }
        public Game(int id, string name, DateTime dateTime, string country, int rate, int price, string category, int download_rate)
        {
            this.id = id;
            this.name = name;
            Date = dateTime;
            this.country = country;
            this.rate = rate;
            this.price = price;
            this.category = category;
            this.download_rate = download_rate;
        }
        public void Info()
        {
            Console.WriteLine($"id:{id}, name:{name}, rate:{rate}, price:{price}, category:{category}, download rate:{download_rate}");

        }

        public static void purchase(List<Ranking> ranking, List<Game> gam, int id, int Id, List<Person> person)
        {
            foreach (Game p in gam)
            {
                if (Id == p.id)
                {
                    foreach (Person r in person)
                    {
                        if (id == r.Id)
                        {
                            r.games.Add(p);
                            string freeTicket = Ranking.UseMatch(gam);
                            Console.WriteLine(freeTicket);

                            for (int i = 0; i < r.games.Count; i++)
                            {
                                Console.WriteLine(r.games[i].id + " " + r.games[i].name + " " + r.games[i].rate + " " + r.games[i].price + " " + r.games[i].category + " " + r.games[i].download_rate);
                            }
                        }
                    }
                }
            }
        }
        public static void costly(List<Game> gam, int Id, List<Person> person)
        {   
            int sum = gam.Select(x => x.price).Sum();
            foreach(Game p in gam)
            {
                if (Id == p.id)
                {
                    foreach (Person r in person)
                    {
                        
                        if (r.currentMoney > sum)
                        {
                            Console.WriteLine("у вас не хватает денег купить всех игр");
                        }
                    }
                }
            }
        }
        public static void free_games(List<Game> gam, int Id, List<Person> person)
        {
            int sum = gam.Select(x => x.price).Sum();
            foreach (Game p in gam)
            {
                if (p.price == 0)
                {
                    Console.WriteLine($"id:{p.id}, name:{p.name}, rate:{p.rate}, price:{p.price}, category:{p.category}, download rate:{p.download_rate}");
                }
            }
        }
        public override string ToString() => $"{name}, {price:C}";

        public static bool CompareSalary(Game e1, Game e2) =>
          e1.price < e2.price;
    }
}