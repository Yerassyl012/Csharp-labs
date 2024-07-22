using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srs_2_steam
{
    internal class Database
    {
        public int user_tags { get; set; }
        public string upcoming_release { get; set;}
        public string most_follow_upcoming { get; set;}
        public DateTime release_calendar { get; set; }
        public DateTime game_release_by_year { get; set; }
        public string Developer { get; set; }
        public string Profile_badges { get; set; }
        public string publishers { get; set; }
    }
}
