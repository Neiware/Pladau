using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PladauAPI.Models;
using PladauAPI.Services;

namespace PladauAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;
        private readonly string CollectionName = "universities";

        public UniversityController(MongoDBService mongoDBService)
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

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] University updatedUniversity)
        {
            var existingUniversity = await _mongoDBService.GetByIdAsync<University>(CollectionName, id);

            if (existingUniversity == null)
            {
                return NotFound();
            }

            updatedUniversity.Id = id;
            await _mongoDBService.ReplaceAsync(CollectionName, id, updatedUniversity);

            return NoContent();
        }
    }
}
