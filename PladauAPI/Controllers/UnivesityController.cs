using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PladauAPI.Models;
using PladauAPI.Services;

namespace PladauAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnivesityController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;
        private readonly string CollectionName = "universities";

        public UnivesityController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }
        [HttpGet]
        public async Task<List<University>> Get()
        {
            return await _mongoDBService.GetAllAsync<University>(CollectionName);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] University university)
        {
            await _mongoDBService.CreateAsync<University>(CollectionName, university);
            return CreatedAtAction(nameof(Get), new { id = university.Id }, university);
        }
    }
}
