using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using rx13.Messages;
namespace rx13.Actors
{
    public class MyTypedActor : ReceiveActor
    {
        public MyTypedActor()
        {
            Receive<GreetingMessage>(message =>
            {
                GreetingMessageHandler(message);
                GreetingMessageHandler2(message);
                GreetingMessageHandler3(message);
            });
            Receive<GreetingMessage>(message =>
            {
                GreetingMessageHandler2(message);
            });
            Receive<GreetingMessage>(message =>
            {
                GreetingMessageHandler3(message);
            });
            ReceiveAny(obj => Console.WriteLine($"!!! This is handle by ReceiveAny method. Input is: {obj} !!!"));
        }

        private void GreetingMessageHandler(GreetingMessage greeting)
        {
            Console.WriteLine($"Typed Actor named: {Self.Path.Name}");
            Console.WriteLine($"Received a greeting: {greeting.Greeting}");
            Console.WriteLine($"Actor's path: {Self.Path}");
            Console.WriteLine($"Actor is part of the ActorSystem: {Context.System.Name}");
        }

        private void GreetingMessageHandler2(GreetingMessage greeting)
        {
            Console.WriteLine($" GreetingMessageHandler2 Received a greeting: {greeting.Greeting}");
        }

        private void GreetingMessageHandler3(GreetingMessage greeting)
        {
            Console.WriteLine($" GreetingMessageHandler3 Received a greeting: {greeting.Greeting}");
        }
    }
}
