using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rx_srs2
{
    delegate void ShooterEventHandler(Shooter e);
    abstract class Shooter
    {
        public int Id;
        public string Aty;
        public int experience;
        public List<New_enemy> enemy;
        public event ShooterEventHandler SomeEvent;
        public Shooter(int Id, string Aty, int experience)
        {
            enemy = new List<New_enemy>();
            this.Id = Id;
            this.Aty = Aty;
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
            Console.WriteLine($"snaryad atyldy");
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
                Console.WriteLine($"{this.Aty} tank tigizdi");

            }
            else
            {
                Console.WriteLine();

                Display();
                Console.WriteLine($"{this.Aty} tank tigizbedi");
            }
        }
        public abstract double probability();
        public bool successfulShotProbability(double probability)
        {
            Random randomNumber = new Random();
            double temp = randomNumber.NextDouble();
            if (probability <= temp)
                return true;
            else
            { return false; }
        }
        public void DisplayMessage(Shooter e)
        {

            Console.WriteLine($"{this.Aty}:окига орындалды. { e.Aty} Tank nysanaga tigizdi!");
        }
    }
    class Strela10 : Shooter
    {
        public Strela10(int Id, string Aty, string name, int experience) : base(Id, Aty, experience) { }
        public override double probability()
        {
            return 0.5 * get_experience();
        }
    }
    class Shilka : Shooter
    {
        public Shilka(int Id, string Aty,string name, int experience) : base(Id, Aty, experience) { }
        public override double probability()
        {
            return 0.8 * get_experience();
        }
    }
}
