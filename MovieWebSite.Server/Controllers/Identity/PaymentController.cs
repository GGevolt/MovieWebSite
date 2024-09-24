using Microsoft.AspNetCore.Mvc;
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
        public ActionResult Post()
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = 1099,
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


    }
}
