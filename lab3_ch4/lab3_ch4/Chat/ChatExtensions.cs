using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lab3_ch4.Chat
{
    public static class ChatExtensions
    {
        public static IObservable<string> ToObservable(this IChatConnection connection)
        {//Кеңейтім әдісімен ObservableConnection жасау
            return new ObservableConnection(connection);
        }
    }
}
