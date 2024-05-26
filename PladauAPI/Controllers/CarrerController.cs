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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Carrer carrer)
        {
            await _mongoDBService.CreateAsync<Carrer>(CollectionName, carrer);
            return CreatedAtAction(nameof(Get), new { id = carrer.Id }, carrer);
        }
    }
}
