using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Akka.Actor;

namespace StatefulActors
{
    /// <summary>
    /// Actor that receives text from commandline
    /// and counts word frequency.
    /// </summary>
    public class WordFrequencyActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _wordFrequencies;

        public WordFrequencyActor() : this(new Dictionary<string, int>())
        {
        }

        public WordFrequencyActor(Dictionary<string, int> wordFrequencies)
        {
            _wordFrequencies = wordFrequencies;

            Receive<LineIn>(l =>
            {
                var tokenize = l.Line?.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                // special case - check to see if we got garbage
                if (tokenize == null || tokenize.Length == 0)
                {
                    Sender.Tell(FrequencyOut.Empty);
                    return;
                }

                var resp = new Dictionary<string, int>();
                // convert all tokens to lower case and update state
                foreach (var token in tokenize.Select(x => x.ToLowerInvariant()))
                {
                    if (_wordFrequencies.TryGetValue(token, out var current))
                    {
                        _wordFrequencies[token] = current + 1;
                    }
                    else
                    {
                        _wordFrequencies[token] = 1;
                    }

                    resp[token] = _wordFrequencies[token];
                }

                // reply to sender with updated frequencies
                Sender.Tell(new FrequencyOut(resp));
            });
        }
    }
}
