using Catalog.API.Interfaces;
using Catalog.Domain;
using Microsoft.Extensions.Logging;

namespace Catalog.Business.Services
{
    public class PlateService : Service<Plate>
    {
        public PlateService(IRepository<Plate> repository, ILogger logger) : base(repository, logger)
        {
        }
    }
}
