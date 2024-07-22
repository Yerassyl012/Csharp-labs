using System;
using System.Linq;
using System.Reactive.Linq;

namespace srs1_rx_steam
{
    internal class ChatServer
    {
        private static int createdServers = 0;
        public ChatServer()
        {
            ServerId = createdServers++;
        }

        public int ServerId { get; set; }

        public static ChatServer Current
        {
            get { return new ChatServer(); }
        }

        public IObservable<string> ObserveMessages()
        {
            return Observable.Never<string>().StartWith("NewItem1", "NewItem2", "NewItem3").Select(m => "Server " + ServerId + " - " + m);
        }
    }
}