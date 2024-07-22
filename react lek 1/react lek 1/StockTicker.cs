using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace react_lek_1
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

//Класс тек жаңа қор ақпараты қолжетімді болған сайын көтерілетін StockTick оқиғасын қамтамасыз етеді.