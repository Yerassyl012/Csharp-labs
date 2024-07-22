using System;
using Akka.Actor;
using rx13.Messages;
namespace rx13.Actors
{
    public class MyUntypedActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            var greeting = message as GreetingMessage;
            if (greeting != null)
            {
                GreetingMessageHandler(greeting);
                GreetingMessageHandler2(greeting);
                GreetingMessageHandler3(greeting);
            }
        }

        private void GreetingMessageHandler(GreetingMessage greeting)
        {
            Console.WriteLine($"Untyped Actor named: {Self.Path.Name}");
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
