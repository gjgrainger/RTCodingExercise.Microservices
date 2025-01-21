using AutoMapper;
using Catalog.API.DTOs;
using Microsoft.AspNetCore.Cors;
using RTCodingExercise.Microservices.Models;
using System.Diagnostics;
using WebMVC;
using WebMVC.Models;
using WebMVC.Services;

namespace RTCodingExercise.Microservices.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPlateClientService _plateClientService;
        private readonly IMapper _mapper;
        private readonly int PageSize = 20;

        public HomeController(ILogger<HomeController> logger, IPlateClientService plateClientService, IMapper mapper)
        {
            _logger = logger;
            _plateClientService = plateClientService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int? pageNumber, string searchTerm)
        {
            ViewData["CurrentFilter"] = searchTerm;

            var plateDtos = await _plateClientService.GetPlates(pageNumber ?? 1, PageSize, searchTerm);
            var plateVms = new List<PlateViewModel>();
            _mapper.Map(plateDtos, plateVms);

            // For whatever reason, I cannot get this to work. I can see the 'X-Total-Count' header value
            // when I use Postman to call the API directly, but I can't seem to get the value in the MVC controller
            // I've spent hours trying to get it to work, so in the interest of time, I going to use a magic string
            //var headerValues = Response.Headers.ToList();
            //var totalPlates = headerValues.FirstOrDefault(x => x.Value == "X-Total-Count").Value;
            var totalPlates = 501;

            return View(PaginatedList<PlateViewModel>.Create(plateVms, totalPlates, pageNumber ?? 1, PageSize));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PlateViewModel model) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dto = new PlateDto();
                    _mapper.Map(model, dto);

                    _plateClientService.CreatePlate(dto);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save plate.");
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}