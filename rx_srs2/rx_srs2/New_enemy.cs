using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rx_srs2
{
    internal class New_enemy
    {
        public int enemy_id;
        public string enemy_name;
        public string marka;
        public int enemy_level;
        public string country;
        public int flight_speed;
        public int combat_capacity;
        public New_enemy(int enemy_id, string enemy_name, string marka, int enemy_level, string country, int flight_speed, int combat_capacity)
        {
            this.enemy_id = enemy_id;
            this.enemy_name = enemy_name;
            this.marka = marka;
            this.enemy_level = enemy_level;
            this.country = country;
            this.flight_speed = flight_speed;
            this.combat_capacity = combat_capacity;
        }
        public static void showAllinfos(List<New_enemy> enemy)
        {
            foreach (New_enemy t in enemy)
            {
                Console.WriteLine("id: " + t.enemy_id + ", name: " + t.enemy_name + ", marka: " + t.marka + ", enemy level: " + t.enemy_level + ", Country:" + t.country+ ", Flight speed:" + t.flight_speed + ", боеспособность:" + t.combat_capacity);
            }

        }
    }
}
