using System;
using System.Threading.Tasks;
using Akka.Actor;

namespace StatefulActors
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var actorSystem = ActorSystem.Create("WordCount");

            IActorRef wordFrequencyActor = actorSystem.ActorOf(
                Props.Create(() => new WordFrequencyActor()), 
                "words");

            Console.WriteLine("Type a line to have its word frequencies counted.");
            Console.WriteLine("The actors will aggregate the frequencies of words" +
                              "across all lines it receives");
            Console.WriteLine("Type /exit to quit.");
            bool stillRunning = true;
            while (stillRunning)
            {
                var line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    continue;
                if (line.Equals("/exit"))
                    stillRunning = false;
                else
                {
                    var lineFrequency = await wordFrequencyActor.Ask<FrequencyOut>(new LineIn(line), 
                        TimeSpan.FromSeconds(1));
                    Console.WriteLine(lineFrequency);
                }
            }

            await actorSystem.Terminate();
        }
    }
}
