namespace Interfaces
{
    public interface IService<T> : IGetService<T>, IPostService<T>, IPutService<T>, IDeleteService<T>
    {
    }
}