using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace srs1_rx
{
    using static Enumerable;
    using static Console;

    class Tenets
    {
        static void _main()
        {
            var ints = new List<int> { 1, 2, 3, 4, 5 };

            foreach (var i in ints)
                WriteLine(i);

            ints.ForEach(WriteLine);
        }
    }

    public class MutationShouldBeAvoided
    {
        [Test]
        public static void NoInPlaceUpdates()
        {
            var original = new[] { 5, 7, 1 };
            var sorted = original.OrderBy(x => x).ToList();

            Assert.AreEqual(new[] { 5, 7, 1 }, original);
            Assert.AreEqual(new[] { 1, 5, 7 }, sorted);
        }

        public static void WithIEnumerableItWorks()
        {
            var nums = Range(-10000, 20001).Reverse();

            Action task1 = () => WriteLine(nums.Sum());
            Action task2 = () => { nums.OrderBy(x => x); WriteLine(nums.Sum()); };

            Parallel.Invoke(task1, task2);
        }
    }
}
