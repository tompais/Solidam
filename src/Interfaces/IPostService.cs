namespace Interfaces
{
    public interface IPostService<T> where T : class
    {
        T Post(T model);
    }
}