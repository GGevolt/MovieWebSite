﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Stripe.Checkout;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MovieWebSite.Server.DTO;
using MovieWebSite.Server.AuthModels;

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

        [Authorize]
        [HttpPost("create_checkout_session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CheckOutSessionDTO checkOutSession)
        {
            string priceId = "";
            if (checkOutSession.Plan.Equals("pro"))
            {
                priceId = _settings.ProPriceId;
            }
            else if (checkOutSession.Plan.Equals("premium"))
            {
                priceId = _settings.PremiumPriceId;
            }
            else
            {
                Debug.WriteLine($"💥Can't find the correct plan to create checkout session!");
                return BadRequest();
            }
            ClaimsPrincipal principals = HttpContext.User as ClaimsPrincipal;
            var claim = principals.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(claim.Value);
            if (user == null)
            {
                return BadRequest();
            }
            SessionCreateOptions options;
            if (!string.IsNullOrEmpty(user.CustomerId))
            {
                options = new SessionCreateOptions
                {
                    SuccessUrl = checkOutSession.SuccessUrl,
                    CancelUrl = checkOutSession.FailureUrl,
                    Customer = user.CustomerId,
                    PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },
                    Mode = "subscription",
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            Price = priceId,
                            Quantity = 1,
                        },
                    },
                };
            }
            else
            {
                var customerService = new CustomerService();
                var customerOptions = new CustomerCreateOptions
                {
                    Email = user.Email,
                    Name = user.FullName
                };
                try
                {
                    var stripeCustomer = await customerService.CreateAsync(customerOptions);
                    options = new SessionCreateOptions
                    {
                        SuccessUrl = checkOutSession.SuccessUrl,
                        CancelUrl = checkOutSession.FailureUrl,
                        Customer = stripeCustomer.Id,
                        PaymentMethodTypes = new List<string>
                        {
                            "card",
                        },
                        Mode = "subscription",
                        LineItems = new List<SessionLineItemOptions>
                        {
                            new SessionLineItemOptions
                            {
                                Price = priceId,
                                Quantity = 1,
                            },
                        },
                    };
                }
                catch (StripeException e)
                {
                    Debug.WriteLine($"💥Stripe Create Customer exception: {e.StripeError.Message}");
                    return BadRequest(new { ErrorMessage = e.StripeError.Message });
                }
            }
            var service = new SessionService();
            service.Create(options);
            try
            {
                var session = await service.CreateAsync(options);
                return Ok(new
                {
                    sessionId = session.Id,
                    publicKey = _settings.PublicKey
                });
            }
            catch (StripeException e)
            {
                Debug.WriteLine($"💥Stripe Create Checkout Session exception: {e.StripeError.Message}");
                return BadRequest(new
                {
                    ErrorMessage = e.StripeError.Message
                });
            }
        }

        [Authorize]
        [HttpPost("customer_portal")]
        public async Task<IActionResult> CustomerPortal([FromBody] CustomerPortalDTO portal)
        {
            try
            {
                ClaimsPrincipal principals = HttpContext.User as ClaimsPrincipal;
                var claim = principals.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(claim.Value);
                if (user == null)
                {
                    return BadRequest();
                }
                var options = new Stripe.BillingPortal.SessionCreateOptions
                {
                    Customer = user.CustomerId,
                    ReturnUrl = portal.ReturnUrl,
                };
                var service = new Stripe.BillingPortal.SessionService();
                var session = await service.CreateAsync(options);
                return Ok(new
                {
                    url = session.Url
                });
            }
            catch (StripeException e)
            {
                Debug.WriteLine($"💥Stripe Customer Portal exception: {e.StripeError.Message}");
                return BadRequest(new
                {
                    ErrorMessage = e.StripeError.Message
                });
            }

        }
        [HttpGet("cancel/{userId}")]
        public async Task<IActionResult> CancelSubScription(string UserId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(UserId);
                if (user == null || user.SubscriptionId == null)
                {
                    return BadRequest("Can't find user or Subscription Id");
                }
                var service = new SubscriptionService();
                service.Cancel(user.SubscriptionId);
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Count == 0)
                {
                    throw new Exception("💥User don't have any role to delete!");
                }
                if (roles.Contains("UserT1"))
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, "UserT1");
                    if (!result.Succeeded)
                    {
                        throw new Exception("💥Fail to remove T1 user when subscription ended!");
                    }
                }
                if (roles.Contains("UserT2"))
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, "UserT2");
                    if (!result.Succeeded)
                    {
                        throw new Exception("💥Fail to remove T2 user when subscription ended!");
                    }
                }
                user.SubscriptionEndPeriod = null;
                user.PriceId = null;
                user.SubscriptionStatus = null;
                user.SubscriptionStartPeriod = null;
                user.SubscriptionId = null;
                var updateUserResult = await _userManager.UpdateAsync(user);
                if (!updateUserResult.Succeeded)
                {
                    throw new Exception("💥Fail to update User when cancel subscription!");
                }
                return Ok();
            }
            catch (Exception ex) {
                Debug.WriteLine($"💥Stripe cancel subscription exception: {ex.Message}");
                return BadRequest();
            }
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
            catch (Exception ex)
            {
                Debug.WriteLine($"💥Something failed {ex}");
                return BadRequest();
            }
            switch (stripeEvent.Type)
            {
                case "customer.subscription.created":
                    var subscription = stripeEvent.Data.Object as Subscription;
                    await PaidSuccessfully(subscription);
                    break;
                case "customer.subscription.updated":
                    var UpdatedSubscription = stripeEvent.Data.Object as Stripe.Subscription;
                    await SubscriptionUpdate(UpdatedSubscription);
                    break;
                case "customer.subscription.deleted":
                    var Endedcustomer = stripeEvent.Data.Object as Stripe.Subscription;
                    await SubscriptionEnd(Endedcustomer);
                    break;
                case "customer.created":
                    var NewCustomer = stripeEvent.Data.Object as Customer;
                    await AddCustomerToDB(NewCustomer);
                    break;
                default:
                    Debug.WriteLine("💥Unhandled event type: {0}", stripeEvent.Type);
                    break;
            }
            return Ok();
        }

        private async Task AddCustomerToDB(Customer customer)
        {
            try
            {
                var userFromDb = await _userManager.FindByEmailAsync(customer.Email);
                if (userFromDb == null)
                {
                    throw new Exception("💥User not found by email: " + customer.Email);
                }
                userFromDb.CustomerId = customer.Id;
                var updateUserResult = await _userManager.UpdateAsync(userFromDb);
                if (!updateUserResult.Succeeded)
                {
                    throw new Exception("💥Fail to add Customer Id to user");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }           
        }
        private async Task PaidSuccessfully(Subscription subscription)
        {
            try
            {
                var customerId = subscription.CustomerId;
                var priceId = subscription.Items.Data[0].Price.Id;
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.CustomerId == customerId);
                if (user == null)
                {
                    throw new Exception("💥Fail to find the paided user");
                }
                if (priceId.Equals(_settings.ProPriceId))
                {
                    var resultT1 = await _userManager.AddToRoleAsync(user, "UserT1");
                    if (!resultT1.Succeeded)
                    {
                        throw new Exception($"💥Failed to add user to role: {resultT1}");
                    }
                }
                else if (priceId.Equals(_settings.PremiumPriceId))
                {
                    var resultT2 = await _userManager.AddToRoleAsync(user, "UserT2");
                    if (!resultT2.Succeeded)
                    {
                        throw new Exception($"💥Failed to add user to role: {resultT2}");
                    }
                }
                else {
                    throw new Exception($"💥No match priceId, here is the priceId: {priceId}");
                }
                user.SubscriptionId = subscription.Id;
                user.SubscriptionStatus = subscription.Status;
                user.PriceId = priceId;
                user.SubscriptionEndPeriod = subscription.CurrentPeriodEnd;
                user.SubscriptionStartPeriod = subscription.CurrentPeriodStart;
                var updateUserResult = await _userManager.UpdateAsync(user);
                if (!updateUserResult.Succeeded)
                {
                    throw new Exception("💥Fail to update User when paided successfully!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            
        }
    
        private async Task SubscriptionEnd(Subscription subscription)
        {
            try
            {
                var customerId = subscription.CustomerId;
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.CustomerId == customerId);
                if (user == null)
                {
                    throw new Exception("💥Cannot find user to cancel subscription!");
                }
                if(user.SubscriptionId == null || user.SubscriptionEndPeriod == null) { return; }
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Count == 0)
                {
                    throw new Exception("💥User don't have any role to delete!");
                }
                if (roles.Contains("UserT1"))
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, "UserT1");
                    if (!result.Succeeded)
                    {
                        throw new Exception("💥Fail to remove T1 user when subscription ended!");
                    }
                }
                if (roles.Contains("UserT2"))
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, "UserT2");
                    if (!result.Succeeded)
                    {
                        throw new Exception("💥Fail to remove T2 user when subscription ended!");
                    }
                }
                user.SubscriptionEndPeriod = null;
                user.PriceId = null;
                user.SubscriptionStatus = null;
                user.SubscriptionStartPeriod = null;
                user.SubscriptionId = null;
                var updateUserResult = await _userManager.UpdateAsync(user);
                if (!updateUserResult.Succeeded)
                {
                    throw new Exception("💥Fail to update User when subscription ended!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("💥Fail to Delete subscription info: ",ex);
                throw;
            }
        
        }
        private async Task SubscriptionUpdate(Subscription subscription)
        {
            try
            {
                var customerId = subscription.CustomerId;
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.CustomerId == customerId);
                if (user == null)
                {
                    throw new Exception("💥Fail to find the user to upadate");
                }
                var isDiff = false;
                if (user.SubscriptionStatus != subscription.Status)
                {
                    user.SubscriptionStatus = subscription.Status;
                    isDiff = true;
                }
                if (user.SubscriptionId != subscription.Id)
                {
                    user.SubscriptionId = subscription.Id;
                    isDiff = true;
                }
                if (user.PriceId != subscription.Items.Data[0].Price.Id)
                { 
                    user.PriceId = subscription.Items.Data[0].Price.Id;
                    isDiff = true;
                }
                if(user.SubscriptionEndPeriod != subscription.CurrentPeriodEnd)
                {
                    user.SubscriptionEndPeriod = subscription.CurrentPeriodEnd;
                    isDiff = true;
                }
                if(user.SubscriptionStartPeriod != subscription.CurrentPeriodStart)
                {
                    user.SubscriptionStartPeriod = subscription.CurrentPeriodStart;
                    isDiff = true;
                }
                if (isDiff)
                {
                    var UpdateUserResult = await _userManager.UpdateAsync(user);
                    if (!UpdateUserResult.Succeeded)
                    {
                        throw new Exception("💥Fail to update user when subscription updated");
                    }
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            
        }
    }
}
