using System;

namespace Abstractions
{
    public abstract class AbstractSingleton<T> where T : class
    {
        private static T _instance;
        public static T Instance => _instance ?? (_instance = (T) Activator.CreateInstance(typeof(T), true));
    }
}