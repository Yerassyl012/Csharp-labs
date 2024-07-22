using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rx_srs1
{
    public class EmailMessage
    {
        public EmailMessage(string from, string to, string content)
        {
            From = from;
            To = to;
            Content = content;
        }
        public string From { get; }
        public string To { get; }
        public string Content { get; }
    }
}
