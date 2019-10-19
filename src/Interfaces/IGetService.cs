namespace Interfaces
{
    public interface IGetService<T> where T : class
    {
        T GetById(ulong id);
        T Get(T model);
    }
}