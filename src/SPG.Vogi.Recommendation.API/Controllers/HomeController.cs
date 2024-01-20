using Microsoft.AspNetCore.Mvc;
using SPG.Vogi.Recommendation.Application;

namespace SPG.Vogi.Recommendation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private readonly RabbitMqService _rabbitMqService;

        public MessageController(ILogger<MessageController> logger, RabbitMqService rabbitMqService)
        {
            _logger = logger;
            _rabbitMqService = rabbitMqService;
        }

        [HttpPost]
        public IActionResult Send([FromBody] string message)
        {
            _rabbitMqService.SendMessage("messages", message);
            _logger.LogInformation("Send request processed successfully");
            return Ok();
        }

        [HttpGet]
        public IActionResult Receive()
        {
            var message = _rabbitMqService.ReceiveMessage("messages");
            if (message != null)
            {
                _logger.LogInformation("Receive request processed successfully");
                return Ok(message);
            }
            else
            {
                _logger.LogInformation("No Content found");
                return NoContent();
            }
        }
    }
}
