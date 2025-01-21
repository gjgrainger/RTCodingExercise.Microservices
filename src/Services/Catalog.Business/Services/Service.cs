using Catalog.API.Interfaces;
using Catalog.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Business.Services
{
    public abstract class Service<T> 
    {
        private readonly IRepository<T> _repository;
        private readonly ILogger _logger;

        protected Service(IRepository<T> repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _repository.GetAll().ToListAsync();
        }
    }
}
