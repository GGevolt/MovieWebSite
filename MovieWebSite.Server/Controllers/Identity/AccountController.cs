using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Server.Model.AuthModels;
using Server.Model.DTO;
using Stripe;
using System.Diagnostics;

namespace MovieWebSite.Server.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly StripeSettingDTO _settings;
        public AccountController(UserManager<ApplicationUser> userManager, IOptions<StripeSettingDTO> settings, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _settings = settings.Value;
            _roleManager = roleManager;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersAsync()
        {
            try
            {
                var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
                var adminUserIds = adminUsers.Select(u => u.Id).ToHashSet();
                var users = await _userManager.Users
                    .Where(u => !adminUserIds.Contains(u.Id))
                    .Select(u => new
                    {
                        UserId = u.Id,
                        UserName = u.UserName,
                        Email = u.Email,
                        CreatedDate = u.CreatedDate,
                        LastLogin = u.LastLogin,
                        SubscriptionPlan = u.PriceId == null ? "Not Subscribed" :
                                           u.PriceId == _settings.ProPriceId ? "Pro Plan" : "Premium Plan",
                    })
                    .ToListAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"💥Something failed while get Users: {ex}");
                return BadRequest(ex);
            }
        }
        [HttpDelete("{UserId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAccount(string UserId)
        {
            try {
                var user = await _userManager.FindByIdAsync(UserId);
                if (user == null)
                {
                    throw new Exception("💥Fail to find user by Id");
                }
                Debug.WriteLine($"💥Hello!");
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception("💥Fail to delete account");
                }
                return Ok();
            }
            catch(Exception ex) {
                Debug.WriteLine($"💥Something failed while delete User: {ex}");
                return BadRequest(ex);
            }
        }
    }
}
