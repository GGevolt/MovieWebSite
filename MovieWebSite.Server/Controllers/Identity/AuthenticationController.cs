using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Model.Models;
using Server.Model.ViewModels;

namespace MovieWebSite.Server.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(SignInManager<ApplicationUser> sM, UserManager<ApplicationUser> uM) : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signInManager = sM;
        private readonly UserManager<ApplicationUser> userManager = uM;
        [HttpPost("register")]
        public async Task<ActionResult> Register(ApplicationUser user)
        {
            IdentityResult result = new IdentityResult();
            try
            {
                ApplicationUser new_user = new ApplicationUser() { 
                    FirstName =user.FirstName,
                    LastName =user.LastName,
                    Email =user.Email,
                    Age = user.Age,
                };
                result = await userManager.CreateAsync(new_user, user.PasswordHash);
                if (!result.Succeeded) { 
                    return BadRequest(result);
                }
            }
            catch (Exception ex) { 
                return BadRequest($"Some thing went wrong: {ex.Message}");
            }
            return Ok();
        }
        [HttpPost("login")]
        public async Task<ActionResult> login(LoginVM loginVM)
        {
            try
            {
                ApplicationUser login_user = await userManager.FindByEmailAsync(loginVM.Email);
                if (login_user != null && !login_user.EmailConfirmed) { 
                    return BadRequest("Need to confirmed email!");
                }
                var result = await signInManager.PasswordSignInAsync(login_user, loginVM.Password, loginVM.RememberMe, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    return Unauthorized("Invalid login! Try again.");
                }
                login_user.LastLogin = DateTime.Now;
                var updateResult = await userManager.UpdateAsync(login_user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Some thing went wrong: {ex.Message}");
            }
            return Ok();
        }
        [HttpGet("logout"), Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await signInManager.SignOutAsync();
            }
            catch (Exception ex) {
                return BadRequest($"Some thing went wrong: {ex.Message}");
            }
            return Ok();
        }
    }
}

