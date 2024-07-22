using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace rx_srs2
{
    enum OpenDirection
    {
        Entering,
        Leaving
    }
    class DoorOpened
    {
        public DoorOpened(string name, Tank Tank, OpenDirection direction)
        {
            Name = name;
            this.Tank = Tank;
            Direction = direction;
        }

        public string Name { get; set; }
        public OpenDirection Direction { get; set; }
        public Tank Tank { get; set; }
    }

    enum Tank
    {
        Shilka,
        Strela
    }
    class Program
    {
        private static void GroupJoin()
        {
            Demo.DisplayHeader("The GroupJoin operator - correlates elements from two observables based on overlapping duration windows and put them in a correlation group");

            Subject<DoorOpened> doorOpenedSubject = new Subject<DoorOpened>();
            IObservable<DoorOpened> doorOpened = doorOpenedSubject.AsObservable();

            var enterences = doorOpened.Where(o => o.Direction == OpenDirection.Entering);
            var maleEntering = enterences.Where(x => x.Tank == Tank.Shilka);
            var femaleEntering = enterences.Where(x => x.Tank == Tank.Strela);

            var exits = doorOpened.Where(o => o.Direction == OpenDirection.Leaving);
            var maleExiting = exits.Where(x => x.Tank == Tank.Shilka);
            var femaleExiting = exits.Where(x => x.Tank == Tank.Strela);

            var malesAcquaintances =
                maleEntering
                    .GroupJoin(femaleEntering,
                        male => maleExiting.Where(exit => exit.Name == male.Name),
                        female => femaleExiting.Where(exit => female.Name == exit.Name),
                        (m, females) => new { Male = m.Name, Females = females });

            var amountPerUser =
                from acquinteces in malesAcquaintances
                from cnt in acquinteces.Females.Scan(0, (acc, curr) => acc + 1)
                select new { acquinteces.Male, cnt };

            amountPerUser.SubscribeConsole("Amount of meetings per User");

            //
            // Using Query Syntax GroupJoin clause
            //
            var malesAcquaintances2 =
            from male in maleEntering
            join female in femaleEntering on maleExiting.Where(exit => exit.Name == male.Name) equals
                femaleExiting.Where(exit => female.Name == exit.Name)
                into females
            select new { Male = male.Name, Females = females };
            var amountPerUser2 =
               from acquinteces in malesAcquaintances2
               from cnt in acquinteces.Females.Scan(0, (acc, curr) => acc + 1)
               select new { acquinteces.Male, cnt };

            //amountPerUser2.SubscribeConsole("Amount of meetings per User (query syntax)");

            doorOpenedSubject.OnNext(new DoorOpened("Стрела-10", Tank.Strela, OpenDirection.Entering));
            doorOpenedSubject.OnNext(new DoorOpened("ЗСУ-23-4 Шилка", Tank.Shilka, OpenDirection.Entering));
            doorOpenedSubject.OnNext(new DoorOpened("Стрела-10", Tank.Strela, OpenDirection.Entering));
            doorOpenedSubject.OnNext(new DoorOpened("ЗСУ-23-4 Шилка", Tank.Shilka, OpenDirection.Leaving));
            doorOpenedSubject.OnNext(new DoorOpened("ЗСУ-23-4 Шилка", Tank.Shilka, OpenDirection.Entering));
            doorOpenedSubject.OnNext(new DoorOpened("Стрела-10", Tank.Strela, OpenDirection.Leaving));
            doorOpenedSubject.OnNext(new DoorOpened("Стрела-10", Tank.Strela, OpenDirection.Entering));
            doorOpenedSubject.OnNext(new DoorOpened("ЗСУ-23-4 Шилка", Tank.Shilka, OpenDirection.Leaving));
            doorOpenedSubject.OnNext(new DoorOpened("Стрела-10", Tank.Strela, OpenDirection.Leaving));
            doorOpenedSubject.OnNext(new DoorOpened("Стрела-10", Tank.Strela, OpenDirection.Leaving));


        }

        static void Main(string[] args)
        {
            List<BMP> bMPs = new List<BMP>()
            {
                new BMP("Стрела-10", 1984, 120, 5000, 3),
                new BMP("ЗСУ-23-4 Шилка", 1990, 23, 2400, 4)
            };

            Shooter[] sh = { new Strela10(1, "Yeldos", "Стрела-10", 18),
                new Shilka(2, "Temirlan", "ЗСУ-23-4 Шилка", 40),
                new Strela10(3, "Rizabek", "Стрела-10", 5),
                new Shilka(4,"Olzhas", "ЗСУ-23-4 Шилка", 17),
                new Strela10(5,"Gani", "Стрела-10", 11),
                new Shilka(6,"Nurken", "ЗСУ-23-4 Шилка", 38),
                new Strela10(7,"Dias", "Стрела-10", 6)};

            List<New_enemy> enemy = new List<New_enemy>()
            {
                new New_enemy(1, "вертолет", "KA-10", 5, "USA", 1000, 200),
                new New_enemy(2, "Дрон", "ft_123", 6, "Germany", 2000, 100),
                new New_enemy(3, "самолет", "Apachi", 2, "USA", 1200, 200),
                new New_enemy(4, "ракета", "M4A1", 5, "North Korea", 1600, 50),
                new New_enemy(5, "вертолет", "KA-10", 4, "Iran", 1700, 100),
                new New_enemy(6, "истребитель", "XA50", 7, "Great Britain", 4000, 800),
                new New_enemy(7, "Крылатая ракета", "F1", 4, "Japan", 1300, 500),
            };
            Shooter.Tirkeu(sh);
            for (int i = 0; i < sh.Length; i++)
            {
                sh[i].successfulShot();

            }
            Console.ReadKey();

            while (true)
            {
                Console.WriteLine("If you want to finish purchase write 0. Otherwise write 1.");
                int userChoice1 = Convert.ToInt32(Console.ReadLine());
                if (userChoice1 != 0)
                {
                    Console.WriteLine("Введите ID: ");
                    int passId = Convert.ToInt32(Console.ReadLine());
                    New_enemy.showAllinfos(enemy);
                    Console.WriteLine("Выберите информацию которого хотите посмотреть (напишите его ID): ");
                    int ticketId = Convert.ToInt32(Console.ReadLine());
                    //BMP.TicketPurchase(sh, enemy, passId, ticketId);

                }
                else
                {
                    break;
                }
            }
            Console.WriteLine("---------------");
            GroupJoin();


            Console.WriteLine("Thank you for purchase");
        }
    }
}
