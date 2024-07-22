using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_project
{

    public class PriceChangedEventArgs : EventArgs
    {
        public readonly string name;
        public PriceChangedEventArgs(string a)
        {
            name = a;
        }
    }
    class Passenger
    {
        public int pass_id;
        public string pass_name;
        public string pass_sname;
        public int pass_age;
        public List<Ticket> ticket; 
        public List<Aircraft> aircraft;
        public event EventHandler<PriceChangedEventArgs> SomeEvent;
        public Passenger(int ID, string pass_name, string pass_sname, int pass_age)
        {
            aircraft = new List<Aircraft>();
            ticket = new List<Ticket>();
            this.pass_id = ID;
            this.pass_name = pass_name;
            this.pass_sname = pass_sname;
            this.pass_age = pass_age;
        }
        public void OnSomeEvent(PriceChangedEventArgs ps) // оқиға тудырушы
        {
            if (SomeEvent != null)
                SomeEvent(this, ps);
        }
        public static void Tirkeu(List<Passenger> ps)   // өңдеушілерді тіркеу
        {
            for (int i = 0; i < ps.Count; i++)
            {
                for (int j = 0; j < ps.Count; j++)
                {
                    if (i != j)
                        ps[i].SomeEvent += ps[j].DisplayMessage;
                }
            }
        }
        public static void Passengerdata(string pass_name, List<Passenger> passengers) // жолаушы туралы малимет
        {
            bool ok = false;
            foreach (Passenger p in passengers)
            {
                if (p.pass_name == pass_name)
                {
                    p.Shygaru();
                    ok = true;
                }
            }
            if (!ok)
                Console.WriteLine();
        }
        public static string free_ticket(int pass_age) // ұшаққа тегін мінетіндер
        {
            if (pass_age < 6)
                return "child Free ticket";
            else
                return "non free ticket";
        }
        public static void free_ticket(int Ticket_num, List<Ticket> tickets, List<Passenger> passengers) // ұшаққа тегін мінетіндер
        {
            Console.WriteLine(Ticket_num + "  Tegin bilet alatyn zholayshi ma:");
            bool ok = false;
            foreach (var t in tickets)
            {
                if (t.Ticket_num == Ticket_num)
                {
                    foreach (var p in passengers)
                    {
                        if (t.pass_id == p.pass_id)
                        {
                            Console.WriteLine($"Passenger: {p.pass_name}. ticket type: {free_ticket(p.pass_age)}");
                            ok = true;

                        }
                    }
                }
            }
            if (!ok)
                Console.WriteLine();

        }

        public void Shygaru()
        {
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - -");
            Console.WriteLine($"Passenger id:{pass_id}\nFIO:{pass_name} {pass_sname}\n_Zhasy:{pass_age}");
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - -");
        }
        public void business_bilet(List<Aircraft> aircraft, List<Ticket> ticket, List<Passenger> passenger)
        {
            foreach (Ticket t in ticket)
            {
                if (t.Ticket_sal > 40000)
                {
                    foreach (Passenger p in passenger)
                    {
                        if (p.pass_id == t.pass_id)
                        {
                            Console.WriteLine($"Name: {p.pass_name} \nbilet bagasy {t.Ticket_sal} \nbilet class {Ticket.ticket_class(t.Ticket_sal)}");
                            Console.WriteLine();

                        }
                    }
                }
            }
        }
        public void vozvrat_bileta(string pass_name, List<Ticket> tickets, List<Passenger> passengers) // билет қайтару кезінде штраф
        {
            foreach (Passenger p in passengers)
            {
                if (p.pass_name == pass_name)
                {
                    foreach (Ticket t in tickets)
                    {
                        if (t.pass_id == p.pass_id)
                        {
                            int percent = 80; // 80%
                            t.Ticket_sal = t.Ticket_sal * (100 - percent) / 100;
                            Console.WriteLine($"Name: {p.pass_name} {p.pass_sname} \nvozvrat bileta so shtrafom: {t.Ticket_sal}");
                            OnSomeEvent(new PriceChangedEventArgs(pass_name));
                        }
                    }
                }
            }
        }
        public void DisplayMessage(Object sender, PriceChangedEventArgs ps) //оқиға өндеуші
        {
            Console.WriteLine($"{this.pass_name}:окига орындалды. {ps.name} Shtrafpen bilet qaitaryldy");
        }
    }
    class Ticket
    {
        public int Ticket_id;
        public int Ticket_sal;
        public int pass_id;
        public string place;
        public int Ticket_num;
        public int Ticket_qnum;
        public List<Aircraft> aircraft;
        public List<Passenger> passenger;
        public int Sany //билеттің саны орынның санынан асып кетпеу үшін
        {
            get { return Ticket_num; }
            set
            {
                Ticket_qnum = 60;
                if (value > Ticket_qnum)
                    Ticket_num = 0;
                else Ticket_num = value;
            }
        }
        public Ticket(int kod, int Ticket_sal, int pass_id, string place, int Ticket_num)
        {
            passenger = new List<Passenger>();
            aircraft = new List<Aircraft>();
            this.Ticket_id = kod;
            this.Ticket_sal = Ticket_sal;
            this.pass_id = pass_id;
            this.place = place;
            this.Ticket_num = Ticket_num;

        }
        public void Shygaru()
        {
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - -");
            Console.WriteLine($"Ticket id:{Ticket_id}\nBagasy:{Ticket_sal}\nOrny:{Ticket_num}{place}");
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - -");
        }
        public static void OrynTabu(int Ticket_id, List<Ticket> tickets) // билет орны туралы малимет
        {
            bool ok = false;
            foreach (Ticket t in tickets)
            {
                if (t.Ticket_id == Ticket_id)
                {
                    t.Shygaru();
                    ok = true;
                }
            }
            if (!ok)
                Console.WriteLine();
        }
        public static void Ticket_class(int Ticket_num, List<Ticket> tickets, List<Passenger> passengers) //билет классын аныктау
        {
            Console.WriteLine(Ticket_num + "  Bilet classy:");
            bool ok = false;
            foreach (var t in tickets)
            {
                if (t.Ticket_num == Ticket_num)
                {
                    foreach (var p in passengers)
                    {
                        if (t.pass_id == p.pass_id)
                        {
                            Console.WriteLine($"Name: {p.pass_name} ticket class: {ticket_class(t.Ticket_sal)}");
                            ok = true;
                        }
                    }
                }
            }
            if (!ok)
                Console.WriteLine();
        }
        public static void showAllTickeys(List<Ticket> ticket)
        {
            foreach (Ticket t in ticket)
            {
                Console.WriteLine("id: " + t.Ticket_id + ", price: " + t.Ticket_sal + ", place: " + t.place + ", ticket num: " + t.Ticket_num);
            }

        }
        public static string ticket_class(int Ticket_sal)
        {
            if (Ticket_sal < 40000)
                return "econom class";
            else
                return "business class";
        }
        public static void Kymbat_bilet(List<Aircraft> aircraft, List<Ticket> ticket, List<Passenger> passenger, DateTime Date, string qala)//мына күні ұшатындардан ең қымбат билет
        {
            foreach (Aircraft a in aircraft)
            {
                if (a.qala == qala)
                {
                    foreach (Ticket t in ticket)
                    {
                        if (t.Ticket_sal >= 60000)
                        {
                            foreach (Passenger p in passenger)
                            {
                                if (p.pass_id == t.pass_id)
                                {
                                    Console.WriteLine($"Name: {p.pass_name} \nen kymbat bilet:{t.Ticket_sal}\nticket class: {ticket_class(t.Ticket_sal)}\nushatyn qalasy:{a.qala}, \nkuni:{a.Data}");

                                }
                            }
                        }
                    }
                }
            }
        }
        public static void TicketPurchase(List<Passenger> passenger, List<Ticket> tickets, int passenger_id, int ticket_id)  // билет сатып алу
        {
            foreach (Passenger p in passenger)
            {
                if (passenger_id == p.pass_id)
                {
                    foreach (Ticket t in tickets)
                    {
                        if (ticket_id == t.Ticket_id)
                        {
                            p.ticket.Add(t);
                            string ticketClass = ticket_class(t.Ticket_sal);
                            string freeTicket = Passenger.free_ticket(p.pass_age);
                            Console.WriteLine(p.pass_id + " " + p.pass_name + " " + p.pass_sname + " " + p.pass_age + " " + freeTicket);
                            for (int i = 0; i < p.ticket.Count; i++)
                            {
                                Console.WriteLine(p.ticket[i].Ticket_id + " " + p.ticket[i].Ticket_sal + " " + p.ticket[i].place + " " + p.ticket[i].Ticket_num + " " + ticketClass);
                            }
                        }
                    }
                }
            }
        }
    }




    class Aircraft
    {
        public int pass_id;
        public int ticket_id;
        public string qala;
        public DateTime Data;
        public List<Passenger> passenger;
        public List<Ticket> ticket;

        public Aircraft(int ID, int kod, string qala, DateTime Data)
        {
            this.pass_id = ID;
            this.ticket_id = kod;
            this.qala = qala;
            this.Data = Data;
            ticket = new List<Ticket>();
            passenger = new List<Passenger>();

        }

        public void Shygaru()
        {
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - -");
            Console.WriteLine($"Qalasy:{qala}\\nData:{Data}");
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - -");
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Passenger> passenger = new List<Passenger>
            {
                new Passenger(1, "Almaz", "Kelmenbetov", 9),
                new Passenger(2, "Dias", "Kalniyazov", 54),
                new Passenger(3, "Nurgali", "Alpysbaev", 19),
                new Passenger(4, "Asem", "Marshalqyzy", 31),
                new Passenger(5, "Zhaniya", "Rasulova", 56),
                new Passenger(6, "Nurali", "Alip", 45),
                new Passenger(7, "Zhaina", "Kalbaeva", 13),
                new Passenger(8, "Bereket", "Turniyaz", 2),
                new Passenger(9, "Bauirzhan", "Abdykilov", 20),
                new Passenger(10, "Aizada", "Satubaldiyeva", 35)
            };

            List<Ticket> ticket = new List<Ticket>
            {
                new Ticket(20, 22000, 2, "A", 42),
                new Ticket(21, 22000, 4, "B", 32),
                new Ticket(22, 30000, 5, "C", 14),
                new Ticket(23, 40000, 6, "D", 54),
                new Ticket(24, 42000, 7, "E", 42),
                new Ticket(25, 10000, 1, "F", 48),
                new Ticket(26, 29000, 3, "A", 54),
                new Ticket(27, 30000, 8, "B", 10),
                new Ticket(28, 60000, 10, "C", 1),
                new Ticket(29, 52000, 9, "D", 27)
            };
            List<Aircraft> aircraft = new List<Aircraft>
            { 
                new Aircraft(2, 24, "Aqtau", new DateTime(2021, 12, 26)),
                new Aircraft(1, 20, "Almaty", new DateTime(2021, 12, 26)),
                new Aircraft(3, 21, "Dubal", new DateTime(2021, 12, 26)),
                new Aircraft(8, 22, "Atyrau", new DateTime(2021, 12, 26)),
                new Aircraft(7, 23, "Turkistan", new DateTime(2021, 12, 26)),

                new Aircraft(6, 26, "Shymkent", new DateTime(2021, 12, 27)),
                new Aircraft(5, 25, "Taraz", new DateTime(2021, 12, 27)),
                new Aircraft(4, 27, "Nursultan", new DateTime(2021, 12, 27)),
                new Aircraft(10, 28, "Istanbul", new DateTime(2021, 12, 27)),
                new Aircraft(9, 29, "Qostanay", new DateTime(2021, 12, 27)),

                new Aircraft(1, 24, "Bishkek", new DateTime(2021, 12, 28)),
                new Aircraft(7, 21, "Pavlodar", new DateTime(2021, 12, 28)),
                new Aircraft(4, 20, "Moskva", new DateTime(2021, 12, 28)),
                new Aircraft(8, 23, "Oral", new DateTime(2021, 12, 28)),
                new Aircraft(6, 22, "Uskemen", new DateTime(2021, 12, 28)),

                new Aircraft(5, 25, "Qyzylorda", new DateTime(2021, 12, 29)),
                new Aircraft(3, 26, "Semey", new DateTime(2021, 12, 29)),
                new Aircraft(2, 27, "Aqtobe", new DateTime(2021, 12, 29)),
                new Aircraft(9, 28, "Zhezkazgan", new DateTime(2021, 12, 29)),
                new Aircraft(10, 29, "Petropavl", new DateTime(2021, 12, 29)),
            };
            Console.WriteLine("Choose service:");
            Console.WriteLine("1. Passenger info:");
            Console.WriteLine("2. Return tickey(20% penalty):");
            Console.WriteLine("3. Buy ticket:");
            Console.WriteLine("4. En kymbat bilet:");
            Console.WriteLine("5. Tegin bilet iegeri: ");
            Console.WriteLine("6. Business class bilet iegerleri: ");
            int userChoice = Convert.ToInt32(Console.ReadLine());
            if (userChoice == 1)
            {
                Console.WriteLine();
                Console.WriteLine("passenger data:");
                string name = Console.ReadLine();
                Passenger.Passengerdata(name, passenger);
            }
            else if (userChoice == 2)
            {
                for (int i = 0; i < passenger.Count; i++)
                {
                    Passenger.Tirkeu(passenger);
                    Console.WriteLine();
                    Console.WriteLine("Штраф возврата билета (20%):");
                    string name = Console.ReadLine();
                    passenger[i].vozvrat_bileta(name, ticket, passenger);
                    Console.ReadKey();
                }
            }
            else if (userChoice == 3)
            {
                while (true)
                {
                    Console.WriteLine("If you want to finish purchase write 0. Otherwise write 1.");
                    int userChoice1 = Convert.ToInt32(Console.ReadLine());
                    if (userChoice1 != 0)
                    {
                        Console.WriteLine("Введите ваш ID: ");
                        int passId = Convert.ToInt32(Console.ReadLine());
                        Ticket.showAllTickeys(ticket);
                        Console.WriteLine("Выберите билет который хотите купить (напишите его ID): ");
                        int ticketId = Convert.ToInt32(Console.ReadLine());
                        Ticket.TicketPurchase(passenger, ticket, passId, ticketId);

                    }
                    else
                    {
                        break;
                    }
                }
                Console.WriteLine("Thank you for purchase");
            }
            else if (userChoice == 4)
            {
                DateTime date = new DateTime(2021, 12, 26);
                string qala = "Aqtau";
                Ticket.Kymbat_bilet(aircraft, ticket, passenger, date, qala);
            }
            else if (userChoice == 5)
            {
                Console.WriteLine();
                Console.WriteLine("Билет бесплатный для: ");
                int number = Convert.ToInt32(Console.ReadLine());
                Passenger.free_ticket(number, ticket, passenger);
                Console.ReadKey();
            }
            else if (userChoice == 6)
            {
                for (int i = 0; i < ticket.Count; i++)
                {
                    Passenger.Tirkeu(passenger);
                    Console.WriteLine();
                    Console.WriteLine("Бизнес класс билет купил: ");
                    passenger[i].business_bilet(aircraft, ticket, passenger);
                }
            }
            Console.ReadKey();
        }
    }
}
