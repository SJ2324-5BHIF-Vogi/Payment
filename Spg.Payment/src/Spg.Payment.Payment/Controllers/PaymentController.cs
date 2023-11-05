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
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    //[ApiVersion("1.0")]
    //[Authorize]

    public class PaymentController : ControllerBase
    {
        private readonly Mediator _mediator;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentController> _logger;
        private readonly IValidator<CreatePaymentDto> _createPaymentValidator;

        public PaymentController(Mediator mediator, IWebHostEnvironment env, IConfiguration configuration, ILogger<PaymentController> logger, IValidator<CreatePaymentDto> createPaymentValidator)
        {
            _mediator = mediator;
            _env = env;
            _configuration = configuration;
            _logger = logger;
            _createPaymentValidator = createPaymentValidator;
        }

        [HttpGet]
        public IActionResult IsPaid([FromQuery] HashCode hashCode)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePaymentDto createPaymentDto)
        {
            _createPaymentValidator.ValidateAndThrow(createPaymentDto);
            return (IActionResult)_mediator.Send(createPaymentDto);
        }
    }
}
