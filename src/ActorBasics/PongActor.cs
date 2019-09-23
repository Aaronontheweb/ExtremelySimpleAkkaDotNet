// -----------------------------------------------------------------------
// <copyright file="PongActor.cs" company="Petabridge, LLC">
//      Copyright (C) 2015 - 2019 Petabridge, LLC <https://petabridge.com>
// </copyright>
// -----------------------------------------------------------------------

using System;
using Akka.Actor;
using Akka.Event;
using Akka.Util;

namespace ActorBasics
{
    public class PongActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();
        private readonly IActorRef _pingActor;

        public PongActor(IActorRef pingActor)
        {
            _pingActor = pingActor;
            Receive<Pong>(p =>
            {
                _log.Info("Received {0}", p);

                // reply back at a random, short interval
                var replyTime = TimeSpan.FromSeconds(
                    ThreadLocalRandom.Current.Next(1, 5));

                Context.System.Scheduler.ScheduleTellOnce(
                    replyTime, // delay
                    _pingActor, // target
                    p.Next(), // message
                    Self); // sender (optional)
            });
        }
    }
}