using Catalog.API.Interfaces;
using MassTransit.Initializers;
namespace Catalog.API.Repositories
{
    public class PlateRepository : Repository<Plate>, IPlateRepository
    {
        public PlateRepository(ApplicationDbContext db) : base(db) { }

        public async Task<bool> Exists(string registration)
        {
            return await _context.Set<Plate>().AsNoTracking().AnyAsync(x => x.Registration == registration);
        }

        public override async Task<Plate> Add(Plate item)
        {
            var exists = await Exists(item.Registration);
            if (exists)
                throw new Exception($"{typeof(Plate).Name} with Registration {item.Registration} already exists");

            var entry = await _context.AddAsync(item);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }
    }
}
