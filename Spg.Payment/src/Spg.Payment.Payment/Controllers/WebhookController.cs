using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MediatR;
using Spg.Payment.DomainModel.Dtos;
using FluentValidation;
using Stripe;
using Spg.Payment.DomainModel.Interfaces;

namespace Spg.Payment.Payment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class WebHookController : ControllerBase
    {
        //private readonly Mediator _mediator;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly ILogger<WebHookController> _logger;
        private readonly IWebhookService _webhookService;

        public WebHookController(IWebHostEnvironment env, IConfiguration configuration, ILogger<WebHookController> logger, IWebhookService webhookService)
        {
            //_mediator = mediator;
            _env = env;
            _configuration = configuration;
            _logger = logger;
            _webhookService = webhookService;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Webhook received");

            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            const string endpointSecret = "whsec_16594a54f449ccda0c89335a061cab8c22c7ef9c3403377c9368999371e2bbb4";
            try
            {
                var signature = Request.Headers["Stripe-Signature"];

                _webhookService.HandleStripeWebhook(json, endpointSecret, signature);

                return Ok();
            }
            catch (StripeException e)
            {
                _logger.LogError("Error: {0}", e.Message);
                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError("Error: {0}", e.Message);
                return StatusCode(500);
            }
        }
    }
}
