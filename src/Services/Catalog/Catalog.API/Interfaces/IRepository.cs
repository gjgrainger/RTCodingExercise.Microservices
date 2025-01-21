namespace Catalog.API.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> Add(T item);
        IQueryable<T> GetAll();

        // Originally here I had `Task<bool> Exists(Guid id)` however on building out the application
        // found that Plate repository needed to implement the method when searching by Registration
        // so I chose to leave it out the interface

    }
}
