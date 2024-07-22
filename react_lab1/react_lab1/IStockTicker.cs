using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace react_lab1
{
    public interface IStockTicker
    {
        event EventHandler<StockTick> StockTick;
    }   
}
