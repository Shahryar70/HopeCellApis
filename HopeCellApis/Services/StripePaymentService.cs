// Services/StripePaymentService.cs
using HopeCellApis.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe;

namespace HopeCellApis.Services
{
    public class StripePaymentService : IPaymentService
    {
        private readonly StripeSettings _stripeSettings;
        private readonly ILogger<StripePaymentService> _logger;

        public StripePaymentService(
            IOptions<StripeSettings> stripeSettings,
            ILogger<StripePaymentService> logger)
        {
            _stripeSettings = stripeSettings.Value;
            _logger = logger;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }

        public async Task<PaymentResponse> ProcessPayment(Donation donation)
        {
            try
            {
                _logger.LogInformation("Creating payment intent for {Amount} PKR", donation.Amount);

                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(donation.Amount * 100), // Convert to cents
                    Currency = "pkr",
                    Description = $"Donation from {donation.Name}",
                    Metadata = new Dictionary<string, string>
                {
                    { "email", donation.Email },
                    { "phone", donation.Phone }
                },
                    PaymentMethodTypes = new List<string> { "card" }
                };

                var service = new PaymentIntentService();
                var paymentIntent = await service.CreateAsync(options);

                _logger.LogInformation("Created payment intent: {PaymentIntentId}", paymentIntent.Id);

                return new PaymentResponse
                {
                    TransactionId = paymentIntent.Id,
                    PaymentUrl = paymentIntent.Status == "requires_action"
                        ? paymentIntent.NextAction.RedirectToUrl.Url
                        : $"https://checkout.stripe.com/pay/{paymentIntent.Id}",
                    Status = paymentIntent.Status
                };
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe error: {StripeError}", ex.StripeError?.Message);
                throw new ApplicationException("Payment gateway error: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Payment processing error");
                throw new ApplicationException("Payment processing failed");
            }
        }
    }
}