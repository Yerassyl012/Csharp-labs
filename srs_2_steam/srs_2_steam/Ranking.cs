using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaYumba.Functional;
namespace srs_2_steam
{
    class Ranking : IComparable<Ranking>, IFormattable
    {
        public Ranking(int id, int top_rated_games, string top_rated_games_str, int top_owner, string top_profile_level, string top_seiling_games, int top_seiling_games_week, int most_followed_games, IEnumerable<int> year, IEnumerable<string> sales)
        {
            this.id = id;
            Top_rated_games = top_rated_games;
            Top_rated_games_str = top_rated_games_str;
            Top_owner = top_owner;

            Top_profile_level = top_profile_level;
            Top_seiling_games = top_seiling_games;
            Top_seiling_games_week = top_seiling_games_week;
            Most_followed_games = most_followed_games;
            games = new List<Person>();
            Year = year != null ? new List<int>(year) : new List<int>();
            Sales = sales != null ? new List<string>(sales) : new List<string>();
        }
        public List<Person> games { get; set; }
        public int id { get; set; }
        public int Top_rated_games { get; set; }
        public string Top_rated_games_str { get; set; }
        public int Top_owner { get; set;}
        public string Top_profile_level { get; set; }
        public string Top_seiling_games { get; set; }
        public int Top_seiling_games_week { get; set; }
        public int Most_followed_games { get; set; }

        public IEnumerable<string> Sales { get; }
        public IEnumerable<int> Year { get; }

        public override string ToString() => $"{Top_rated_games} {Top_rated_games_str}";

        public int CompareTo(Ranking other) => Top_rated_games_str.CompareTo(other?.Top_rated_games_str);

        public string ToString(string format) => ToString(format, null);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case null:
                case "N":
                    return ToString();
                case "F":
                    return Top_profile_level;
                case "L":
                    return Top_rated_games_str;
                case "C":
                    return Top_seiling_games;
                case "S":
                    return Top_rated_games.ToString();
                case "W":
                    return Top_seiling_games_week.ToString();
                case "A":
                    return $"{Top_rated_games} {Top_rated_games_str}, Top seiling game: {Top_seiling_games}; Top game: {Top_rated_games}, sale: {Top_seiling_games_week}";
                default:
                    throw new FormatException($"Format {format} not supported");
            }
        }
        public static Either<string, double> CalcAverage(List<Game> g)
        {
            double countRes = g.Select(x => x.price).Count();
            double sumRes = g.Select(x => x.price).Sum();

            if (countRes == 0)
                return "count of result cannot be 0";

            if (sumRes != 0 && Math.Sign(countRes) != Math.Sign(sumRes))
                return "countRes and sumRes cannot be negative";

            return sumRes / countRes;
        }
        public static string UseMatch(List<Game> x)
        {
            var message = CalcAverage(x).Match(
               Right: z => $"Result = {z}",
               Left: err => $"Invalid input = {err}");
            return message;
        }
    }
}