using Bogus.Bson;
using MediatR;
using Microsoft.Extensions.Logging;
using Spg.Payment.DomainModel.Enums;
using Spg.Payment.DomainModel.Interfaces;
using Spg.Payment.DomainModel.Model;
using Spg.Payment.Repository;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Payment.Application.Services
{
    public class WebhookService : IWebhookService
    {
        private readonly GenericRepository _repository;
        private readonly ILogger<WebhookService> _logger;

        public WebhookService(GenericRepository repository, ILogger<WebhookService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public void HandleStripeWebhook(string json, string endpointSecret, string signature)
        {
            var stripeEvent = EventUtility.ParseEvent(json, false);
            var signatureHeader = signature;

            stripeEvent = EventUtility.ConstructEvent(json,
                    signatureHeader, endpointSecret, throwOnApiVersionMismatch: false);

            // Handle the event
            _logger.LogInformation($"Webhook received: {stripeEvent.Id} - {stripeEvent.Type}");

            if (stripeEvent.Type == Events.PaymentIntentCreated)
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                Console.WriteLine("A payment for {0} was created.", paymentIntent.Amount);
                // Then define and call a method to handle the successful payment intent.
                // handlePaymentIntentSucceeded(paymentIntent);
                //handlePaymentIntendCreated(paymentIntent);

            }
            else if(stripeEvent.Type == Events.PaymentIntentSucceeded)
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                Console.WriteLine("A successful payment for {0} was made.", paymentIntent.Amount);
                // Then define and call a method to handle the successful payment intent.
                // handlePaymentIntentSucceeded(paymentIntent);
            }
            else if (stripeEvent.Type == Events.PaymentMethodAttached)
            {
                var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                // Then define and call a method to handle the successful attachment of a PaymentMethod.
                // handlePaymentMethodAttached(paymentMethod);
            }
            else if (stripeEvent.Type == Events.CheckoutSessionCompleted)
            {
                var PaymentSession = stripeEvent.Data.Object as Session;
                // Then define and call a method to handle the successful attachment of a PaymentMethod.
                // handlePaymentMethodAttached(paymentMethod);
                handlePaymentSessionCompleted(PaymentSession);
            }
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }
        }

        private void handlePaymentIntentSucceeded(PaymentIntent paymentIntent)
        {
            // Fulfill the purchase.
            Console.WriteLine("PaymentIntent was successful!");
        }

        private void handlePaymentSessionCompleted(Session paymentIntent)
        {
            PaidForWhatEnum paidForWhatEnum;
            DateTime validTill = DateTime.Now;

            _logger.LogInformation($"Checkout Session completed for {paymentIntent.AmountTotal}");

            if(paymentIntent.AmountTotal == 100*100)
            {
                paidForWhatEnum = PaidForWhatEnum.Yearly;
                validTill = DateTime.Now.AddYears(1);
            }
            else if(paymentIntent.AmountTotal == 10*100)
            {
                paidForWhatEnum = PaidForWhatEnum.Monthly;
                validTill = DateTime.Now.AddMonths(1);
            }
            else
            {
                throw new Exception("Invalid amount");
            }

            int userId;

            _logger.LogInformation($"PaymentIntent Metadata: {paymentIntent.Metadata}");

            if(paymentIntent.Metadata.TryGetValue("userId", out var userIdString))
            {
                userId = int.Parse(userIdString);
            }
            else
            {
                _logger.LogInformation($"PaymentIntent Metadata: {paymentIntent.Metadata.TryGetValue("userId", out var tes1)}");
                throw new Exception("User ID not found");
            }

            var user = _repository.GetAll<User>().FirstOrDefault(p => p.MyUser == userId); ;

            if (user == null)
            {
                _logger.LogInformation($"User with ID {userId} not found. Creating a new user.");
                // Create a new user
                user = new DomainModel.Model.User(userId);
                _repository.Add(user);
            }

            var payment = new DomainModel.Model.Payment(user, paidForWhatEnum, DateTime.Now, validTill, (long)paymentIntent.AmountTotal, 0);
            payment.Active = true;
            _logger.LogInformation($"Payment for user {userId} created. Valid till {validTill}. Payment HashCode {payment.Hash}");

            _repository.Add(payment);

            user.AddPayment(payment);
            _repository.Update(user);
        }
    }
}
