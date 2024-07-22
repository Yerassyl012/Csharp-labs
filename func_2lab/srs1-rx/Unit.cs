using System;
using System.Diagnostics;
using LaYumba.Functional;
using Microsoft.Extensions.Logging;
using Unit = System.ValueTuple;

namespace srs1_rx
{
    public static class Instrumentation
    {
        public static T Time<T>(ILogger log, string op, Func<T> f)
        {
            var sw = new Stopwatch();
            sw.Start();

            T t = f();

            sw.Stop();
            log.LogDebug($"{op} took {sw.ElapsedMilliseconds}ms");
            return t;
        }


        public static T Time<T>(string op, Func<T> f)
        {
            var sw = new Stopwatch();
            sw.Start();

            T t = f();

            sw.Stop();
            Console.WriteLine($"{op} took {sw.ElapsedMilliseconds}ms");
            return t;
        }

        public static void Time(string op, Action act)
           => Time<Unit>(op, act.ToFunc());
    }
}
