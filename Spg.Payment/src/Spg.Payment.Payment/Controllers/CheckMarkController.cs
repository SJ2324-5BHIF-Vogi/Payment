using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Spg.Payment.Payment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CheckMarkController : ControllerBase
    {
        private readonly Mediator _mediator;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CheckMarkController> _logger;

        public CheckMarkController(Mediator mediator, IWebHostEnvironment env, IConfiguration configuration, ILogger<CheckMarkController> logger)
        {
            _mediator = mediator;
            _env = env;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult IsVerifiedTill()
        {
            return Ok();
        }
    }
}
