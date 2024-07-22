
using LaYumba.Functional;
using static LaYumba.Functional.F;
using NUnit.Framework;
using static System.Math;

namespace srs1_rx
{
    public class Either_Example1
    {
        public Option<int> id { get; set; }

        public int result { get; set; }
        public Either_Example1(int a, int b)
        {
            id = a;
            result = b;
        }

        public static Either<string, double> CalcAverage(List<Either_Example1> x)
        {
            double countRes = x.Select(x => x.result).Count();
            double sumRes = x.Select(x => x.result).Sum();

            if (countRes == 0)
                return "count of result cannot be 0";

            if (sumRes != 0 && Math.Sign(countRes) != Math.Sign(sumRes))
                return "countRes and sumRes cannot be negative";

            return sumRes / countRes;
        }

        public static void UseMatch(List<Either_Example1> x)
        {
            var message = CalcAverage(x).Match(
               Right: z => $"Result = {z}",
               Left: err => $"Invalid input = {err}");
            Console.WriteLine(message);
        }

        public static bool TestCalc(List<Either_Example1> x) => Either_Example1.CalcAverage(x).Match(_ => false, _ => true);

    }
}
