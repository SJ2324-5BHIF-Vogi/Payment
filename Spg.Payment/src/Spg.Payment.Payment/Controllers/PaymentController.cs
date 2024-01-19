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

        /*[HttpPost]
        public IActionResult Create([FromBody] CreatePaymentDto createPaymentDto)
        {
            _createPaymentValidator.ValidateAndThrow(createPaymentDto);
            return (IActionResult)_mediator.Send(createPaymentDto);
        }*/

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentDto createPaymentDto)
        {
            try
            {
                // Validate the incoming DTO using FluentValidation
                _createPaymentValidator.ValidateAndThrow(createPaymentDto);

                // Send the request to the Mediator for handling
                var paymentId = await _mediator.Send(createPaymentDto);

                // Assuming the Mediator sends back the created payment ID
                return CreatedAtAction("GetPayment", new { id = paymentId }, null);
            }
            catch (ValidationException ex)
            {
                // Handle validation errors
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                _logger.LogError(ex, "An error occurred while processing the payment creation request.");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
