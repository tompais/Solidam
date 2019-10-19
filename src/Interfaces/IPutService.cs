namespace Interfaces
{
    public interface IPutService<T> where T : class
    {
        T Put(T model);
    }
}