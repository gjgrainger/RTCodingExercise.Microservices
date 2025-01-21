using Catalog.API.DTOs;
using Catalog.Business.Interfaces;
using System.Text.Json;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("plate")]
    public class PlateController : Controller
    {
        private readonly IPlateService _plateService;

        // See if we can find somewhere to put this common to all controllers
        protected Uri BuildUri(string path) => new($"{Request.Scheme}://{Request.Host.Host}/{path}");

        public PlateController(IPlateService plateService)
        {
            _plateService = plateService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPlates(int pageIndex, int pageSize, string? searchTerm)
        {
            var filteredPlateResultSet = await _plateService.GetPagedAndFilteredResults(pageIndex, pageSize, searchTerm);
            var jsonPlates = JsonSerializer.Serialize(filteredPlateResultSet.Plates);

            Response.Headers.Add("X-Total-Count", filteredPlateResultSet.TotalPlates.ToString());

            return Ok(jsonPlates);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePlate(PlateDto plateDto)
        {
            var returnedPlate = await _plateService.Create(plateDto);
            return Created(BuildUri($"plate/{returnedPlate.Id}"), returnedPlate);
        }

    }
}
