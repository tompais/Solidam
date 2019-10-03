using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions
{
    public abstract class AbstractBaseSingleton<T> where T : class
    {
        private static T _instance;
        public static T Instance => _instance ?? (_instance = (T) Activator.CreateInstance(typeof(T), true));
    }
}
