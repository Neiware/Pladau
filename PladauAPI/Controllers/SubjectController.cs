using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PladauAPI.Models;
using PladauAPI.Services;

namespace PladauAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;
        private readonly string CollectionName = "subjects";

        public SubjectController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }
        [HttpGet]
        public async Task<List<Subject>> Get()
        {
            return await _mongoDBService.GetAllAsync<Subject>(CollectionName);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Subject subject)
        {
            await _mongoDBService.CreateAsync<Subject>(CollectionName, subject);
            return CreatedAtAction(nameof(Get), new { id = subject.Id }, subject);
        }
    }
}
