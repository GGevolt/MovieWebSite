using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Server.Utility.Interfaces;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.WebUtilities;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using Server.Model.DTO;
using Server.Model.AuthModels;
using MovieWebSite.Server.Data;
using System.Diagnostics;

namespace MovieWebSite.Server.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public AuthenticationController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ITokenService tokenService, IEmailService emailService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO user)
        {
            try
            {
                var emailExist = await userManager.FindByEmailAsync(user.Email);
                var userNameExist = await userManager.FindByNameAsync(user.UserName);
                if (emailExist != null && userNameExist != null)
                {
                    return BadRequest(new { message = "User with this email and username already exists!" });
                }
                else if (emailExist != null)
                {
                    return BadRequest(new { message = "This Email address is already associated with another account." });
                }
                else if (userNameExist != null)
                {
                    return BadRequest(new { message = "This Username is already taken. Please choose a different username." });
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
                    return BadRequest(new { message = "💥Failed to create account", errors = createUserResult.Errors });
                }
                var roleResult = await userManager.AddToRoleAsync(new_user, "UserT0");
                if (!roleResult.Succeeded)
                {
                    await userManager.DeleteAsync(new_user);
                    return BadRequest(new { message = "💥Failed to assign role!", errors = roleResult.Errors });
                }
                var token = await userManager.GenerateEmailConfirmationTokenAsync(new_user);
                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                var urlComfirmEmail = $"{user.EmailConfirnUrl}/{token}/{new_user.Email}";
                //var confirmLink = Url.Action(nameof(ConfirmEmail), "Authentication", new {token, email = new_user.Email}, Request.Scheme);
                var emailSubject = "Confirm your email with Sodoki.";
                var emailBody = $"You can confirm your account by <a href='{HtmlEncoder.Default.Encode(urlComfirmEmail)}'>clicking here</a>. Hope you have nice day :)";
                var emailComponent = new EmailComponent
                {
                    To = new_user.Email,
                    Subject = emailSubject,
                    Body = emailBody
                };
                await _emailService.SendEmailAsync(emailComponent);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromForm] ConfirmDTO confirmDTO)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(confirmDTO.Email);
                if (user == null)
                {
                    return NotFound(new { message = "💥User don't exist to confirm email!", email = confirmDTO.Email });
                }
                var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(confirmDTO.Token));
                var result = await userManager.ConfirmEmailAsync(user, token);
                if (!result.Succeeded)
                {
                    await userManager.DeleteAsync(user);
                    return BadRequest(new { message = "💥Failed to confirm email!", errors = result.Errors });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "💥Something have gone wrong with confirm Email" });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromForm] LoginDTO loginDTO)
        {
            try
            {
                ApplicationUser login_user = await userManager.FindByEmailAsync(loginDTO.Email);
                if (login_user != null)
                {
                    if (!login_user.EmailConfirmed)
                    {
                        return Unauthorized(new { error = "💥You need to confirm email" });
                    }
                    var result = await signInManager.PasswordSignInAsync(login_user, loginDTO.Password, loginDTO.RememberMe, lockoutOnFailure: false);
                    if (!result.Succeeded)
                    {
                        return Unauthorized(new { error = "💥Invalid Password! Try again.", statusCode = 401 });
                    }

                    login_user.LastLogin = DateTime.Now;
                    var updateResult = await userManager.UpdateAsync(login_user);
                    //string tokenValue = _tokenService.GenarateToken(login_user);
                    var roles = await userManager.GetRolesAsync(login_user);
                    return Ok(new { UserRoles = roles, UserName = login_user.UserName });
                }
                else
                {
                    return BadRequest(new { error = "💥Invalid account! Try again.", statusCode = 400 });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"💥Something went wrong: {ex.Message}", statusCode = 500 });
            }
        }



        [HttpGet("logout"), Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await signInManager.SignOutAsync();
                return Ok(new { Message = "💥Logged out successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest($"💥Some thing went wrong: {ex.Message}");
            }
        }

        [HttpGet("info"), Authorize]
        public async Task<IActionResult> GetUserInfo()
        {

            try
            {
                var currentUser = await signInManager.UserManager.GetUserAsync(User);

                if (currentUser == null)
                {
                    return Unauthorized(new { error = "💥User not authenticated" });
                }

                return Ok(new { userInfo = currentUser });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "💥Something has gone wrong!" });
            }
        }

        [HttpGet("validate")]
        public IActionResult ValidateCurrentUser()
        {
            try
            {
                ClaimsPrincipal principals = HttpContext.User as ClaimsPrincipal;
                var result = signInManager.IsSignedIn(principals);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return Forbid("💥Access Denied");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"💥Something has gone wrong! {ex.Message}");
            }
        }
    }
}

