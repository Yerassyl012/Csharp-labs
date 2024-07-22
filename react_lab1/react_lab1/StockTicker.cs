using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace react_lab1
{
    public class StockTicker : IStockTicker
    {
        public event EventHandler<StockTick> StockTick = delegate { };

        public void Notify(StockTick tick)
        {
            StockTick(this, tick);
        }
    }

}
//Класс предоставляет только событие о StockTick, которое возникает каждый раз, когда доступна новая информация об акции.