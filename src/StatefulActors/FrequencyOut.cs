// -----------------------------------------------------------------------
// <copyright file="FrequencyOut.cs" company="Petabridge, LLC">
//      Copyright (C) 2015 - 2019 Petabridge, LLC <https://petabridge.com>
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace StatefulActors
{
    public sealed class FrequencyOut
    {
        public FrequencyOut(IReadOnlyDictionary<string, int> observedFrequencies)
        {
            ObservedFrequencies = observedFrequencies;
        }

        public IReadOnlyDictionary<string, int> ObservedFrequencies { get; }

        /// <summary>
        /// Special case for when the <see cref="LineIn"/> had no usable tokens
        /// </summary>
        public static FrequencyOut Empty = new FrequencyOut(new Dictionary<string, int>());

        public override string ToString()
        {
            if (ObservedFrequencies.Count == 0)
            {
                return "WordFrequencies(Empty)";
            }

            return $"WordFrequencies({string.Join(",", ObservedFrequencies.Select(x => x.Key + "=" + x.Value))})";
        }
    }
}