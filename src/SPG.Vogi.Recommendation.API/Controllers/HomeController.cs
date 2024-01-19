using Microsoft.AspNetCore.Mvc;
using SPG.Vogi.Recommendation.Application;

namespace SPG.Vogi.Recommendation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly RabbitMqService _rabbitMqService;

        public MessageController(RabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        [HttpPost]
        public IActionResult Send([FromBody] string message)
        {
            _rabbitMqService.SendMessage("messages", message);
            return Ok();
        }

        [HttpGet]
        public IActionResult Receive()
        {
            var message = _rabbitMqService.ReceiveMessage("messages");
            if (message != null)
            {
                return Ok(message);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
