namespace Interfaces
{
    public interface IGetByIdService<T> where T : class
    {
        T GetById(ulong id);
    }
}