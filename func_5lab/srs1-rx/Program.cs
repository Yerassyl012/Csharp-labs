using System;
using System.Collections.Generic;
using System.Linq;
namespace srs1_rx
{
    public class Program
    {
        public static void Main()
        {
            List<Either_Example1> e = new List<Either_Example1>
            {
                new Either_Example1(1, 20),
                new Either_Example1(2, 30)
            };


            Candidate c = new Candidate("Erasyl");
            
            int userChoice = Convert.ToInt32(Console.ReadLine());
            if (userChoice == 1)
            {
                Console.WriteLine("Using Match:");
                Either_Example1.UseMatch(e);
            }
            if (userChoice == 2)
            {
                Console.WriteLine(" results with Using Match");
                Interview_Example_Option.FirstRound(c);
            }
        }
    }
}