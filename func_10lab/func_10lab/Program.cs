using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace func_10lab
{

    delegate void ShooterEventHandler(Shooter e);
    abstract class Shooter
    {
        public string Aty;
        public int zhasy;
        public int experience;
        public event ShooterEventHandler SomeEvent;
        public Shooter(string Aty, int zhasy, int experience)
        {
            this.Aty = Aty;
            this.zhasy = zhasy;
            this.experience = experience;
        }
        public static void Tirkeu(Shooter[] sh)
        {
            for (int i = 0; i < sh.Length; i++)
            {
                for (int j = 0; j < sh.Length; j++)
                {
                    if (i != j)
                        sh[i].SomeEvent += sh[j].DisplayMessage;


                }
            }
        }


        public void OnSomeEvent(Shooter e)
        {
            if (SomeEvent != null)
                SomeEvent(e);
        }

        public void Display()
        {
            Console.WriteLine($"sadak atyldy");
        }
        public int get_age()
        {
            return zhasy;
        }
        public int get_experience()
        {
            return experience;
        }
        public void successfulShot()
        {
            bool ok = successfulShotProbability(probability());
            if (ok)
            {

                Console.WriteLine();
                OnSomeEvent(this);

                Display();
                Console.WriteLine($"{this.Aty} Shooter tigizdi");

            }
            else
            {
                Console.WriteLine();

                Display();
                Console.WriteLine($"{this.Aty} Shooter tigizbedi");
            }
        }
        public abstract double probability();
        public bool successfulShotProbability(double probability)
        {
            Random randomNumber = new Random();
            double temp = randomNumber.NextDouble();
            if (probability == temp)
                return true;
            else
            { return false; }
        }
        public void DisplayMessage(Shooter e)
        {

            Console.WriteLine($"{this.Aty}:окига орындалды. { e.Aty} Shooter nysanaga tigizdi!");
        }
    }
    class Beginner : Shooter
    {
        public Beginner(string Aty, int zhasy, int experience) : base(Aty, zhasy, experience) { }
        public override double probability()
        {
            return 0.01 * get_experience();
        }
    }
    class Average : Shooter
    {
        public Average(string Aty, int zhasy, int experience) : base(Aty, zhasy, experience) { }
        public override double probability()
        {
            return 0.05 * get_experience();
        }
    }

    class Profi : Shooter
    {
        public Profi(string Aty, int zhasy, int experience) : base(Aty, zhasy, experience) { }
        public override double probability()
        {
            return 0.9 - 0.01 * get_age();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Shooter[] sh = { new Average("Yeldos", 50, 18),
                new Profi("Temirlan", 70, 40),
                new Beginner("Rizabek", 26, 5),
                new Average("Olzhas", 51, 17),
                new Average("Gani", 39, 11),
                new Profi("Nurken", 80, 38),
                new Beginner("Dias", 20, 6)};
            Shooter.Tirkeu(sh);
            for (int i = 0; i < sh.Length; i++)
            {
                sh[i].successfulShot();

            }
            Console.ReadKey();
        }
    }

}
