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
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.WebUtilities;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace MovieWebSite.Server.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(SignInManager<ApplicationUser> sM, UserManager<ApplicationUser> uM, ITokenService tokenService, IEmailService emailService) : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signInManager = sM;
        private readonly UserManager<ApplicationUser> userManager = uM;
        private readonly ITokenService _tokenService= tokenService;
        private readonly IEmailService _emailService = emailService;

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] ApplicationUser user)
        {
            try
            {
                var userExist = await userManager.FindByEmailAsync(user.Email);
                if (userExist != null) {
                    return BadRequest(new { message = "This user already exist!" });
                }
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
                var token =await userManager.GenerateEmailConfirmationTokenAsync(new_user);
                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                var confirmLink = Url.Action(nameof(ConfirmEmail), "Authentication", new {token, email = new_user.Email}, Request.Scheme);
                var emailSubject = "Confirm your email with Sodoki.";
                var emailBody = $"You can confirm your account by <a href='{HtmlEncoder.Default.Encode(confirmLink)}'>clicking here</a>. Hope you have nice day :)";
                var emailComponent = new EmailComponent
                {
                    To = new_user.Email,
                    Subject = emailSubject,
                    Body = emailBody
                };
                await _emailService.SendEmailAsync(emailComponent);
                return Ok();
            }
            catch (Exception ex) {
                return BadRequest(new { message =  ex.Message } );
            }
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return NotFound("User don't exist to confirm email!");
                }
                token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
                var result = await userManager.ConfirmEmailAsync(user, token);
                if (!result.Succeeded) {
                    await userManager.DeleteAsync(user);
                    return BadRequest(new { message = "Failed to confirm eamil!", errors = result.Errors });
                }
                return Ok("Confirm email succsessfully. Pls close this page");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Something have gone wrong with confirm Email" });
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
                        return Unauthorized(new {error="You need to confirm email"});
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

