using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MediatR;
using Spg.Payment.DomainModel.Interfaces;

namespace Spg.Payment.Payment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CheckMarkController : ControllerBase
    {
        //private readonly Mediator _mediator;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CheckMarkController> _logger;
        private readonly IPaymentService _paymentService;

        public CheckMarkController(IWebHostEnvironment env, IConfiguration configuration, ILogger<CheckMarkController> logger, IPaymentService paymentService)
        {
            //_mediator = mediator;
            _env = env;
            _configuration = configuration;
            _logger = logger;
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> IsVerifiedTill([FromQuery] int userId)
        {
            var isVerifiedTill = await _paymentService.IsVerifiedTill(userId);
            return Ok(isVerifiedTill);
        }
    }
}
