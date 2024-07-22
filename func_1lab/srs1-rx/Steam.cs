using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srs1_rx
{
    internal class Steam
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Game { get; set; }
        public int Price { get; set; }
        public DateTime DateTime { get; set; }
        public Steam(int a, string b, int c, DateTime d)
        {
            Id = a;
            Game = b;
            Price = c;
            DateTime = d;
        }
    }
}
