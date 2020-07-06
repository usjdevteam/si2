﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using si2.bll.Dtos.Requests.Account;
using si2.bll.Helpers;
using si2.bll.Services;
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
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger, IConfiguration configuration, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
            _emailSender = emailSender;
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

                //send email------------------------------------------------
                var email = model.Email;
                //var email = "marie.kassis@usj.edu.lb";
                var subject = "SI-Prototype - Register";
                var message = "Please click on this link to confirm your email and reset your password: " + confirmationLink;
                await _emailSender.SendEmailAsync(email, subject, message);
                //-----------------------------------------------------------

                return Created("", new object[] { confirmationLink, user });
            }

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

            var passwordResetLink = Url.Action("ForgotPasswordReset", "Account", new { code1 = token, email = forgotPasswordRequestDto.Email }, Request.Scheme);

            //send email------------------------------------------------
            var email = forgotPasswordRequestDto.Email;
            //var email = "marie.kassis@usj.edu.lb";
            var subject = "SI-Prototype - Forgot Password";
            var message = "Please click on this link to reset your password: " + passwordResetLink;
            await _emailSender.SendEmailAsync(email, subject, message);
            //-----------------------------------------------------------

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

            if (result.Succeeded)
            {
                return Ok("Email Reset Successfully");
            }

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
