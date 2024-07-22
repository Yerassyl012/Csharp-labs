using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rx_srs2
{
    
    class BMP
    {
        public Tank Tank { get; set; }
        public string tank_name;
        public int year;
        public int kolibr;
        public int dalnost;
        public int ekipazh;
        public BMP(string tank_name, int year, int kolibr, int dalnost, int ekipazh)
        {
            this.tank_name = tank_name;
            this.year = year;
            this.kolibr = kolibr;
            this.dalnost = dalnost;
            this.ekipazh = ekipazh;
        }
        public void info()
        {
            Console.WriteLine($"tank:{tank_name}, {year} производства с колибром:{kolibr}, экипажом:{ekipazh} и дальностью:{dalnost} стреляет в цель");
        }

        public static void TicketPurchase(List<Shooter> sh, List<New_enemy> enemy, int shooter_id, int enemy_id)  // билет сатып алу
        {
            foreach (Shooter p in sh)
            {
                if (shooter_id == p.Id)
                {
                    foreach (New_enemy t in enemy)
                    {
                        if (enemy_id == t.enemy_id)
                        {
                            p.enemy.Add(t);
                            int Flight_efficiency = (t.flight_speed + t.combat_capacity)/100;
                            for (int i = 0; i < p.enemy.Count; i++)
                            {
                                Console.WriteLine(p.enemy[i].enemy_id + " " + p.enemy[i].enemy_name + " " + p.enemy[i].marka + " " + p.enemy[i].enemy_level + " " + p.enemy[i].country + " " + Flight_efficiency);
                            }
                        }
                    }
                }
            }
        }
    }
}
