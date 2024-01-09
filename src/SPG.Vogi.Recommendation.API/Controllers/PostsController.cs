using Microsoft.AspNetCore.Mvc;
using SPG.Vogi.Recommendation.Application;
using SPG.Vogi.Recommendation.DomainModel;

namespace SPG.Vogi.Recommendation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly RecommService _service;



        private readonly ILogger<PostsController> _logger;

        public PostsController(ILogger<PostsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]

        public ActionResult<IEnumerable<Posts>> Get(int id)
        {
            var result = _service.getPosts(id);
            if(result is null)
                return NotFound();
            return Ok(result);
        }
    }
}