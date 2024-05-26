using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PladauAPI.Models;
using PladauAPI.Services;

namespace PladauAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;
        private readonly string CollectionName = "admins";

        public AdminController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }
        [HttpGet]
        public async Task<List<Admin>> Get() 
        {
            return await _mongoDBService.GetAllAsync<Admin>(CollectionName);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Admin admin) 
        {
            await _mongoDBService.CreateAsync<Admin>(CollectionName, admin);
            return CreatedAtAction(nameof(Get), new { id = admin.Id }, admin);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> AddToPlaylist(string id, [FromBody] string movieId) { }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(string id) { }
    }
}
