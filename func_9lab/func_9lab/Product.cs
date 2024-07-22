using static System.Linq.Enumerable;
using System.Threading.Tasks;
using System;   
using LaYumba.Functional;
using LaYumba.Functional.Data.LinkedList;
using String = LaYumba.Functional.String;
using static LaYumba.Functional.Data.LinkedList.LinkedList;
namespace func_9lab
{
    using static Console;
    class Product
    {
        public int Inventory { get; private set; }

        public void ReplenishInventory(int units)
           => Inventory += units;

        public void ProcessSale(int units)
           => Inventory -= units;
    }
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
    class Product_
    {
        int inventory;

        public bool IsLowOnInventory { get; private set; }
        public int Inventory
        {
            get { return inventory; }
            private set
            {
                inventory = value;

                IsLowOnInventory = inventory <= 5;
            }
        }
    }

    public class LocalMutationIsOk
    {
        int Sum(int[] ints)
        {
            var result = 0;
            foreach (int i in ints) result += i;
            return result;
        }

        public static void _main()
        {
            var nums = Range(-10000, 20001).Reverse().ToList();
            Parallel.Invoke(
               () => WriteLine(nums.Sum()),
               () => { nums.Sort(); WriteLine(nums.Sum()); });
        }

        public static void __main()
        {
            var nums = Range(-10000, 20001).Reverse().ToList();

            Action task1 = () => WriteLine(nums.Sum());
            Action task2 = () => { nums.Sort(); WriteLine(nums.Sum()); };

            Parallel.Invoke(task1, task2);
        }

        public static void WithIEnumerable()
        {
            var nums = Range(-10000, 20001).Reverse();

            Action task1 = () => WriteLine(nums.Sum());
            Action task2 = () => { nums.OrderBy(x => x); WriteLine(nums.Sum()); };

            Parallel.Invoke(task1, task2);
        }


        
    }
    public class CoyoExample
        {
            public static void _main()
            {
                var emails = List(" Some@Ema.il ");

                // eager evaluation: 2 passes through the list
                emails.Map(String.Trim)
                      .Map(String.ToLower)
                      .ForEach(Console.WriteLine);

                // lazy evaluation: coyo just composes the functions to be mapped
                var coyo = Coyo.Of<List<string>, string>(emails)
                   .Map(String.Trim)
                   .Map(String.ToLower);

                // composed functions are applied only when Run is called
                // with a single pass over the list
                var results = coyo.Run();
                results.ForEach(Console.WriteLine);
            }
        }
}
