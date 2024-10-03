using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Server.Model.DTO;
using Stripe.Events;
using Stripe;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Server.Model.Models;
using Stripe.Checkout;
using Newtonsoft.Json;

namespace MovieWebSite.Server.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly StripeSettingDTO _settings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public PaymentController(IConfiguration configuration, IOptions<StripeSettingDTO> settings, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _settings = settings.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        [HttpPost("create_checkout_session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CheckOutSessionDTO checkOutSession)
        {
            var options = new SessionCreateOptions
            {
                SuccessUrl = checkOutSession.SuccessUrl,
                CancelUrl = checkOutSession.FailureUrl,
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                Mode = "subscription",
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = checkOutSession.PriceId,
                        Quantity = 1,
                    },
                },
            };

            var service = new SessionService();
            service.Create(options);
            try
            {
                var session = await service.CreateAsync(options);
                Debug.WriteLine($"Sesstion id: {session.Id}");
                Debug.WriteLine($"Public Key: {_settings.PublicKey}");
                return Ok(new
                {
                    sessionId = session.Id,
                    publicKey = _settings.PublicKey
                });
            }
            catch (StripeException e)
            {
                Debug.WriteLine($"Stripe exception: {e.StripeError.Message}");
                return BadRequest(new 
                {
                    ErrorMessage =  e.StripeError.Message
                });
            }
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


        [HttpPost("webhook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            Event stripeEvent;
            try
            {
                stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"],
                    _settings.WebHookSecret
                );
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something failed {e}");
                return BadRequest();
            }
            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    Debug.WriteLine("A successful payment for {0} was made.", paymentIntent.Amount);
                    break;
                case "payment_method.attached":
                    var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                    Debug.WriteLine("Payment method");
                    break;
                case "checkout.session.completed":
                    // Payment is successful and the subscription is created.
                    // You should provision the subscription and save the customer ID to your database.
                    PaidSuccessfully();
                    break;
                case "invoice.paid":
                    // Continue to provision the subscription as payments continue to be made.
                    // Store the status in your database and check when a user accesses your service.
                    // This approach helps you avoid hitting rate limits.
                    break;
                case "invoice.payment_failed":
                    // The payment failed or the customer does not have a valid payment method.
                    // The subscription becomes past_due. Notify your customer and send them to the
                    // customer portal to update their payment information.
                    FailToPaid();
                    break;
                default:
                    Debug.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                    break;
            }
            return Ok();
        }

        private async void PaidSuccessfully()
        {
            var currentUser = await _signInManager.UserManager.GetUserAsync(User);
            if (currentUser == null) {
                Debug.WriteLine("Can't find the paided user");
                return;
            }
            var roleResult = await _userManager.AddToRoleAsync(currentUser, "UserT1");
            if (!roleResult.Succeeded)
            {
                Debug.WriteLine("Fail to assign role to paided user");
            }
        }
        private async void FailToPaid()
        {
            var currentUser = await _signInManager.UserManager.GetUserAsync(User);
            if (currentUser == null)
            {
                Debug.WriteLine("Can't find the fail to paid user");
                return;
            }
            var roles = await _userManager.GetRolesAsync(currentUser);
            if (roles != null && roles.Contains("UserT1")) {
                await _userManager.RemoveFromRoleAsync(currentUser, "UserT1");
            }
        }
    }
}
