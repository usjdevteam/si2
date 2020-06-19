using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using si2.bll.Dtos.Requests.Account;
using si2.bll.Helpers;
using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
      
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto model, CancellationToken ct)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
                return BadRequest("user email already in use");

            user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstNameAr = model.FirstNameAr,
                LastNameAr = model.LastNameAr,
                FirstNameFr = model.FirstNameFr,
                LastNameFr = model.LastNameFr
            };

            var password = StaticHelpers.GenerateRandomPassword();

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                var token1 = await _userManager.GeneratePasswordResetTokenAsync(user);
                var token2 = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("ConfirmEmail", "Account", new { code1 = token1, code2 = token2, email = user.Email }, Request.Scheme);

                return Created("", new object[] { confirmationLink, user });
            }

            //send Confirmation Email to the user---------------------------------------
            //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //var confirmationLink = Url.Action(nameof(model.Email), "Account", new { token, email = user.Email }, Request.Scheme);
            //var message = new Message(new string[] { user.Email }, "Confirmation email link", confirmationLink, null);
            //await _emailSender.SendEmailAsync(message);
            //_logger.Log(LogLevel.Warning, "the token is" + token);
            //---------------------------------------------------------------------------

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout(CancellationToken ct)
        {
            //await _signInManager.SignOutAsync();

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync();
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");

            //return RedirectToAction("AccessDenied", "Error");

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto, CancellationToken ct)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);
                if (user == null)
                {
                    return NotFound();
                }
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequestDto.Password, false);

                //check if the user's email is confirmed-----
                var resultConfirm = await _userManager.IsEmailConfirmedAsync(user);
                //-------------------------------------------

                //if (result.Succeeded)
                if (result.Succeeded && resultConfirm == true)
                {
                    var userClaims = await _userManager.GetClaimsAsync(user);
                    var userRoles = await _userManager.GetRolesAsync(user);
                    return Token(user, userClaims, userRoles);
                }
                else
                {
                    return BadRequest(result);
                }
            }

            return BadRequest();
        }

        private IActionResult Token(ApplicationUser user, IList<Claim> userClaims, IList<string> userRoles)
        {
            var configKey = _configuration.GetSection("Si2JwtBearerConstants").GetSection("Key").Value;
            var configIssuer = _configuration.GetSection("Si2JwtBearerConstants").GetSection("Issuer").Value;
            var configAudience = _configuration.GetSection("Si2JwtBearerConstants").GetSection("Audience").Value;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            claims.AddRange(userClaims);

            foreach (string role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                configIssuer,
                configAudience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            var results = new
            {
                roles = userRoles,
                email = user.Email,
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };

            return new CreatedResult("", results);
        }

        /*[HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword([FromBody] ResetRequestDto model)
        {
            /*if (!ModelState.IsValid)
            {
                return View(model);
            }*/
        /*var user = await _userManager.FindByNameAsync(model.Email);
        if (user == null)
        {
            // Don't reveal that the user does not exist
        }
        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        if (result.Succeeded)
        {
            return Ok();
        }
        //AddErrors(result);
        //return View();
        return BadRequest(result.Errors);
    }*/

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto forgotPasswordRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(forgotPasswordRequestDto.Email);

            if (user == null)
            {
                return Ok("Ok");
            }


            if (user.Email == null)
            {
                throw new InvalidOperationException("Cannot send email. Email address not configured.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            //redirect to the reset password action
            /*System.Diagnostics.Process.Start(
                string.Format(
                    "http://localhost:44301/api/account/forgotPasswordReset/{0}/{1}",
                    HttpUtility.UrlEncode(user.UserName),
                    HttpUtility.UrlEncode(token)
                )
            );*/

            var passwordResetLink = Url.Action("ForgotPasswordReset", "Account", new { code1 = token, email = forgotPasswordRequestDto.Email }, Request.Scheme);

            return Ok(passwordResetLink);
        }

        [HttpPost]
        [Route("ForgotPasswordReset")]
        public async Task<ActionResult> ForgotPasswordReset([FromQuery] string code1, [FromQuery] string email, [FromBody] ResetPasswordRequestDto resetRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(resetRequestDto.Email);
            if (user == null)
            {
                return Ok("Ok");
            }

            var result = await _userManager.ResetPasswordAsync(user, code1, resetRequestDto.Password);

            //update EmailConfirmed column------------------------
            //Can't be done here
            //await _userManager.ConfirmEmailAsync(user,model.Token);
            //user.EmailConfirmed = true;
            //await _userManager.UpdateAsync(user);
            //-----------------------------------------------------

            if (result.Succeeded)
            {
                return Ok("Email Reset Successfully");
            }

            //throw new InvalidOperationException(string.Join("\r\n", result.Errors));
            return BadRequest(result.Errors);
        }

        [Route("ConfirmEmail")]
        [HttpPost]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string code1,
          [FromQuery] string code2, [FromQuery] string email, [FromBody] ResetPasswordRequestDto resetRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest();

            var result = await _userManager.ConfirmEmailAsync(user, code2);

            //return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
            if (result.Succeeded)
            {
                result = await _userManager.ResetPasswordAsync(user, code1, resetRequestDto.Password);
                if (result.Succeeded)
                    return Ok("Email Confirmed and updated Successfully");
            }

            return BadRequest(result.Errors);
        }
    }
}
