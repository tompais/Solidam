namespace Interfaces
{
    public interface IGetService<T> where T : class
    {
        T Get(T model);
    }
}