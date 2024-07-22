using System;
using LaYumba.Functional;
using LaYumba.Functional.Data.LinkedList;
using String = LaYumba.Functional.String;
using static LaYumba.Functional.Data.LinkedList.LinkedList;
using func9lab_ex_.Chapter10.Data.Account.Immutable;
namespace func9lab_ex_
{
    using static Console;

    class DateTime_Example
    {
        internal static void _main()
        {
            var momsBirthday = new DateTime(1966, 12, 13);
            momsBirthday.AddDays(-7);
            WriteLine(momsBirthday);

            var johnsBirthday = momsBirthday;
            johnsBirthday = johnsBirthday.AddDays(1);
            WriteLine("Johns: " + johnsBirthday.Date);
            WriteLine("moms: " + momsBirthday.Date);
        }
    }

    static class ImmutableList_Example
    {
        public static void _main()
        {
            var fruit = List("pineapple", "banana");
            WriteLine(fruit);
            // => ["pineapple", "banana"]

            var tropicalMix = fruit.Add("kiwi");
            WriteLine(tropicalMix);
            // => ["kiwi", "pineapple", "banana"]

            var yellowFruit = fruit.Add("lemon");
            WriteLine(yellowFruit);
            // => ["lemon", "pineapple", "banana"]

            ReadKey();
        }

        public static int Sum(this List<int> @this)
           => @this.Match(
              Empty: () => 0,
              Cons: (head, tail) => head + tail.Sum());
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime_Example._main();
            Console.WriteLine("____________________");
            LocalMutationIsOk._main();
            LocalMutationIsOk.WithIEnumerable();
            Console.WriteLine("____________________");
            ImmutableList_Example._main();
            Console.WriteLine("____________________");
            Usage.Run();
        }
    }
}
