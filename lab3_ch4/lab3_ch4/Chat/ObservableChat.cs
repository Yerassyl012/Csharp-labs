using System;
using System.Reactive;
using System.Reactive.Disposables;

namespace lab3_ch4.Chat
{
    public class ObservableConnection : ObservableBase<string> //ObservableBase дан туынды болып шыққан ObservableConnection абстрактты SubscribeCore әдісін орындайды
    {
        private readonly IChatConnection _chatConnection;

        public ObservableConnection(IChatConnection chatConnection)
        {
            _chatConnection = chatConnection;
        }

        protected override IDisposable SubscribeCore(IObserver<string> observer)//SubscribeCore, ObservableBase дан Subscribe әдісін шақырады
        {
            Action<string> received = message =>
            {
                observer.OnNext(message);
            };

            Action closed = () =>
            {
                observer.OnCompleted();
            };

            Action<Exception> error = ex =>
            {
                observer.OnError(ex);
            };

            _chatConnection.Received += received;
            _chatConnection.Closed += closed;
            _chatConnection.Error += error;

            return Disposable.Create(() =>
            {
                _chatConnection.Received -= received;
                _chatConnection.Closed -= closed;
                _chatConnection.Error -= error;
                _chatConnection.Disconnect();//егер хабарламалар тауысылса Disposable мен ажыратылады
            });
        }
    }
}
