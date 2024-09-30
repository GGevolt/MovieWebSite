using Microsoft.AspNetCore.Mvc;
using Server.Model.DTO;
using Stripe;

namespace MovieWebSite.Server.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:ApiKey"];
        }
        [HttpPost]
        public ActionResult PaymentIntent([FromBody] PaymentDTO payment)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = payment.Amount,
                Currency = "usd",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            };
            var service = new PaymentIntentService();
            PaymentIntent intent = service.Create(options);
            return Json(new { client_secret = intent.ClientSecret });
        }
        
        [HttpGet("paymentsucess")]
        public ActionResult PaymentSuccessful()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the payment: {ex.Message}");
            }
        }

    }
}
