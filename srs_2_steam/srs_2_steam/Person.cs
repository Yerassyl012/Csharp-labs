using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srs_2_steam
{
    internal class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int currentMoney { get; set; }
        public List<Game> games { get; set; }
        public Person(int Id, string Name, int currentMoney)
        {
            this.Id = Id;
            this.Name = Name;
            this.currentMoney = currentMoney;
            games = new List<Game>();
        }

    }
}
