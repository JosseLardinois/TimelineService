using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2.DataModel;
using TimelineService.Model;
using TimelineService.Interfaces;

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


        [HttpPost]
        public async Task<ActionResult> Create(FollowerInputModel model)
        {
            await _repository.Add(model);
            return Ok(model);
        }

    }
}