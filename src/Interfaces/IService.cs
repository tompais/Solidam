namespace Interfaces
{
    public interface IService<T> : IFullGetService<T>, IPostService<T>, IPutService<T>, IFullDeleteService<T> where T : class
    {
    }
}