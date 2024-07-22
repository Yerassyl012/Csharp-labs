using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rx13.Messages
{
    public class GreetingMessage
    {
        public GreetingMessage(string greeting)
        {
            Greeting = greeting;
        }

        public string Greeting { get; }
    }
}
