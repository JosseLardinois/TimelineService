using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2.DataModel;
using TimelineService.Model;

namespace TimelineService.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimelineController : ControllerBase
    {
        private readonly IDynamoDBContext _context;

        public TimelineController(IDynamoDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Post postRequest)
        {
            var post = await _context.LoadAsync<Post>(postRequest.Id);
            if (post != null) return BadRequest($"Post with Id {postRequest.Id} Already Exists");
            await _context.SaveAsync(postRequest);
            return Ok(postRequest);
        }

    }
}