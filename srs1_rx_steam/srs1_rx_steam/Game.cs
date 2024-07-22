using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srs1_rx_steam
{
    class Game
    {
        public Game(string name, int authorID)
        {
            Name = name;
            AuthorID = authorID;
        }
        public string Name { get; set; }
        public int AuthorID { get; set; }
    }
    class Author
    {
        public Author(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public int ID { get; set; }
        public string Name { get; set; }
    }

}
