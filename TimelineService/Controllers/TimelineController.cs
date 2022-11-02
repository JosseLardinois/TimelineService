using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2.DataModel;
using TimelineService.Model;
using TimelineService.Interfaces;
using TimelineService.Repositories;
using Microsoft.Extensions.Hosting;

namespace TimelineService.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class TimelineController : Controller
    {
        private IFollowersRepository _repository;
        

        private IConfiguration _configuration;

        public TimelineController(IConfiguration configuration, IFollowersRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
        }
        [HttpGet]
        public async Task<string> GetPosts(Guid userId)
        {
            TimelineRepository timelineRepo = new TimelineRepository();
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET,PUT,POST,DELETE,PATCH,OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization, X-Requested-With");

            return await timelineRepo.MakeTimeline(userId);
        }

        [HttpPost]
        public async Task<ActionResult> Create(FollowerInputModel model)
        {
            TimelineRepository repository = new TimelineRepository();
            await repository.MakeTimeline(model.UserId);
            return Ok(model);
        }

    }
}