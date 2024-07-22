using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using LaYumba.Functional;
using NUnit.Framework;
using System.IO;
using System.Text;
using static System.Console;

namespace srs1_rx
{
    using static F;
    class Program
    {
        static void Main(string[] args)
        {
            int userChoice = Convert.ToInt32(Console.ReadLine());
            if (userChoice == 1)
            {
                Func<string, Option<Age>> parseAge = s
            => Int.Parse(s).Bind(Age.Of);

                parseAge("26");        // => Some(26)
                parseAge("notAnAge");  // => None
                parseAge("11111");     // => None
            }
            else if (userChoice == 2)
            {
                // call the overload that takes a Func<T>
                var contents = Instrumentation.Time("reading from file.txt"
                   , () => File.ReadAllText("C:\\Games\\file.txt"));

                // explicitly call with Func<Unit>
                Instrumentation.Time("reading from file.txt", () =>
                {
                    File.AppendAllText("file.txt", "New content!", Encoding.UTF8);
                    return Unit();
                });

                // call the overload that takes an Action
                Instrumentation.Time("reading from file.txt"
                   , () => File.AppendAllText("file.txt", "New content!", Encoding.UTF8));
            }

            else if (userChoice == 3)
            {
                try
                {
                    var empty = new NameValueCollection();//NameValueCollection представляет собой карту из строки в строку
                    var green = empty["green"];
                    WriteLine("green!");
                    var alsoEmpty = new Dictionary<string, string>();
                    var blue = alsoEmpty["blue"];
                    WriteLine("blue!");
                }
                catch (Exception ex)
                {
                    WriteLine(ex.GetType().Name);
                }

            }
        }
        
    }

}