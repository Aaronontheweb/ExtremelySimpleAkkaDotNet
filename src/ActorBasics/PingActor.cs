using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;
using Akka.Event;
using Akka.Util;

namespace ActorBasics
{
    public class PingActor : Akka.Actor.ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();

        public PingActor()
        {
            Receive<Ping>(p =>
            {
                _log.Info("Received {0}", p);

                // reply back at a random, short interval
                var replyTime = TimeSpan.FromSeconds(
                    ThreadLocalRandom.Current.Next(1, 5));

                Context.System.Scheduler.ScheduleTellOnce(
                    replyTime, // delay
                    Sender, // target
                    p.Next(), // message
                    Self); // sender (optional)
            });
        }
    }
}
