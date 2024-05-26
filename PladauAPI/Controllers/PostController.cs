using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PladauAPI.Models;
using PladauAPI.Services;

namespace PladauAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;
        private readonly string CollectionName = "posts";

        public PostController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }
        [HttpGet]
        public async Task<List<Post>> Get()
        {
            return await _mongoDBService.GetAllAsync<Post>(CollectionName);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Post post)
        {
            await _mongoDBService.CreateAsync<Post>(CollectionName, post);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] Post updatedPost)
        {
            var existingPost = await _mongoDBService.GetByIdAsync<Post>(CollectionName, id);

            if (existingPost == null)
            {
                return NotFound();
            }

            updatedPost.Id = id;
            await _mongoDBService.ReplaceAsync(CollectionName, id, updatedPost);

            return NoContent();
        }
    }
}
