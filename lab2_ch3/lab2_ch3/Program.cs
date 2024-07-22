using System;
using System.Collections.Generic;
using System.Linq;

namespace lab2_ch3
{
    class Program
    {
        static void Main(string[] args)
        {
            IntroToLinq.Run();
            Console.ReadLine();
        }
    }

    class IntroToLinq
    {
        public static void Run()
        {
            SimpleQueryOnList();
            Console.WriteLine("-----");
            NestedQueryExample();
            Console.WriteLine("-----");
            QueryWithAnonymousType();
            Console.WriteLine("-----");
            DeferredExecutionExample();
            Console.WriteLine("-----");
            UnderstandingYieldExample();
            Console.WriteLine("-----");
            FibonacciWithYieldExample();
            Console.WriteLine("-----");
            ModifiedWhereToExplainDeferedExecutionExample();
            Console.WriteLine("-----");
            DynamicLINQQueryExample();
            Console.WriteLine("-----");
        }

        private static void DynamicLINQQueryExample()
        {
            var numbers = new[] { 1000, 2000, 3000, 4000, 5000, 6000, 20000 };
            var query = numbers.Where(x => x % 2 == 0);
            if (/*some condition*/true)
            {
                query = query.Where(x => x > 5000);
            }
            if (/*another condition*/true)
            {
                query = query.Where(x => x > 7000);
            }
            foreach (var item in query)
            {
                Console.WriteLine("the most expensive games there is: $" + item);
            }
        }


        private static void ModifiedWhereToExplainDeferedExecutionExample()
        {
            Console.WriteLine("ModifiedWhereToExplainDeferedExecutionExample");
            var numbers = new[] { 1, 2, 3, 4, 5, 6 };
            var evenNumbers = numbers.WhereWithLog(x => x % 2 == 0);
            Console.WriteLine("before foreach");
            foreach (var number in evenNumbers)
            {
                Console.WriteLine("expensive game id:{0}", number);
            }
        }

        static IEnumerable<int> GenerateFibonacci()
        {
            int a = 0;
            int b = 1;
            yield return a;
            yield return b;
            while (true)
            {
                b = a + b;
                a = b - a;
                yield return b;
            }//yield return бойынша коллекцияны қайтару
        }
        private static void FibonacciWithYieldExample()
        {
            Console.WriteLine("10 Fibonacci items");
            foreach (var item in GenerateFibonacci().Take(10))
            {
                Console.WriteLine(item);
            }
        }

        static IEnumerable<string> GetGreetings()
        {
            yield return "Start";
            yield return "Dota 2";
        }
        private static void UnderstandingYieldExample()
        {
            foreach (var greeting in GetGreetings())
            {
                Console.WriteLine(greeting);
            }
        }//yield return әдіс ішінде пайдаланылғанда, оның қайтару мәні қайтарылған жинақтың бөлігі болып табылады(IEnumerable<string>)

        private static void DeferredExecutionExample()
        {
            var numbers = new List<int> { 1, 2, 3, 4 };
            var evenNumbers =
                from number in numbers
                where number % 2 == 0
                select number;

            numbers.Add(6);

            foreach (var number in evenNumbers)
            {
                Console.WriteLine(number);
            }
            Console.WriteLine();
        }


        private static void QueryWithAnonymousType()
        {
            var authors = new[] { new Author(1, "Абдул Измаил"), new Author(2, "Джесс Клифф"), };
            var games = new[] { new Game("Dota 2", 1), new Game("Cs 1.6", 2), new Game("Spirit of the North", 2), };

            var authorsGames =
                from author in authors
                from game in games
                where game.AuthorID == author.ID
                select new { author, game };

            foreach (var authorsgame in authorsGames)
            {
                Console.WriteLine("created new games in Europe: ");
                Console.WriteLine("{0} create the game: {1}", authorsgame.author.Name, authorsgame.game.Name);
            }
        }//егер екі анонимді түрі бірдей қасиеттермен жасалса, онда олар бір типті болады. Авторлардың кітаптарын іздеу мысалында біз әрбір автор-кітап жұбына жол жасадық;
         //оның орнына екі сипатты бірге инкапсуляциялайтын нысан жасай аласыз:


        private static void NestedQueryExample()
        {
            var authors = new[] { new Author(1, "James Rodriguez"), new Author(2, "yuiong Chen"), };
            var games = new[] { new Game("Left 4", 1), new Game("Farcry", 1), new Game("Valorant", 2), };
            //Сұрау авторлық жинақтағы әрбір авторды декарттық өнімге ұқсас кітап жинағындағы әрбір кітаппен салыстырады.
            var authorsGames =
                from author in authors
                from game in games
                where game.AuthorID == author.ID
                select author.Name + " create the game: " + game.Name;

            foreach (var authorsgame in authorsGames)
            {
                Console.WriteLine("created new games in Asia: ");
                Console.WriteLine(authorsgame);
            }


        }



        

        private static void SimpleQueryOnList()
        {

            var numbers = new List<int> { 1, 35, 22, 6, 10, 11 };
            var result = numbers.Where(x => x % 2 == 1)
                .Where(x => x > 10)
                .Select(x => x + 2)
                .Distinct()
                .OrderBy(x => x);
            //10-нан үлкен барлық тақ сандарды табу үшін бүтін сандар тізімінде LINQ сұрауын қолданатын және олардың әрқайсысына
            //2 мәнін қосқаннан кейін оларды сұрыптап және қайталамай қайтаратын бағдарлама
            Console.WriteLine("games which passed the experimentation");//объявлять игры которые прошли все проверки

            foreach (var number in result)
            {
                
                Console.Write("{0} ", number);
            }
            Console.WriteLine();
        }
    }
}