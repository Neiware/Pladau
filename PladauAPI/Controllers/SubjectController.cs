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

        [HttpGet("{id}/subjects")]
        public async Task<Subject> GetById(string id)
        {
            return await _mongoDBService.GetByIdAsync<Subject>(CollectionName, id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Subject subject)
        {
            await _mongoDBService.CreateAsync<Subject>(CollectionName, subject);
            return CreatedAtAction(nameof(Get), new { id = subject.Id }, subject);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] Subject updateSubject)
        {
            var existingSubject = await _mongoDBService.GetByIdAsync<Subject>(CollectionName, id);

            if (existingSubject == null)
            {
                return NotFound();
            }

            updateSubject.Id = id;
            await _mongoDBService.ReplaceAsync(CollectionName, id, updateSubject);

            return NoContent();
        }
    }
}
