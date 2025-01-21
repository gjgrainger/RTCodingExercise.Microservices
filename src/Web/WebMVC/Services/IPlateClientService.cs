using Catalog.API.DTOs;

namespace WebMVC.Services
{
    public interface IPlateClientService
    {
        Task<List<PlateDto>> GetPlates(int pageIndex, int pageSize, string? searchTerm);

        Task CreatePlate(PlateDto model);

    }
}
