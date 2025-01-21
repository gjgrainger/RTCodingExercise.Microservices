
namespace Catalog.Business.Interfaces
{
    public interface IService<T>
    {
        IQueryable<T> GetAll();
    }
}
