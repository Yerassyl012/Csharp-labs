using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_ch4.Chat
{
    public interface IChatConnection//Сөйлесу қызметіне қосылу кезінде сіз келесі интерфейсі бар қосылым нысанын аласыз
    {
        event Action<string> Received;
        event Action Closed;
        event Action<Exception> Error;

        void Disconnect();

        void NotifyClosed();
        void NotifyError();
        void NotifyRecieved(string msg);
    }
}
