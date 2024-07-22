using System;
using System.Linq;
using System.Reactive.Linq;

namespace rx6
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
            //"Never" is used so that the resulted observable will not complete, so  the observers wont be detached
            return Observable.Never<string>().StartWith("Worst", "Normal", "Good").Select(m => "Server" + ServerId + " - " + m);
        }
    }
}