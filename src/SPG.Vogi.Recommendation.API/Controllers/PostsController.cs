using Microsoft.AspNetCore.Mvc;
using SPG.Vogi.Recommendation.Application;
using SPG.Vogi.Recommendation.DomainModel;
using SPG.Vogi.Recommendation.DomainModel.Interfaces;

namespace SPG.Vogi.Recommendation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IRecommService _service;

        private readonly ILogger<PostsController> _logger;

        public PostsController(ILogger<PostsController> logger, IRecommService service)
        {
            _logger = logger;
            _service = service;
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