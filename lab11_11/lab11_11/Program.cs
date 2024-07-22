using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab11_11
{
    delegate void Operation(Sport s);
    class Sport
    {
        public int ID;
        public string Name;
        public double Ver;
        public string Categorya;
        public Sport(int ID, string Name, double Ver, string Categorya)
        {
            this.ID = ID;
            this.Name = Name;
            this.Ver = Ver;
            this.Categorya = Categorya;
        }
        public event Operation Veroyat;
        public void OnVeroyat(Sport s)
        {
            if (Veroyat != null)
                Veroyat(s);
        }
        public static void Tirkeu(Sport[] s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                for (int j = i + 1; j < s.Length; j++)
                    if (s[i].Categorya == s[j].Categorya)
                    {
                        s[i].Veroyat += s[j].Ondeushi;
                        s[j].Veroyat += s[i].Ondeushi;
                    }
            }
        }
        public void Ondeushi(Sport s)
        {
            Console.WriteLine($"{this.Name} sportshy {s.Name} normany oryndamagany turaly khabarlama aldy");
        }
        public void Habarlama()
        {
            Random rnd = new Random();
            int i = rnd.Next(0, 21);
            double v = rnd.NextDouble();
            if (i == ID)
            {
                if (v >= Ver)
                {
                    Console.WriteLine($"{ this.Name} Normany oryndamady");
                }
                else
                {
                    OnVeroyat(this);
                    Console.WriteLine($"{ this.Name} Normany oryndamady");
                }

            }
        }


    }
    class Shangyshy : Sport
    {
        public Shangyshy(int ID, string Name, double Ver, string Categorya) : base(ID, Name, Ver, Categorya)
        { }
    }
    class Jugirushi : Sport
    {
        public Jugirushi(int ID, string Name, double Ver, string Categorya) : base(ID, Name, Ver, Categorya)
        {

        }
    }
    class Belosipedshi : Sport
    {
        public Belosipedshi(int ID, string Name, double Ver, string Categorya) : base(ID, Name, Ver, Categorya)
        { }
    }
    class Program
    {
        static void Main()
        {
            Sport[] s =
            {
                new Shangyshy(1, "Kelmenbetov",0.9, "Shangy"),
                new Shangyshy(2, "Elbrus",0.9, "Shangy"),
                new Shangyshy(3, "Serikbai",0.9, "Shangy"),
                new Shangyshy(4, "Abdikhallov",0.9, "Shangy"),
                new Shangyshy(5, "Khamzabek",0.9, "Shangy"),
                new Shangyshy(6, "Mukhamediaruly",0.9, "Shangy"),
                new Shangyshy(7, "Talap",0.9, "Shangy"),
                new Shangyshy(8, "Bagyt",0.9, "Shangy"),
                new Shangyshy(9, "Rysbek",0.9, "Shangy"),
                new Shangyshy(10, "Rakhimov",0.9, "Shangy"),
                new Shangyshy(11, "Ermekova",0.9, "Shangy"),
                new Shangyshy(12, "Serikkhalieva",0.9, "Shangy"),
                new Jugirushi(13, "Almaz",0.75, "Jugirushi"),
                new Jugirushi(14, "Erasyl",0.75, "Jugirushi"),
                new Jugirushi(15, "Murat",0.75, "Jugirushi"),
                new Jugirushi(16, "Akjol",0.75, "Jugirushi"),
                new Jugirushi(17, "Gibrat",0.75, "Jugirushi"),
                new Jugirushi(18, "Nazerke",0.75, "Jugirushi"),
                new Belosipedshi(19, "Saken",0.8, "Belosiped"),
                new Belosipedshi(20, "Guldiana",0.8, "Belosiped")
            };
            Sport.Tirkeu(s);
            for (int i = 0; i < s.Length; i++)
            {
                s[i].Habarlama();
            }

        }
    }

}
