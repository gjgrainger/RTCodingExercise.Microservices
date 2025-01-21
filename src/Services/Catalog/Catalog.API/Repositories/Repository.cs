using Catalog.API.Interfaces;

namespace Catalog.API.Repositories
{
    public class Repository<T> : IRepository<T> where T : DbEntity, new()
    {
        protected readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> Add(T item)
        {
            var exists = await Exists(item.Id);
            if (exists)
                throw new Exception($"{typeof(T).Name} with Id {item.Id} already exists");

            var entry = await _context.AddAsync(item);
            return entry.Entity;
        }

        // Using `.AsNoTracking()` here as is read only query
        // NB: https://learn.microsoft.com/en-us/ef/core/querying/tracking#no-tracking-queries
        public virtual async Task<bool> Exists(Guid id)
        {
            return await _context.Set<T>().AsNoTracking().AnyAsync(x => x.Id == id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}
