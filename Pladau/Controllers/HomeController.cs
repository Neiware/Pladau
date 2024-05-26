using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
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
        public async Task<IActionResult> Faculty(string universityId)
        {
            var faculties = await _apiService.GetFacultiesByIdUni(universityId);
            return View(faculties);
        }

        public async Task<IActionResult> Carrer(string facultyId)
        {
            var carrers = await _apiService.GetCarrerByIdFaculty(facultyId);
            return View(carrers);
        }

        public async Task<IActionResult> Subject(string carrerId)
        {
            var subjects = await _apiService.GetSubjectsByIdCarrers(carrerId);
            return View(subjects);
        }

        public async Task<IActionResult> RecommendedBySubject(string subjectId)
        {
            var subject = await _apiService.GetSubjectById(subjectId);
            return View(subject);
        }
        public async Task<IActionResult> Foro()
        {
            var post = await _apiService.GetAllPost();
            return View(post);
        }
        public IActionResult Donation()
        {
            return View();
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
