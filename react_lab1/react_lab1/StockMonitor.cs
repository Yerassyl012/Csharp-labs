using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace react_lab1
{
    class StockMonitor : IDisposable
    {
        object _stockTickLocker = new object();
        private readonly StockTicker _ticker;
        Dictionary<string, StockInfo> _stockInfos = new Dictionary<string, StockInfo>();
        public StockMonitor(StockTicker ticker)
        {
            _ticker = ticker;
            ticker.StockTick += OnStockTick;//Здесь вы будете проверять каждый тик акции, если у вас уже есть его предыдущий тик, чтобы вы могли сравнить новую цену со старой ценой. 
        }//Для работы с биржевыми тиками вы создадите класс StockMonitor, который будет прослушивать изменения акций, подключаясь к событию StockTick с помощью оператора +=


        void OnStockTick(object sender, StockTick stockTick)
        {
            const decimal maxChangeRatio = 0.1m;
            StockInfo stockInfo;
            var quoteSymbol = stockTick.QuoteSymbol;
            var stockInfoExists = _stockInfos.TryGetValue(quoteSymbol, out stockInfo);
            if (stockInfoExists)
            {
                var priceDiff = stockTick.Price - stockInfo.PrevPrice;
                var changeRatio = Math.Abs(priceDiff / stockInfo.PrevPrice); //#A the percentage of change
                if (changeRatio > maxChangeRatio)
                {
                    Console.WriteLine("Stock:{0} has changed with {1} ratio, Old Price:{2} New Price:{3}", quoteSymbol,
                        changeRatio,
                        stockInfo.PrevPrice,
                        stockTick.Price);
                }
                _stockInfos[quoteSymbol].PrevPrice = stockTick.Price;
            }//Если информация об акциях существует, вы можете проверить текущую и предыдущую цены акции,
             //чтобы увидеть, было ли изменение больше порогового значения, определяющего резкое изменение.
            else
            {
                _stockInfos[quoteSymbol] = new StockInfo(quoteSymbol, stockTick.Price);
            }//Если информации об акциях нет в словаре, вам нужно добавить  ее в словарь 

        }
        public void Dispose()
        {
            _ticker.StockTick -= OnStockTick;
            _stockInfos.Clear();
        }

        //Каждый раз, когда OnStockTick вызывается с новым тиком, приложению необходимо проверить, не сохранена ли уже старая цена в словаре.
        //Вы используете метод TryGet Value, который возвращает true, если ключ, который вы ищете, существует в словаре, а затем вы устанавливаете параметр out со значением, хранящимся под этим ключом.

    }
}