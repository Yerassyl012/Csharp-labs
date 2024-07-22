using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace react_lek_1
{
    public class RxStockMonitor : IDisposable
    {
        private IDisposable _subscription;

        public RxStockMonitor(IStockTicker ticker)
        {
            const decimal maxChangeRatio = 0.1m;

            //StockTick оқиғасынан бақыланатын оқиғаны жасау кезінде әрбір хабарландыру тек оқиғалар таңбаларын қамтиды және синхрондалады
            IObservable<StockTick> ticks =
                    Observable.FromEventPattern<EventHandler<StockTick>, StockTick>(
                        h => ticker.StockTick += h,
                        h => ticker.StockTick -= h)// оқиғадан observable ға айналдыру 
                        .Select(tickEvent => tickEvent.EventArgs)
                        .Synchronize(); 

            var drasticChanges =
                from tick in ticks
                group tick by tick.QuoteSymbol //қарапайым группировка жасау әдісі
                into company 
                from tickPair in company.Buffer(2, 1) //Buffer әдісі қатарынан екі құсбелгіден тұратын массив шығарады
                //Бұл екі белгі арасындағы айырмашылықты есептеуге мүмкіндік береді және оның рұқсат етілген шектерде екенін білуге ​​​​мүмкіндік береді.
                let changeRatio = Math.Abs((tickPair[1].Price - tickPair[0].Price) / tickPair[0].Price)//Let кілт сөзін пайдаланып, Rx есептеуді бақыланатынға тасымалданатын айнымалы мәнде сақтауға мүмкіндік береді
                where changeRatio > maxChangeRatio 
                select new DrasticChange()
                {
                    Symbol = company.Key,
                    ChangeRatio = changeRatio,
                    OldPrice = tickPair[0].Price,
                    NewPrice = tickPair[1].Price
                };//draasticChanges айнымалысы maxChangeRatio мәнінен жоғары акция бағасының өзгеруін білдіретін белгілер үшін ғана хабарландыру.

            DrasticChanges = drasticChanges;

            _subscription =
                drasticChanges.Subscribe(change => //subscribe  бақылаушыларға хабарландыруларға жазылуға мүмкіндік береді
                {
                    Console.WriteLine("Stock:{0} has changed with {1} ratio, Old Price:{2} New Price:{3}", change.Symbol,
                        change.ChangeRatio,
                        change.OldPrice,
                        change.NewPrice);
                },//өзгертілген мәндерді экранға шығару
                    ex => { /* code that handles erros */}, //#C
                    () => {/* code that handles the observable completenss */}); //#C
        }

        public IObservable<DrasticChange> DrasticChanges { get; }

        public void Dispose()
        {
            _subscription.Dispose();
        }//хабарландыруларды үнемі алмау үшін осы әдісті пайдаланып жазылымнан бас тартуға болады
    }

    public class DrasticChange
    {
        public decimal NewPrice { get; set; }
        public string Symbol { get; set; }
        public decimal ChangeRatio { get; set; }
        public decimal OldPrice { get; set; }
    }
}
