using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace react_lek_1
{
    class StockMonitor : IDisposable
    {
        object _stockTickLocker = new object();
        private readonly StockTicker _ticker;
        Dictionary<string, StockInfo> _stockInfos = new Dictionary<string, StockInfo>();
        public StockMonitor(StockTicker ticker)
        //Сіз қолданып жатқан сөздік ағынға қауіпсіз емес. Сіз пайдаланып жатқан сөздік бір уақытта бірнеше оқырманды қолдайды, бірақ сөздік өзгертіліп жатқан кезде оқылып жатса, ерекше жағдай жасалады.
        {
            _ticker = ticker;
            ticker.StockTick += OnStockTick;//Мұнда сіз акцияның әрбір белгісін тексересіз, егер сізде оның алдыңғы белгісі бар болса, жаңа бағаны ескі бағамен салыстыра аласыз.
        }//Акция белгілерімен жұмыс істеу үшін += операторы арқылы StockTick оқиғасына қосылу арқылы қор өзгерістерін тыңдайтын StockMonitor сыныбын жасайсыз.



        void OnStockTick(object sender, StockTick stockTick)
        {
            const decimal maxChangeRatio = 0.1m;
            StockInfo stockInfo;
            var quoteSymbol = stockTick.QuoteSymbol;
            lock (_stockTickLocker)//OnStockTick оқиға өңдегішінде маңызды бөлім бар және оны қорғау жолы - құлыпты пайдалану.
            {
                var stockInfoExists = _stockInfos.TryGetValue(quoteSymbol, out stockInfo);
                if (stockInfoExists)
                {
                    var priceDiff = stockTick.Price - stockInfo.PrevPrice;
                    var changeRatio = Math.Abs(priceDiff / stockInfo.PrevPrice); 
                    if (changeRatio > maxChangeRatio)
                    {
                        Console.WriteLine("Stock:{0} has changed with {1} ratio, Old Price:{2} New Price:{3}", quoteSymbol,
                            changeRatio,
                            stockInfo.PrevPrice,
                            stockTick.Price);
                    }
                    _stockInfos[quoteSymbol].PrevPrice = stockTick.Price;
                }//Акция туралы ақпарат бар болса, өзгеріс күрт өзгерісті анықтайтын шекті мәннен жоғары болғанын көру үшін акцияның ағымдағы және алдыңғы бағаларын тексеруге болады.
                else
                {
                    _stockInfos[quoteSymbol] = new StockInfo(quoteSymbol, stockTick.Price);
                }//Егер қор туралы ақпарат сөздікте болмаса, оны сөздікке қосу керек
            }
        }

        public void Dispose()
        {
            _ticker.StockTick -= OnStockTick;
            _stockInfos.Clear();
        }
    }
}//OnStockTick жаңа белгімен шақырылған сайын, қолданба ескі бағаның сөздікте сақталғанын тексеруі керек.
 //Сіз іздеген кілт сөздікте бар болса, ақиқат мәнін қайтаратын TryGet Value әдісін пайдаланасыз, содан кейін осы кілт астында сақталған мәнмен out параметрін орнатасыз.

