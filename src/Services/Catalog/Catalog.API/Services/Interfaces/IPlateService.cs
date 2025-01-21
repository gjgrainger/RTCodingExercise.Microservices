using Catalog.API.DTOs;

namespace Catalog.Business.Interfaces
{
    public interface IPlateService : IService<Plate>
    {
        Task<FilteredPlateResultSet> GetPagedAndFilteredResults(int pageIndex, int pageSize, string? searchTerm);

        Task<PlateDto> Create(PlateDto dto);
    }
}
