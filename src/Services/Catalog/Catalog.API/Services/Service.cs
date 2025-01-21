using Catalog.API.Interfaces;

namespace Catalog.Business.Services
{
    public abstract class Service<T> 
    {
        private readonly IRepository<T> _repository;

        protected Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
