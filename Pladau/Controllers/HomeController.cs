using Microsoft.AspNetCore.Mvc;
using Pladau.Models;
using Pladau.Services;
using System.Diagnostics;

namespace Pladau.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiService _apiService;
        public HomeController(ILogger<HomeController> logger, ApiService apiService)
        {
            _apiService = apiService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {


            var admins = await _apiService.GetAdminAsync();
            return View(admins);
        }

        public async Task<IActionResult> University()
        {
            var universities = await _apiService.GetAllUniversitiesAsync();
            return View(universities);

        }

        public async Task<IActionResult> Carrer(int id)
        {
            var universities = await _apiService.GetAllUniversitiesAsync();
            return View(universities);
        }

        public async Task<IActionResult> Subject()
        {

            return View()
        }
        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Modify()
        {
            return RedirectToAction("Index", "Modify");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
