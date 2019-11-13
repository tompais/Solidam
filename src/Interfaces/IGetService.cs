using System.Collections.Generic;

namespace Interfaces
{
    public interface IGetService<T> where T : class
    {
        List<T> Get(T model);
    }
}