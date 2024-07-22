using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace react_lek_1
{
    class StockInfo
    {
        public StockInfo(string symbol, decimal price)
        {
            Symbol = symbol;
            PrevPrice = price;
        }
        public string Symbol { get; set; }
        public decimal PrevPrice { get; set; }
    }
}
//Кілт алдыңғы белгілер туралы ақпаратты сақтайды, сіз StockInfo деп аталатын жаңа сыныпты анықтайсыз