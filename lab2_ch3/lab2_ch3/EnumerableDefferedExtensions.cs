using System;
using System.Collections.Generic;

namespace lab2_ch3
{
    static class EnumerableDefferedExtensions
    {
        public static IEnumerable<T> WhereWithLog<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)//мәндерді жинақтау
            {
                Console.WriteLine("Game id: {0}", item);
                if (predicate(item))
                {
                    yield return item;// yield return арқылы коллекцияларды шығару
                }
            }
        }
    }
}