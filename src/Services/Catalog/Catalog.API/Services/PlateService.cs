using AutoMapper;
using Catalog.API.DTOs;
using Catalog.API.Interfaces;
using Catalog.Business.Interfaces;

namespace Catalog.Business.Services
{
    public class PlateService : Service<Plate>, IPlateService
    {
        private readonly IPlateRepository _plateRepository;
        private readonly IMapper _mapper;

        public PlateService(IPlateRepository repository, IMapper mapper) : base(repository) 
        {
            _plateRepository = repository;
            _mapper = mapper;
        }

        public async Task<PlateDto> Create(PlateDto plateDto)
        {
            var plate = new Plate();
            _mapper.Map(plateDto, plate);

            var savedPlate = await _plateRepository.Add(plate);
            _mapper.Map(savedPlate, plateDto);

            return plateDto;
        }


        public async Task<FilteredPlateResultSet> GetPagedAndFilteredResults(int pageIndex, int pageSize, string? searchTerm)
        {
            var results = _plateRepository.GetAll();
            var totalResultsCount = results.Count();

            if (!string.IsNullOrEmpty(searchTerm))
                results = results.Where(x => x.Registration.Contains(searchTerm));
            
            var pagedResults = await results
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var plateDtos = new List<PlateDto>();
            _mapper.Map(pagedResults, plateDtos);

            return new FilteredPlateResultSet
            {
                Plates = plateDtos,
                TotalPlates = totalResultsCount
            };
        }
    }
}
