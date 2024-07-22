using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace func_9lab
{
    struct Circle
    {
        public Circle(Point center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public Point Center { get; }
        public double Radius { get; }

        public double Area => PI * Pow(Radius, 2);
    }

    struct Point
    {
        public double X { get; }
        public double Y { get; }
        public Point(double x, double y) { X = x; Y = y; }
    }

    static class CircleExt
    {
        static Circle Scale(this Circle @this, double factor)
           => new Circle(@this.Center, @this.Radius * factor);
    }

}
