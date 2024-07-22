using System;
using Akka.Actor;
using rx13.Actors;
using rx13.Messages;

namespace rx13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BasicActorCreationUsingTell();

        }
        static void BasicActorCreationUsingTell()
        {
            ActorSystem system = ActorSystem.Create("my-first-akka");

            IActorRef untypedActor = system.ActorOf<MyUntypedActor>("untyped-actor-name");
            IActorRef typedActor = system.ActorOf<MyTypedActor>("typed-actor-name");

            untypedActor.Tell(new GreetingMessage("Hello untyped actor!"));
            typedActor.Tell(new GreetingMessage("Hello typed actor!"));

            Console.Read();
            system.Terminate();
        }

    }
}
