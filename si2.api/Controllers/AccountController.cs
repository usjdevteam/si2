using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using si2.bll.Dtos.Requests.Account;
using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

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
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }

            //send Confirmation Email to the user---------------------------------------
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(model.Email), "Account", new { token, email = user.Email }, Request.Scheme);

            //var message = new Message(new string[] { user.Email }, "Confirmation email link", confirmationLink, null);
            //await _emailSender.SendEmailAsync(message);
            _logger.Log(LogLevel.Warning, "the token is" + token);
            //---------------------------------------------------------------------------

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout(CancellationToken ct)
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model, CancellationToken ct)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return NotFound();
                }
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                //check if the user's email is confirmed-----
                var resultConfirm = await _userManager.IsEmailConfirmedAsync(user);
                //-------------------------------------------

                if (result.Succeeded)
                //if (result.Succeeded && resultConfirm == true)
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
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(model.Email);

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

            var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);

            //_logger.Log(LogLevel.Warning, passwordResetLink);
            _logger.Log(LogLevel.Warning, token);

            //SendMailForgotPassword(user, token);

            return Ok("Ok");
        }

        [HttpPost]
        [Route("ForgotPasswordReset")]
        public async Task<ActionResult> ForgotPasswordReset([FromBody] ResetRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return Ok("Ok");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            //update EmailConfirmed column------------------------
            //Can't be done here
            //await _userManager.ConfirmEmailAsync(user,model.Token);
            //user.EmailConfirmed = true;
            //await _userManager.UpdateAsync(user);
            //-----------------------------------------------------

            if (result.Succeeded)
            {
                return Ok("Ok");
            }

            //throw new InvalidOperationException(string.Join("\r\n", result.Errors));
            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmRequestDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest();

            var result = await _userManager.ConfirmEmailAsync(user, model.Token);
            //return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
            if (result.Succeeded)
            {
                return Ok(nameof(ConfirmEmail));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
