using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace sort
{
    public class Apple : IComparable<Apple>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int mass { get; set; }
        public Apple(int Id, string Name, int mass)
        {
            this.Id = Id;
            this.Name = Name;
            this.mass = mass;
        }
        public override string ToString()
        {
            return $"Name:{Name}, Mass:{mass}";
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Apple objAsApple = obj as Apple;
            if (objAsApple == null) return false;
            else return Equals(objAsApple);
        }
        public int CompareTo(Apple comparePart)
        {
            // A null value means that this object is greater.
            if (comparePart == null)
                return 1;

            else
                return this.Id.CompareTo(comparePart.Id);
        }
    }
    class Program
    {
        public static void Main(string[] args)
        {
            List<Apple> apple = new List<Apple>
            {
                new Apple(1, "Aport", 200),
                new Apple(2, "Klose", 900),
                new Apple(3, "Quinty", 100),
                new Apple(4, "Spartan", 2000),
                new Apple(5, "Walsey", 1300),
                new Apple(6, "Sava", 1200),
                new Apple(7, "Sava", 900),
                new Apple(8, "Aport", 1000),
                new Apple(9, "Lobo", 3100),
                new Apple(10, "Klose", 2200),
                new Apple(11, "Aport", 800),

            };

            var sortes = from app in apple
                         where app.Name == "Klose"
                         select app;
            foreach(var n in sortes)
            {
                Console.WriteLine(n);
            }
            var sortes2 = from app in apple
                          where app.Name == "Aport"
                         select app;
            foreach (var n in sortes2)
            {
                Console.WriteLine(n);
            }
            var sortes3 = from app in apple
                          where app.Name == "Sava"
                          select app;
            foreach (var n in sortes3)
            {
                Console.WriteLine(n);
            }

            Thread.Sleep(1000);
            apple.Sort(delegate (Apple x, Apple y)
            {
                if (x.mass == null && y.mass == null) return 0;
                else if (x.mass == null) return -1;
                else if (y.mass == null) return 1;
                else return x.mass.CompareTo(y.mass);
            });
            Console.WriteLine("\nAfter sort by apple mass:");
            foreach (Apple aApple in apple)
            {
                Console.WriteLine(aApple);
            }

            apple.Sort(delegate (Apple x, Apple y)
            {
                if (x.Name == null && y.Name == null) return 0;
                else if (x.Name == null) return -1;
                else if (y.Name == null) return 1;
                else return x.Name.CompareTo(y.Name);
            });

            Console.WriteLine("\nAfter sort by name:");
            foreach (Apple aApple in apple)
            {
                Console.WriteLine(aApple);
            }
        }
    }
}
