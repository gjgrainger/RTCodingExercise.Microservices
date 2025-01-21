
namespace Catalog.Business.Interfaces
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAll();
    }
}
