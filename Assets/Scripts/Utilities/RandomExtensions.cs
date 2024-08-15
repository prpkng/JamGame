using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Utilities
{
    public static class RandomExtensions
    {
        public static T PickRandom<T>(this IEnumerable<T> self)
        {
            return self.ElementAt(Random.Range(0, self.Count()));
        }
    }
}