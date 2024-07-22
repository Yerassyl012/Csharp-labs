//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace srs_2_steam.DataLib
//{
//    public static class DataLib
//    {
//        private static List<Ranking> s_racers;

//        private static List<Ranking> InitializeRacers() =>
//            new List<Ranking>
//            {
//                new Ranking(1, "CS GO", 11, "Hiltop", "Unity1", 110, 300, new int[] { 2000 }, new string[] { "High" }),
//                new Ranking(2, "Dota 2", 12, "Ilon", "Wonderland", 120, 400, new int[] { 1980 }, new string[] { "Low" }),
//                new Ranking(3, "Valorant", 13, "Yield", "Ghostwire", 120, 500, new int[] { 2010 }, new string[] { "Mid" }),
//                new Ranking(4, "L4D", 14, "Weldom", "Cyberpunk", 140, 600, new int[] { 1999 }, new string[] { "Low" }),
//                new Ranking(5, "Bleach", 15, "Eriksen", "Stellaris", 150, 700, new int[] { 2015 }, new string[] { "Null" }),
//                new Ranking(6, "Jujutsu Kaisen", 16, "Spider-man", "Unity1", 160, 800, new int[] { 2020 }, new string[] { "High" }),
//                new Ranking(7, "Ion Disc", 17, "Alex", "Stray", 170, 900, new int[] { 2006 }, new string[] { "Very High" }),
//            };
//        public static IList<Ranking> GetChampions() => s_racers ?? (s_racers = InitializeRacers());

//        private static List<Game> s_teams;
//        public static IList<Game> GetConstructorChampions()
//        {
//            if (s_teams == null)
//            {
//                s_teams = new List<Game>()
//                {
//                    new Game(1, "CS GO", new DateTime(2010, 1, 20), "USA", 5, 0, "famous", 1),
//                new Game(2, "Dota 2", new DateTime(2012, 8, 23), "UK", 5, 0, "famous", 2),
//                new Game(3, "Left 4 dead", new DateTime(2015, 12, 10), "Sweden", 4, 20000, "good", 10),
//                new Game(4, "Valorant", new DateTime(2020, 5, 1), "Germany", 4, 24000, "new", 7),
//                new Game(5, "Warcraft 3", new DateTime(2004, 5, 2), "USA", 5, 1200, "legendary", 3),
//                new Game(6, "Geometry Dash", new DateTime(2019, 4, 12), "Russia", 2, 400, "bad", 73),
//                };
//            }
//            return s_teams;
//        }
//        private static IList<Ranking> _moreRacers;
//        private static IList<Ranking> GetMoreRacers()
//        {
//            if (_moreRacers == null)
//            {

//            }
//            return _moreRacers;
//        }
//    }
//}
