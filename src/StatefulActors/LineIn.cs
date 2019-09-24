// -----------------------------------------------------------------------
// <copyright file="LineIn.cs" company="Petabridge, LLC">
//      Copyright (C) 2015 - 2019 Petabridge, LLC <https://petabridge.com>
// </copyright>
// -----------------------------------------------------------------------
namespace StatefulActors
{
    public sealed class LineIn
    {
        public LineIn(string line)
        {
            Line = line;
        }

        public string Line { get; }
    }
}