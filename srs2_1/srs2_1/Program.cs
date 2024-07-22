using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srs2
{
    class Animal
    {
        public long AnimalId;
        public string NameOfAnimal;
        public string NickNameOfAnimal;
        public int Age;
        public int Weight;
        public DateTime YearOfBuy;
        public string Country;
        public string Climate;
        public string TypeOfRation;
        public int Price;
        public Diet diet;
        public Visit visit;
        public Animal(long a, string b, string c, int d, int e, DateTime f, string g, string h, string i, int o, Diet dd, Visit vv)
        {
            AnimalId = a;
            NameOfAnimal = b;
            NickNameOfAnimal = c;
            Age = d;
            Weight = e;
            YearOfBuy = f;
            Country = g;
            Climate = h;
            TypeOfRation = i;
            Price = o;
            diet = dd;
            visit = vv;
        }
        public void Display()
        {
            Console.WriteLine();
            Console.WriteLine($"Name of animal: {NameOfAnimal}\nNickname: {NickNameOfAnimal}\nAge: {Age}\nWeight: {Weight}\nYear of buy: {YearOfBuy}\nCountry: {Country}\nClimate: {Climate}\nType of ration: {TypeOfRation}\nPrice: {Price}");
        }
        public static void bagasy(List<Animal> a)// 3 - по всем животным – стоимость их содержания по убыванию;
        {
            var ba = a.AsParallel().OrderByDescending(q => q.Price);
            foreach (var n in ba)
            {
                Console.WriteLine();
                Console.WriteLine($"Name of animal: {n.NameOfAnimal}\n Price: {n.Price} ");
            }

        }
        //public static void kymbatzhanuar(List<Animal> a)// 4 - по наиболее дорогому животному (стоимость животного плюс стоимость содержания за один месяц) – все сведения;
        //{
        //    DateTime d1 = new DateTime(2020, 02, 20);
        //    DateTime d2 = new DateTime(2022, 03, 20);

    //    List<int> list = new List<int>();
    //        foreach (var anim in a)
    //        {
    //            var animal = a.Select(q => q.Price).Max();
    //    //var diet = d.Select(q=>q.PriceFeed).Max();
    //    list.Add(animal);
    //            //list.Add(diet);
    //        }
    //int max = list.Max();

    //    var kzh = a.Where(k=> k.Price == max).GroupBy(o => o.AnimalId).Select(i => new
    //    {
    //        name = i.Select(o => o.NameOfAnimal).First(),
    //        price = i.Select(o => o.Price).First(),
    //        pricefeed = i.Select(o=>o.diet.PriceFeed * o.Price).First()
    //    });
    //    int s = 0;
    //    foreach (var n in kzh)
    //    {
    //        Console.WriteLine();
    //        Console.WriteLine($" Name of animal: {n.name}\n Price: {n.price}\n sum:{n.pricefeed}");
    //    }
    //    //
    //}
} 
    


    class Diet
    {
        public long DietId;
        public string TypeOfRation;
        public string TypeOfFeed;
        public int PriceFeed;
        public int PriceWeight;
        public Animal animal;
        public Visit visit;
        public Diet(long a, string b, string c, int d, int e)
        {
            DietId = a;
            TypeOfRation = b;
            TypeOfFeed = c;
            PriceFeed = d;
            PriceWeight = e;
        }
        public static void Dengi(List<Animal> a) // 2 - по зоопарку – количество денег, необходимых на кормление животных за указанный период (день, месяц, год);
        {
            DateTime d1 = new DateTime(2022, 03, 14);
            DateTime d2 = new DateTime(2022, 03, 20);
            var dd = a.Where(k => d1 < k.visit.Data1 && k.visit.Data1 < d2).GroupBy(b => b.AnimalId).AsParallel()
                .Select(t => new
                {
                    typeofanimal = t.Select(i => i.NameOfAnimal).First(),
                    price = t.Sum(i => i.diet.PriceFeed)
                });
            foreach (var n in dd)
            {
                Console.WriteLine();
                Console.WriteLine($"typeofanimal = {n.typeofanimal} \nAll price = {n.price}");
            }
        }
    }

    class Visit
    {
        public long VisitId;
        public DateTime Data1;
        public int NumOfTicket;
        public int Visitor_age;
        public Animal animal;
        public Diet diet;
        public Visit(long a, DateTime b, int c, int d)
        {
            VisitId = a;
            Data1 = b;
            NumOfTicket = c;
            Visitor_age = d;
        }
        //public static void Posititeli(List<Visit> v)// 1 - по зоопарку – общее количество посетителей и сумму выручки за указанный период;
        //{
        //    DateTime d1 = new DateTime(2022, 03, 10);
        //    DateTime d2 = new DateTime(2022, 03, 13);
        //    var kn = v.Where(k => d1 < k.Data1 && k.Data1 < d2).GroupBy(t => t.VisitId)
        //        .Select(k=> new
        //         {
        //            NumOfTicket = k.Sum(i => i.NumOfTicket),
        //            price = k.Sum(i => i.animal.Price * i.NumOfTicket)
                    
        //         });
        //    foreach (var k in kn)
        //    {
        //        Console.WriteLine($"sum of ticket: {k.NumOfTicket}\n sum:{k.price}");
        //        Console.WriteLine();
        //    }
        //}

        //public static void PosCategory(List<Visit> v)// 5 - по зоопарку – сведения о количестве посетителей по возрастным категориям за указанный период.
        //{
        //    DateTime d1 = new DateTime(2022, 03, 14);
        //    DateTime d2 = new DateTime(2022, 03, 20);
        //    var kn = v.Where(k => d1 < k.Data1 && k.Data1 < d2).GroupBy(q => q.Visitor_age)
        //        .Select(t => new
        //    {
        //            NumOfTicket = t.Sum(i => i.NumOfTicket),
        //            visitors1 = t.Select(i => i.Visitor_age).First()
        //        });
        //    foreach (var k in kn)
        //    {
        //        Console.WriteLine();
        //    Console.WriteLine($"kolichestvo pos: {k.NumOfTicket}\nvozrast category: {k.visitors1}");

        //    }
            
        //}
    }

    class Program
    {
        static void Main(string[] args)
        {
            var diet = new List<Diet>
                {
                new Diet(1, "simple", "ex", 20, 1000),
                new Diet(2, "special", "re", 40, 2000),
                new Diet(3, "big", "qw", 30, 2300),
                new Diet(4, "small", "we", 10, 2100),
                new Diet(5, "firstly", "dx", 24, 12000),
                new Diet(6, "secondary", "vc", 43, 11000),
                new Diet(7, "expensive", "jt", 32, 8000)
            };

            var visit = new List<Visit>
                {
                new Visit(1, new DateTime(2022, 03, 11), 200, 22),
                new Visit(2, new DateTime(2022, 03, 12), 230, 10),
                new Visit(3, new DateTime(2022, 03, 13), 223, 40),
                new Visit(4, new DateTime(2022, 03, 14), 222, 4),
                new Visit(5, new DateTime(2022, 03, 15), 221, 25),
                new Visit(6, new DateTime(2022, 03, 16), 220, 51),
                new Visit(7, new DateTime(2022, 03, 17), 210, 65),
                new Visit(8, new DateTime(2022, 03, 18), 200, 35)
            };

            var animal = new List<Animal>
                {
                new Animal(1, "cat", "Lucy", 4, 5, new DateTime(2020, 12, 12), "Turkey", "rainy", "simple", 20000, diet[0], visit[0]),
                new Animal(2, "dog", "Barsik", 3, 14, new DateTime(2021, 04, 12), "Germany", "sunny", "special", 15000, diet[1], visit[1]),
                new Animal(3, "cow", "cow", 7, 30, new DateTime(2020, 02, 20), "Qazaqstan", "cloudy", "big", 60000, diet[2], visit[2]),
                new Animal(4, "dog", "John", 1, 10, new DateTime(2021, 12, 20), "USA", "sunny", "small", 10000, diet[3], visit[3]),
                new Animal(5, "monkey", "assasin", 2, 7, new DateTime(2021, 03, 25), "Ukraine", "snow", "firstly", 14000, diet[4], visit[4]),
                new Animal(6, "mouse", "splinter", 2, 1, new DateTime(2020, 03, 10), "Hungary", "cloudy", "secondary", 4000, diet[5], visit[5]),
                new Animal(7, "turtle", "leo", 10, 3, new DateTime(2019, 09, 10), "Columbia", "windy", "expensive", 9000, diet[6],visit[6]),
                new Animal(8, "camel", "rasul", 12, 22, new DateTime(2016, 12, 30), "Canada", "sunny", "simple", 70000, diet[0], visit[7]),
            };

            Console.WriteLine("Animal:");
            //foreach (var d in animal)
            //    d.Display();
            //Console.WriteLine("Diet:");
            //foreach (var d in animal)
            //    d.Display();
            //Console.WriteLine("Animal:");
            //foreach (var d in animal)
            //    d.Display();


            //Console.WriteLine("\n\n-----1st query-----"); //done
            //Visit.Posititeli(visit);

            Console.WriteLine("\n\n-----2nd query-----"); //done
            Diet.Dengi(animal);

            Console.WriteLine("\n\n-----3rd query-----");//done
            Animal.bagasy(animal);

            //Console.WriteLine("\n\n-----4th query-----");
            //Animal.kymbatzhanuar(animal);

            //Console.WriteLine("\n\n-----5th query-----");//done
            //Visit.PosCategory(visit);
        }
    }
}