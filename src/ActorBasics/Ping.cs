using System;
using System.Collections.Generic;
using System.Text;

namespace ActorBasics
{
    public sealed class Ping
    {
        public Ping(int count)
        {
            Count = count;
        }
        
        public int Count { get; }

        public Pong Next()
        {
            // Immutable (best practice) reply
            return new Pong(Count + 1);
        }

        public override string ToString()
        {
            return $"Ping({Count})";
        }
    }
}
