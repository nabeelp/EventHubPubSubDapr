using Dapr;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Text.Json;

namespace EventCosumerDapr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerController : ControllerBase
    {
        private readonly ILogger<ConsumerController> _logger;

        public ConsumerController(ILogger<ConsumerController> logger)
        {
            _logger = logger;
        }

        [Topic("source-hub-component", "source-hub", enableRawPayload: false)]
        [HttpPost("/onboarding-status")]
        public async Task<ActionResult> PostStatus(Message test)
        {
            _logger.LogInformation("Received: " + JsonSerializer.Serialize(test));
            await Task.Run(DoSomething);
            return Ok(JsonSerializer.Serialize(test));
        }

        private void DoSomething()
        {
            // do something
        }
    }
}
