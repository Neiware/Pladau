using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PladauAPI.Models;
using PladauAPI.Services;

namespace PladauAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrerController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;
        private readonly string CollectionName = "carrers";

        public CarrerController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }
        [HttpGet]
        public async Task<List<Carrer>> Get()
        {
            return await _mongoDBService.GetAllAsync<Carrer>(CollectionName);
        }
        [HttpGet("carrers/{id}/subjects")]
        public async Task<List<Subject>> GetSubjectsByIdCarrers(string id)
        {
            return await _mongoDBService.GetSubjectsByIdCarrers(id);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Carrer carrer)
        {
            await _mongoDBService.CreateAsync<Carrer>(CollectionName, carrer);
            return CreatedAtAction(nameof(Get), new { id = carrer.Id }, carrer);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] Carrer updatedCarrer)
        {
            var existingCarrer = await _mongoDBService.GetByIdAsync<Carrer>(CollectionName, id);

            if (existingCarrer == null)
            {
                return NotFound();
            }

            updatedCarrer.Id = id;
            await _mongoDBService.ReplaceAsync(CollectionName, id, updatedCarrer);

            return NoContent();
        }
    }
}
