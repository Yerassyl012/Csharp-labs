using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace react_lab1
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
}//Ключ хранит информацию о предыдущих тиках, вы определяете новый класс с именем StockInfo 
