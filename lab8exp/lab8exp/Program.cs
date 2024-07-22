using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8exp
{
    abstract class GeomDene 
    {
        public abstract double Audan();
        public abstract double Kolem();
        public abstract void Shugary();
    }
    class Cylinder : GeomDene
    {
        public int R;
        public int h;
        public Cylinder(int R, int h)
        {
            this.R = R;
            this.h = h;
        }
        public override double Audan()
        {
            return Math.PI * Math.Pow(R, 2);
        }
        public override double Kolem()
        {
            return Math.PI * Math.Pow(R, 2) * h;
        }
        public override void Shugary()
        {
            Console.WriteLine($"Audan: {Audan()}, Kolem: {Kolem()}");
        }
    }
    class Tekshe : GeomDene
    {
        public int a;
        public Tekshe(int a) { this.a = a; }
        public override double Audan()
        {
            return 6 * Math.Pow(a, 2);
        }
        public override double Kolem()
        {
            return Math.Pow(a, 3);
        }
        public override void Shugary()
        {
            Console.WriteLine($"Audan: {Audan()}, Kolem: {Kolem()}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Cylinder c = new Cylinder(4, 5);
            c.Audan();
            c.Kolem();
            c.Shugary();

            Tekshe t = new Tekshe(4);
            t.Audan();
            t.Kolem();
            t.Shugary();
        }
    }
}
