namespace Interfaces
{
    public interface IFullDeleteService<T> : IDeleteByIdService<T>, IDeleteService<T> where T : class
    {
        
    }
}