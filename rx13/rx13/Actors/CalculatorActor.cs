using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using rx13.Messages;

namespace rx13.Actors
{
    public class CalculatorActor : ReceiveActor
    {
        public CalculatorActor()
        {
            Receive<AddMessage>(input =>
            {
                Sender.Tell(new AnswerMessage(input.Term1 + input.Term2));
            });
        }
    }
}
