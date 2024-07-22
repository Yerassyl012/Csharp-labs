using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rx8.Model
{
    internal class ChatMessage
    {
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string Sender { get; set; }
        public string Room { get; set; }
    }
}
