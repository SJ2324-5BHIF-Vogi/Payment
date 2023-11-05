using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MediatR;
using Spg.Payment.DomainModel.Dtos;
using FluentValidation;

namespace Spg.Payment.Payment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class WebHookController : ControllerBase
    {
        private readonly Mediator _mediator;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly ILogger<WebHookController> _logger;

        public WebHookController(Mediator mediator, IWebHostEnvironment env, IConfiguration configuration, ILogger<WebHookController> logger)
        {
            _mediator = mediator;
            _env = env;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult WebHook()
        {
            return Ok();
        }
    }
}
