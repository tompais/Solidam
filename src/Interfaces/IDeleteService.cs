namespace Interfaces
{
    public interface IDeleteService<T> where T : class
    {
        T DeleteById(ulong id);
        T Delete(T model);
    }
}