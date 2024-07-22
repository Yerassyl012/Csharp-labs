using System;
using System.Linq;
using System.Collections.Generic;
namespace SRS_18
{
    class Juvelirnaia_firma
    {
        public string nazvanie_firmy;
        public string adress;
        public string familia_direktora;
        public long nomeri;
        public Juvelirnaia_firma(string a, string b, string c, long d)
        {
            nazvanie_firmy = a;
            adress = b;
            familia_direktora = c;
            nomeri = d;
        }
        public void Display()
        {
            Console.WriteLine($"{nazvanie_firmy} fyrmasy.\nAdress: {adress}\nDirektor: {familia_direktora}; Nomeri: {nomeri}\n");
        }
    }
    class Izdelie
    {
        public Juvelirnaia_firma firmasy;
        public string nazvanie_izdelia;
        public int sort;
        public int kolichestvo_sklad;
        public double nachalnaya_tcena;
        public Izdelie(Juvelirnaia_firma a, string b, int c, int d, double e)
        {
            firmasy = a;
            nazvanie_izdelia = b;
            sort = c;
            kolichestvo_sklad = d;
            nachalnaya_tcena = e;
        }
        public void Display()
        {
            Console.WriteLine($"Atauy: {nazvanie_izdelia}\t{nachalnaya_tcena} tg\nSorty: {sort}\tSkladta: {kolichestvo_sklad} dana\nSatatun dukeni: {firmasy.nazvanie_firmy}\n");
        }
    }
    class Jarmarka
    {
        public Juvelirnaia_firma firmasy;
        public Izdelie izdelie;
        public int kolichestvo_prodannyh;
        public double prodazhnaya_tcena;
        private string sort_pokupatelya;
        public string Sort_pokupatelya
        {
            get { return sort_pokupatelya; }
            set
            {
                if (value == "uchrezhdenie" || value == "optovik")
                    sort_pokupatelya = value;
                else sort_pokupatelya = "chastnoe litco";
            }
        }
        public Jarmarka(Izdelie a, int b, double c, string d)
        {
            firmasy = a.firmasy;
            izdelie = a;
            kolichestvo_prodannyh = b;
            prodazhnaya_tcena = c;
            Sort_pokupatelya = d;
            a.kolichestvo_sklad -= kolichestvo_prodannyh;
        }
        public void Display()
        {
            Console.WriteLine($"{firmasy.nazvanie_firmy} fyrmasy\t{izdelie.nazvanie_izdelia} satyldy\n{prodazhnaya_tcena} bagaga   {kolichestvo_prodannyh} dana satyldy\npokupatel: {Sort_pokupatelya}\n");
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            List<Juvelirnaia_firma> firmalar = new List<Juvelirnaia_firma>
            {
                new Juvelirnaia_firma("Altyn", "Seifullina 205", "Qozhahmeov L.", 87002458963),
                new Juvelirnaia_firma("Alpako", "Rysqulova 145", "Aspandyarov E.", 87754256985),
                new Juvelirnaia_firma("Apallon", "Zhansugurova 20", "Kamalatdinov B.", 87078523694)
            };

            List<Izdelie> izdelielar = new List<Izdelie>
            {
                new Izdelie(firmalar[0],"Bilezik",1,20,120000),         new Izdelie(firmalar[0],"Saqina",2,12,20000),
                new Izdelie(firmalar[0],"Syrga",1,15,100000),           new Izdelie(firmalar[0],"Kulon",2,10,40000),
                new Izdelie(firmalar[1],"Bilezik",1,18,115000),         new Izdelie(firmalar[1],"Saqina",2,15,25000),
                new Izdelie(firmalar[1],"Syrga",1,17,95000),            new Izdelie(firmalar[1],"Kulon",2,17,30000),
                new Izdelie(firmalar[2],"Bilezik",1,25,110000),         new Izdelie(firmalar[2],"Saqina",2,13,25000),
                new Izdelie(firmalar[2],"Syrga",1,11,90000),            new Izdelie(firmalar[2],"Kulon",2,17,50000),
            };

            List<Jarmarka> zharmenke = new List<Jarmarka>
            {
                new Jarmarka(izdelielar[0],10,120000,"optovik"),        new Jarmarka(izdelielar[1],12,20000,"uchrezhdenie"),
                new Jarmarka(izdelielar[2],1,100000," "),               new Jarmarka(izdelielar[2],10,100000,"optovik"),
                new Jarmarka(izdelielar[4],6,117000,"uchrezhdenie"),    new Jarmarka(izdelielar[5],8,250000,"optovik"),
                new Jarmarka(izdelielar[6],10,95000,"optovik"),         new Jarmarka(izdelielar[7],7,35000,"uchrezhdenie"),
                new Jarmarka(izdelielar[8],2,115000," "),               new Jarmarka(izdelielar[9],3,30000," "),
                new Jarmarka(izdelielar[10],11,90000,"uchrezhdenie"),   new Jarmarka(izdelielar[11],12,50000,"optovik"),
            };

            var firmy_2 = zharmenke.AsParallel().GroupBy(p => p.firmasy.nazvanie_firmy).Select(g => new { aty = g.Key, mas = g.Join(izdelielar, a => a.izdelie, f => f, (a, f) => new { _do = a, _posle = f }) });
            foreach (var a in firmy_2)
            {
                Console.WriteLine($"{a.aty}: ");
                foreach (var b in a.mas)
                {
                    b._do.Display();
                    b._posle.Display();
                    Console.WriteLine("------------");
                }
                Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            }
            Console.WriteLine("2----------------------------------------------------------------------");

            zharmenke.AsParallel().Where(a => a.prodazhnaya_tcena > a.izdelie.nachalnaya_tcena).Select(a => a.firmasy).Distinct().ForAll(a => a.Display());
            Console.WriteLine("3----------------------------------------------------------------------");

            zharmenke.AsParallel().Where(a => a.prodazhnaya_tcena == zharmenke.Max(a => a.prodazhnaya_tcena)).Select(a => a).ForAll(a => { a.firmasy.Display(); a.izdelie.Display(); });
            Console.WriteLine("4----------------------------------------------------------------------");

            var sony = zharmenke.GroupBy(a => a.firmasy.nazvanie_firmy).Join(firmalar, g => g.Key, b => b.nazvanie_firmy, (g, b) => new { nazvanie = b, kol = g.AsParallel().Select(a => a.kolichestvo_prodannyh).Sum(), summa = g.AsParallel().Select(a => a.kolichestvo_prodannyh * (a.prodazhnaya_tcena - a.izdelie.nachalnaya_tcena)).Sum() }).OrderByDescending(t => t.summa).Take(1);
            foreach (var a in sony)
            {
                a.nazvanie.Display();
                Console.WriteLine($"{a.kol} dana buim satyp {a.summa} paida tapty");
            }
        }
    }
}