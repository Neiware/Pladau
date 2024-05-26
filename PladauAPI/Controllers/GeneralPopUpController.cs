using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PladauAPI.Models;
using PladauAPI.Services;

namespace PladauAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralPopUpController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;
        private readonly string CollectionName = "generalPopUps";

        public GeneralPopUpController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }
        [HttpGet]
        public async Task<List<GeneralPopUp>> Get()
        {
            return await _mongoDBService.GetAllAsync<GeneralPopUp>(CollectionName);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GeneralPopUp generalPopUp)
        {
            await _mongoDBService.CreateAsync<GeneralPopUp>(CollectionName, generalPopUp);
            return CreatedAtAction(nameof(Get), new { id = generalPopUp.Id }, generalPopUp);
        }
    }
}
