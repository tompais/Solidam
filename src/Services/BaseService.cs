﻿using Abstractions;
using Models;

namespace Services
{
    public class BaseService<T> : AbstractSingleton<T> where T : class
    {
        protected BaseService()
        {
        }

        protected static SolidamEntities Db => SolidamContext.Instance;
    }
}