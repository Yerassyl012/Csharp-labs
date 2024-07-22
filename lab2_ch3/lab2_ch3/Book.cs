using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_ch3
{
    class Game
    {
        public Game(string name, int authorID)
        {
            Name = name;
            AuthorID = authorID;
        }
        public string Name { get; set; }
        //public string ISBN { get; set; }
        public int AuthorID { get; set; }
    }
}
