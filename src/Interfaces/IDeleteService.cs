namespace Interfaces
{
    public interface IDeleteService<T> where T : class
    {
        T Delete(T model);
    }
}