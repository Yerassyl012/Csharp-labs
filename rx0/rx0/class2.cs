using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rx0
{
    internal class Position
    {
        public double X { get; set; }
        public double Y { get; set; }

        public static double Distance(Position left, Position right)
        {
            var diff = new Position()
            {
                X = left.X - right.X,
                Y = left.Y = right.Y
            };
            return DotProduct(diff, diff);
        }

        public static double DotProduct(Position left, Position right)
        {
            return Math.Sqrt(left.X * right.X + left.Y * right.Y);
        }
    }
    public enum Connectivity
    {
        Online,
        Offline
    }
    internal class Discount
    {
    }
    class Store
    {
        public Position Location { get; set; }
        public string Name { get; set; }

    }
}
