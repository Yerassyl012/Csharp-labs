using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaYumba.Functional;
using static LaYumba.Functional.F;
using g_name = System.String;
using Greeting = System.String;
using PersonalizedGreeting = System.String;
using NUnit.Framework;

namespace func_lab9
{
    internal class Game
    {
        public int id { get; set; }
        public string g_name { get; set; }
        public DateTime Date { get; set; }
        public string country { get; set; }
        public int rate { get; set; }
        public int price { get; set; }
        public string category { get; set; }
        public int download_rate { get; set; }
        public Game(int id, string g_name, DateTime dateTime, string country, int rate, int price, string category, int download_rate)
        {
            this.id = id;
            this.g_name = g_name;
            Date = dateTime;
            this.country = country;
            this.rate = rate;
            this.price = price;
            this.category = category;
            this.download_rate = download_rate;
        }
    }
}
