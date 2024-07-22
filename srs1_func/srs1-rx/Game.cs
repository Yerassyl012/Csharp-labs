using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using LaYumba.Functional;


namespace srs1_rx
{
    using static Enumerable;
    using static ParallelEnumerable;
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
        public static class FunctionComposition
        {

            static string AbbreviateName(Game p)
               => Abbreviate(p.name);

            static string AppendDomain(string localPart)
               => $"{localPart}@gmail.com";

            static string Abbreviate(string s)
               => s.Substring(0, 2).ToLower();

            public static Func<Game, string> game =
                p => AppendDomain(AbbreviateName(p));



        }

        public static Either<string, double> CalcAverage(List<Game> x)
        {
            double countRes = x.Select(x => x.price).Count();
            double sumRes = x.Select(x => x.price).Sum();

            if (countRes == 0)
                return "count of result cannot be 0";

            if (sumRes != 0 && Math.Sign(countRes) != Math.Sign(sumRes))
                return "countRes and sumRes cannot be negative";

            return sumRes / countRes;
        }
        public static void UseMatch(List<Game> x)
        {
            var message = CalcAverage(x).Match(
               Right: z => $"Result = {z}",
               Left: err => $"Invalid input = {err}");
            Console.WriteLine(message);
        }

        public static bool TestCalc(List<Game> x) => Game.CalcAverage(x).Match(_ => false, _ => true);

        
        
    }
    class ListFormatter2
        {
            int counter;

            string PrependCounter(string s) => $"{++counter}. {s}";//PrependCounter увеличивает счетчик, поэтому он нечистый.
                                                                   //Поскольку это зависит от члена экземпляра - счетчика, вы не можете сделать его статичным. 

            public List<string> Format(List<string> list)
               => list
                  .Select(StringExt.ToSentenceCase)
                  .Select(PrependCounter)
                  .ToList();

        }
    public static class StringExt
        {
            public static string ToSentenceCase(this string s)
               => s.ToUpper()[0] + s.ToLower().Substring(1);//ToSentenceCase является чистым (его вывод строго определяется входными данными).
                                                            //Поскольку его вычисление зависит только от входного параметра, его можно без проблем сделать статическим. 
        }
}
