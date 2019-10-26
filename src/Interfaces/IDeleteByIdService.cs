namespace Interfaces
{
    public interface IDeleteByIdService<T> where T : class
    {
        T Delete(ulong id);
    }
}