using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Disposables;
using System.Reactive;

namespace react_lek3
{
    public class NumbersObservable : IObservable<int>
    {
        private readonly int _amount;
        public NumbersObservable(int amount)
        {
            _amount = amount;
        }
        public IDisposable Subscribe(IObserver<int> observer)
        {
            for (int i = 0; i < _amount; i++)
            {
                observer.OnNext(i);
            }
            observer.OnCompleted();

            observer.OnNext(_amount);
            return Disposable.Empty;
        }

    }

    public class ConsoleObserver<T> : IObserver<T>
    {
        private readonly string _name;
        public ConsoleObserver(string name = "")
        {
            _name = name;
        }
        public void OnNext(T value)
        {
            Console.WriteLine("{0} - OnNext({1})", _name, value);
        }
        public void OnError(Exception error)
        {
            Console.WriteLine("{0} - OnError:", _name);
            Console.WriteLine("\t {0}", error);
        }
        public void OnCompleted()
        {
            Console.WriteLine("{0} - OnCompleted()", _name);
        }
    }
    public static class Extensions
    {
        public static IDisposable SubscribeConsole<T>(
        this IObservable<T> observable,
        string name = "")
        {
            return observable.Subscribe(new ConsoleObserver<T>(name));
        }
    }

    public interface IChatConnection
    {
        event Action<string> Received;
        event Action Closed;
        event Action<Exception> Error;
        void Disconnect();
    }
    public class ChatClient
    {
        public IChatConnection Connect(string user, string password)
        {
            var connection = Connect(user, password);
            return connection;

        }
    }



    public class ObservableConnection : ObservableBase<string>
    {
        private readonly IChatConnection _chatConnection;
        public ObservableConnection(IChatConnection chatConnection)
        {
            _chatConnection = chatConnection;
        }

        protected override IDisposable SubscribeCore(IObserver<string> observer)
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
                _chatConnection.Disconnect();
            });
        }

    }
    public static class ChatExtensions
    {
        public static IObservable<string> ToObservable(
        this IChatConnection connection)
        {
            return new ObservableConnection(connection);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var chatClient = new ChatClient();
            var subscription =
            chatClient.Connect("guest", "guest")
            .ToObservable()
            .SubscribeConsole();
        }
    }

}