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
using Stripe;
using Microsoft.Extensions.Options;

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
        private readonly StripeSettingDTO _settings;

        public AuthenticationController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ITokenService tokenService, IEmailService emailService, IOptions<StripeSettingDTO> settings)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _settings = settings.Value;
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
                    if (emailExist == userNameExist && emailExist.EmailConfirmed == false)
                    {
                        await userManager.DeleteAsync(emailExist);
                    }
                    else
                    {
                        return BadRequest(new { message = "User with this email and username already exists!" });
                    }
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

                UserInfoDTO userInfoDTO = new UserInfoDTO();
                userInfoDTO.Dob = currentUser.Dob;
                userInfoDTO.Email = currentUser.Email;
                userInfoDTO.UserName = currentUser.UserName;
                userInfoDTO.Gender = currentUser.Gender;
                userInfoDTO.FullName = currentUser.FullName;
                userInfoDTO.AccountCreatedDate = currentUser.CreatedDate;
                if (currentUser.SubscriptionEndPeriod != null)
                {
                    if(currentUser.PriceId != null && currentUser.PriceId.Equals(_settings.ProPriceId))
                    {
                        userInfoDTO.SubscriptionPlan = "Pro";
                    }
                    else if (currentUser.PriceId != null && currentUser.PriceId.Equals(_settings.PremiumPriceId))
                    {
                        userInfoDTO.SubscriptionPlan = "Premium";
                    }
                    else
                    {
                        throw new Exception("💥Can't find user plan in get user info!");
                    }
                    userInfoDTO.SubscriptionEndPeriod = currentUser.SubscriptionEndPeriod;

                }
                return Ok(new { userInfo = userInfoDTO });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "💥Something has gone wrong!" });
            }
        }

        [HttpGet("validate")]
        public async Task<IActionResult> ValidateCurrentUser()
        {
            try
            {
                ClaimsPrincipal principals = HttpContext.User as ClaimsPrincipal;
                if (principals == null)
                {
                    return Unauthorized("User not authenticated");
                }

                var result = signInManager.IsSignedIn(principals);
                if (!result)
                {
                    return Forbid("Access Denied");
                }

                var userId = principals.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("User ID not found in claims");
                }

                var user = await userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                var userRoles = await userManager.GetRolesAsync(user);
                if (userRoles.Count == 0)
                {
                    return BadRequest("User doesn't have any roles");
                }

                if (user.SubscriptionEndPeriod != null)
                {
                    if (user.SubscriptionStatus == "paused" || user.SubscriptionStatus == "canceled" || user.SubscriptionStatus == "past_due")
                    {
                        foreach (var role in userRoles.ToList())
                        {
                            if (role == "UserT1" || role == "UserT2")
                            {
                                var removeResult = await userManager.RemoveFromRoleAsync(user, role);
                                if (!removeResult.Succeeded)
                                {
                                    return BadRequest($"Failed to remove {role} role: {string.Join(", ", removeResult.Errors.Select(e => e.Description))}");
                                }
                            }
                        }

                        user.SubscriptionEndPeriod = null;
                        user.SubscriptionStatus = null;
                        user.PriceId = null;
                        var updateUserResult = await userManager.UpdateAsync(user);
                        if (!updateUserResult.Succeeded)
                        {
                            return BadRequest($"Failed to update user: {string.Join(", ", updateUserResult.Errors.Select(e => e.Description))}");
                        }

                        var updatedUserRoles = await userManager.GetRolesAsync(user);
                        return Ok(new { roles = updatedUserRoles });
                    }

                    if (user.PriceId != null)
                    {
                        string targetRole = user.PriceId.Equals(_settings.ProPriceId) ? "UserT1" : "UserT2";
                        string removeRole = targetRole == "UserT1" ? "UserT2" : "UserT1";

                        if (userRoles.Contains(removeRole))
                        {
                            var removeResult = await userManager.RemoveFromRoleAsync(user, removeRole);
                            if (!removeResult.Succeeded)
                            {
                                return BadRequest($"Failed to remove {removeRole} role: {string.Join(", ", removeResult.Errors.Select(e => e.Description))}");
                            }
                        }

                        if (!userRoles.Contains(targetRole))
                        {
                            var addResult = await userManager.AddToRoleAsync(user, targetRole);
                            if (!addResult.Succeeded)
                            {
                                return BadRequest($"Failed to add {targetRole} role: {string.Join(", ", addResult.Errors.Select(e => e.Description))}");
                            }
                        }

                        var updatedRoles = await userManager.GetRolesAsync(user);
                        return Ok(new { roles = updatedRoles });
                    }

                    return Ok(new { roles = userRoles });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Something has gone wrong with validate user: {ex.Message}");
            }
        }
    }
}

