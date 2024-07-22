using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rx8.Model
{
    class ChatRoom
    {
        public string Id { get; set; }
        public IObservable<ChatMessage> Messages { get; set; }

        public override string ToString()
        {
            return "ChatRoom: " + Id;
        }
    }
}
