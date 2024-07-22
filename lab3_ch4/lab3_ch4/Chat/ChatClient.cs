using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace lab3_ch4.Chat
{
    public class ChatClient //Чат қызметіне қосылу Chat Client класының Connect әдісі арқылы жүзеге асырылады. 
    {
        IList<IChatConnection> _connections = new List<IChatConnection>();
        public IChatConnection Connect(string user, string password)
        {
            Console.WriteLine("Connect");
            var chatConnection = new ChatConnection();
            _connections.Add(chatConnection);
            return chatConnection;//жаңа chatConnection жасау
        }


        public IObservable<string> ObserveMessages(string user, string password)
        {
            var connection = Connect(user, password);
            return connection.ToObservable();//connection ды бақылаушыға айналдыру әдісі
        }

        public IObservable<string> ObserveMessagesDeferred(string user, string password)//серверге қосылу
        {
            return Observable.Defer(() =>//Defer операторы нақты бақыланатын айналасында прокси ретінде әрекет ететін Observable жасайды.
            {
                var connection = Connect(user, password);
                return connection.ToObservable();// бұл метод шақырылған сайын чат қызметіне қосылым жасалынады, содан соң бақыланатынға тіркелінеді
            });
        }
        public void NotifyRecieved(string msg)
        {
            foreach (var chatConnection in _connections)
            {
                chatConnection.NotifyRecieved(msg);
            }
        }
        public void NotifyClosed()
        {
            foreach (var chatConnection in _connections)
            {
                chatConnection.NotifyClosed();
            }
        }
        public void NotifyError()
        {
            foreach (var chatConnection in _connections)
            {
                chatConnection.NotifyError();
            }
        }
    }
}
