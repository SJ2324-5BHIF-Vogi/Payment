using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MediatR;
using Spg.Payment.DomainModel.Dtos;
using FluentValidation;
using Spg.Payment.DomainModel.Interfaces;

namespace Spg.Payment.Payment.Controllers
{
    [ApiController]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    //[ApiVersion("1.0")]
    //[Authorize]

    public class PaymentController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentController> _logger;
        private readonly IValidator<CreatePaymentDto> _createPaymentValidator;
        private readonly IPaymentService _paymentService;

        public PaymentController(IWebHostEnvironment env, IConfiguration configuration, ILogger<PaymentController> logger, IValidator<CreatePaymentDto> createPaymentValidator, IPaymentService paymentService)
        {
            _env = env;
            _configuration = configuration;
            _logger = logger;
            _createPaymentValidator = createPaymentValidator;
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> IsPaid([FromQuery] Guid hashCode)
        {
            var isPaid = await _paymentService.IsPaid(hashCode);
            return Ok(isPaid);
        }

        /*[HttpPost]
        public IActionResult Create([FromBody] CreatePaymentDto createPaymentDto)
        {
            _createPaymentValidator.ValidateAndThrow(createPaymentDto);
            return (IActionResult)_mediator.Send(createPaymentDto);
        }*/

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentDto createPaymentDto)
        {
            // Validate the incoming DTO using FluentValidation
            _createPaymentValidator.ValidateAndThrow(createPaymentDto);

            // Send the request to the Mediator for handling
            //var paymentId = await _mediator.Send(createPaymentDto);
            var paymentId = await _paymentService.CreatePayment(createPaymentDto);

            // Assuming the Mediator sends back the created payment ID
            return CreatedAtAction("GetPayment", new { id = paymentId }, null);
        }
    }
}
