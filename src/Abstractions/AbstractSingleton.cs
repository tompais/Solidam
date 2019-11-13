﻿using System;

namespace Abstractions
{
    public abstract class AbstractSingleton<T> where T : class
    {
        private static readonly Lazy<T>
            Lazy =
                new Lazy<T>
                    (() => Activator.CreateInstance(typeof(T), true) as T);

        public static T Instance => Lazy.Value;
    }
}