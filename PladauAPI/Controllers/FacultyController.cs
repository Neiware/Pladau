using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PladauAPI.Models;
using PladauAPI.Services;

namespace PladauAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;
        private readonly string CollectionName = "faculties";

        public FacultyController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }
        [HttpGet]
        public async Task<List<Faculty>> Get()
        {
            return await _mongoDBService.GetAllAsync<Faculty>(CollectionName);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Faculty faculty)
        {
            await _mongoDBService.CreateAsync<Faculty>(CollectionName, faculty);
            return CreatedAtAction(nameof(Get), new { id = faculty.Id }, faculty);
        }
    }
}
