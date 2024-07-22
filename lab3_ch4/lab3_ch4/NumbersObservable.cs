using System;
using System.Reactive.Disposables;


namespace lab3_ch4
{
    public class NumbersObservable : IObservable<int>
    {//NumbersObservable класы IObservable интерфейсін жүзеге асырады, ол кез келген бүтін бақылаушыға оған жазылуға мүмкіндік береді.
        private readonly int _amount;

        public NumbersObservable(int amount)
        {
            _amount = amount;
        }
        //NumbersObservable бақылаушы оған жазылғаннан кейін бірден және синхронды түрде бүтін мәндерді жібереді.
        public IDisposable Subscribe(IObserver<int> observer)
        {
            for (int i = 0; i < _amount; i++)
            {
                observer.OnNext(i);//бақылаушыға жаңа мән беру
            }
            observer.OnCompleted();//бақылаушыға жібергілген хабарламаның біткенін хабарлайды
            return Disposable.Empty;// жұмыс аяқталғаннан кейін, бақылаушы жазылудан Dispose әдісін шақыру арқылы бас тарта алады,
                                    // бұл мысалда жұмыс біткенде Disposable.Empty арқылы бір рет қолданбалы объектті тазалаймыз
        }
    }
}
