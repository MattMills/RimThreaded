﻿using System.Collections.Generic;
using System.Threading;

namespace RimThreaded
{
    /// <summary>
    /// Use this class only when you can demonstrate an object dies after 1 tick and you can't track its life.
    /// If you can track its life use the SimplePool instead.
    /// </summary>
    public static class OneTickPool<T> where T : new()
    {
        private static readonly List<T> ObjectsList = new List<T>();

        public static int ObjectsCount => ObjectsList.Count;

        private static int Pivot = -1;// everything to the left is being used, everything to the right is free.

        /// <summary>
        /// REMEMBER OBJECTS RETURNED FROM THIS MUST BE CLEARED
        /// </summary>
        public static T Get()
        {
            int index = Interlocked.Increment(ref Pivot);

            if (index >= ObjectsCount)
            {
                lock (ObjectsList)
                {
                    while (index >= ObjectsCount)
                    {
                        ObjectsList.Add(new T());
                    }
                }
            }
            return ObjectsList[index];
        }
        public static void Tick()
        {
            Pivot = -1;
        }

    }
}