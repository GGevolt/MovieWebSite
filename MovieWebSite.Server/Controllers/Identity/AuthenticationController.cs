using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.Model.Models;
using Server.Model.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Server.Utility.Interfaces;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace MovieWebSite.Server.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(SignInManager<ApplicationUser> sM, UserManager<ApplicationUser> uM, ITokenService tokenService) : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signInManager = sM;
        private readonly UserManager<ApplicationUser> userManager = uM;
        private readonly ITokenService _tokenService= tokenService;

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] ApplicationUser user)
        {
            try
            {
                ApplicationUser new_user = new ApplicationUser()
                {
                    FullName = user.FullName,
                    Gender = user.Gender,
                    Email = user.Email,
                    UserName = user.UserName,
                    Dob = user.Dob,
                };
                var createUserResult = await userManager.CreateAsync(new_user, user.PasswordHash);
                if (!createUserResult.Succeeded)
                {
                    return BadRequest(new { message = "Failed to create account", errors = createUserResult.Errors });
                }
                var roleResult = await userManager.AddToRoleAsync(new_user, "UserT0");
                if (!roleResult.Succeeded)
                {
                    await userManager.DeleteAsync(new_user);
                    return BadRequest(new { message = "Failed to assign role!", errors = roleResult.Errors });
                }
                return Ok();
            }
            catch (Exception ex) {
                return BadRequest(new { message =  ex.Message } );
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromForm] LoginVM loginVM)
        {
            try
            {
                ApplicationUser login_user = await userManager.FindByEmailAsync(loginVM.Email);
                if (login_user != null ) {
                    if (!login_user.EmailConfirmed)
                    {
                        login_user.EmailConfirmed = true;
                    }
                    var result = await signInManager.PasswordSignInAsync(login_user, loginVM.Password, loginVM.RememberMe, lockoutOnFailure: false);
                    if (!result.Succeeded)
                    {
                        return Unauthorized(new { error = "Invalid Password! Try again.", statusCode = 401 });
                    }
                 
                    login_user.LastLogin = DateTime.Now;
                    var updateResult = await userManager.UpdateAsync(login_user);
                    //string tokenValue = _tokenService.GenarateToken(login_user);
                    var roles = await userManager.GetRolesAsync(login_user);
                    return Ok(new {  UserRoles = roles });
                }
                else
                {
                    return BadRequest(new { error = "Invalid account! Try again.", statusCode = 400 });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"Something went wrong: {ex.Message}", statusCode = 500 });
            }
        }



        [HttpGet("logout"), Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await signInManager.SignOutAsync();
                return Ok(new { Message = "Logged out successfully" });
            }
            catch (Exception ex) {
                return BadRequest($"Some thing went wrong: {ex.Message}");
            }
        }

        [HttpGet("info"), Authorize]
        public async Task<IActionResult> getUserInfo()
        {

            try
            {
                var currentUser = await signInManager.UserManager.GetUserAsync(User);

                if (currentUser == null)
                {
                    return Unauthorized(new { error = "User not authenticated" });
                }

                return Ok(new { userInfo = currentUser });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Something has gone wrong!" });
            }
        }

        [HttpGet("validate")]
        public IActionResult ValidateCurrentUser()
        {
            try
            {
                var user_ = HttpContext.User;
                var principals = new ClaimsPrincipal(user_);
                var result = signInManager.IsSignedIn(principals);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return Forbid("Access Denied");
                }
            }
            catch(Exception ex)
            {
                return BadRequest($"Something has gone wrong! {ex.Message}");
            }
        }
        
    }
}

