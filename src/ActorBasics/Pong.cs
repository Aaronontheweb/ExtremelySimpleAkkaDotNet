// -----------------------------------------------------------------------
// <copyright file="Pong.cs" company="Petabridge, LLC">
//      Copyright (C) 2015 - 2019 Petabridge, LLC <https://petabridge.com>
// </copyright>
// -----------------------------------------------------------------------
namespace ActorBasics
{
    public sealed class Pong
    {
        public Pong(int count)
        {
            Count = count;
        }

        public int Count { get; }

        public Ping Next()
        {
            // Immutable (best practice) reply
            return new Ping(Count +1);
        }

        public override string ToString()
        {
            return $"Pong({Count})";
        }
    }
}