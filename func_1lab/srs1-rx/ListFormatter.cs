using System.Collections.Generic;
using System.Linq;
using LaYumba.Functional;
using NUnit.Framework;
using static System.Console;

namespace srs1_rx
{
    using static ParallelEnumerable;
    using static Enumerable;

    public static class StringExt
    {
        public static string ToSentenceCase(this string s)
           => s.ToUpper()[0] + s.ToLower().Substring(1);//ToSentenceCase является чистым (его вывод строго определяется входными данными).
                                                        //Поскольку его вычисление зависит только от входного параметра, его можно без проблем сделать статическим. 
    }

    public class Numbered<T>
    {
        public Numbered(T Value, int Number)
        {
            this.Value = Value;
            this.Number = Number;
        }

        public int Number { get; set; }
        public T Value { get; set; }

        public override string ToString()
           => $"({Number}, {Value})";

        public static Numbered<T> Create(T Value, int Number)
           => new Numbered<T>(Value, Number);
    }


    



    class ListFormatter3
    {
        int counter;

        Numbered<T> ToNumberedValue<T>(T t) => new Numbered<T>(t, ++counter);


        string Render(Numbered<string> s) => $"{s.Number}. {s.Value}";

        public List<string> Format(IEnumerable<string> list)
           => list.AsParallel()
              .Select(StringExt.ToSentenceCase)
              .Select(ToNumberedValue)
              .OrderBy(n => n.Number)
              .Select(Render)
              .ToList();
    }

    class ListFormatter2
    {
        int counter;

        string PrependCounter(string s) => $"{++counter}. {s}";//PrependCounter увеличивает счетчик, поэтому он нечистый.
                                                                      //Поскольку это зависит от члена экземпляра - счетчика, вы не можете сделать его статичным. 

        public List<string> Format(List<string> list)
           => list
              .Select(StringExt.ToSentenceCase)
              .Select(PrependCounter)
              .ToList();

    }

    static class ListFormatter1
    {

        static IEnumerable<int> Naturals(int startingWith = 0)
        {
            while (true) yield return startingWith++;
        }

        public static IEnumerable<string> Format(IEnumerable<string> list)
           => list.AsParallel()
              .Select(StringExt.ToSentenceCase)
              .Zip(Naturals(startingWith: 1).AsParallel()//Операция сопряжения двух параллельных списков является обычной операцией в FP и называется Zip
                 , (s, i) => $"{i}. {s}");

        //Zip - это функция сопряжения: что делать с каждой парой предметов.
        //Zip может использоваться как метод расширения, поэтому вы можете написать метод Format, используя более свободный синтаксис

    }

}