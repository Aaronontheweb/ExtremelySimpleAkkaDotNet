using System;
using System.Threading.Tasks;
using Akka.Actor;

namespace ActorBasics
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // create ActorSystem (allows actors to talk in-memory)
            var actorSystem = ActorSystem.Create("PingPong");

            // create Props for PingActor
            // Props == formula used to start an actor.
            // * Can be shared among many actor instances.
            // * Used to help actors recover when they restart.
            // * Can be serialized over network.
            var pingActorProps = Props.Create(() => new PingActor());

            // start pingActor and get actor reference (IActorRef)
            // IActorRef == handle we use to send messages to actor
            IActorRef pingActor = actorSystem.ActorOf(pingActorProps, "ping");

            // props for PongActor (depends on PingActor)
            var pongActorProps = Props.Create(() => new PongActor(pingActor));

            // start pongActor
            IActorRef pongActor = actorSystem.ActorOf(pongActorProps, "pong");

            // start ping-pong between actors
            pongActor.Tell(new Pong(0));

            // wait for ActorSystem to be terminated
            // (indefinitely in this app)
            await actorSystem.WhenTerminated;
        }
    }
}
