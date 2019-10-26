namespace Interfaces
{
    public interface IFullGetService<T> : IGetService<T>, IGetByIdService<T> where T : class
    {
        
    }
}