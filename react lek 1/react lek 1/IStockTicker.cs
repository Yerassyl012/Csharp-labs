using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace react_lek_1
{
    public interface IStockTicker//интерфейс
    {
        event EventHandler<StockTick> StockTick;
    }
}
