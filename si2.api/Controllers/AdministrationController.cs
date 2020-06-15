using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using si2.bll.Dtos.Requests.Account;
using si2.bll.Dtos.Requests.Administration;
using si2.bll.Dtos.Requests.Cohort;
using si2.bll.Dtos.Results.Administration;
using si2.bll.Models;
using si2.bll.Services;
using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace si2.api.Controllers
{
    [ApiController]
    [Route("api/administration")]
    public class AdministrationController : ControllerBase
    {
        private readonly ILogger<AdministrationController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserCohortService _userCohortService;

        public AdministrationController(ILogger<AdministrationController> logger,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUserCohortService userCohortService)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _userCohortService = userCohortService;
        }



        [HttpGet("users")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetUsers(CancellationToken ct)
        {
            var Users = _userManager.Users;

            if (Users == null)
                return NotFound();

            return Ok(Users);
        }

        [HttpPost("roles")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto model, CancellationToken ct)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole() { Name = model.RoleName });

            if (result.Succeeded)
            {
                var roleToReturn = _roleManager.Roles.FirstOrDefault(c => c.Name == model.RoleName);
                return CreatedAtRoute("GetRole", new { id = roleToReturn.Id }, roleToReturn);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("roles/{id}", Name = "GetRole")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetRoleById([FromRoute]string id, CancellationToken ct)
        {
            var Role = await _roleManager.FindByIdAsync(id);

            if (Role == null)
                return NotFound();

            return Ok(Role);
        }

        [HttpGet("roles")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetRoles(CancellationToken ct)
        {
            var Roles = _roleManager.Roles;

            if (Roles == null)
                return NotFound();

            return Ok(Roles);
        }


        [HttpPost("users/{userId}/claims")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ManageUserClaims([FromRoute]string userId, [FromBody] UserClaimsDto model, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var existingUserClaims = await _userManager.GetClaimsAsync(user);

            var result = await _userManager.RemoveClaimsAsync(user, existingUserClaims);

            if (!result.Succeeded)
                return BadRequest(); // TODO Bad request is not the best returned error 

            //var claims = model.Claims.Select(c => new Claim(c.ClaimType, c.IsSelected ? "true" : "false")).ToList();
            var claims = model.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();

            //foreach (Claim claim in ClaimsStore.AllClaims)
            foreach (UserClaimDto claim in model.Claims)
            {
                if (!claims.Any(c => string.Equals(c.Type, claim.ClaimType, StringComparison.OrdinalIgnoreCase)) || 
                    !claims.Any(c => string.Equals(c.Value, claim.ClaimValue, StringComparison.OrdinalIgnoreCase)))
                    claims.Add(new Claim(claim.ClaimType, claim.ClaimValue));
            }

            result = await _userManager.AddClaimsAsync(user, claims);
            if (!result.Succeeded)
                return BadRequest(); // TODO Bad request is not the best returned error 

            return CreatedAtRoute("GetUserClaims", new { userId = userId }, model);
        }

        [HttpGet("users/{userId}/claims", Name = "GetUserClaims")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetUserClaims([FromRoute]string userId, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var existingUserClaims = await _userManager.GetClaimsAsync(user);

            var claimsDto = new List<UserClaimDto>(existingUserClaims.Select(claim => new UserClaimDto()
            {
                ClaimType = claim.Type,
                IsSelected = string.Equals(claim.Value, "true", StringComparison.OrdinalIgnoreCase) ? true : false,
                ClaimValue = claim.Value
            }));

            //foreach (Claim claim in ClaimsStore.AllClaims)
            foreach (Claim claim in existingUserClaims)
            {
                if (!existingUserClaims.Any(c => string.Equals(c.Type, claim.Type, StringComparison.OrdinalIgnoreCase)))
                {
                    claimsDto.Add(new UserClaimDto()
                    {
                        ClaimType = claim.Type,
                        IsSelected = string.Equals(claim.Value, "true", StringComparison.OrdinalIgnoreCase) ? true : false,
                        ClaimValue = claim.Value
                    });
                }
            }

            var result = new UserClaimsDto()
            {
                Claims = claimsDto.ToList()
            };

            return Ok(result);
        }

        [HttpGet("users/{userId}/roles")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetRolesForUser([FromRoute]string userId, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(roles);
        }

        [HttpPost("users/{userId}/roles")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddRolesToUser([FromRoute]string userId, [FromBody]RolesDto addRoles, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles.ToArray());
            await _userManager.AddToRolesAsync(user, addRoles.Roles.ToArray());

            var finalRoles = await _userManager.GetRolesAsync(user);

            return Ok(finalRoles);
        }

        [HttpPut("users/{userId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateUser([FromRoute] string userId,[FromBody] RegisterRequestDto model,CancellationToken ct)
        {
            //var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var user = await _userManager.FindByIdAsync(userId);

            user.Email = model.Email;

            user.FirstNameFr = model.FirstNameFr;
            user.LastNameFr = model.LastNameFr;

            user.FirstNameAr = model.FirstNameAr;
            user.LastNameAr = model.LastNameAr;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }

        }


        [HttpDelete("users/{userId}/roles")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> RevokeRolesFromUser([FromRoute]string userId, [FromBody]RolesDto removeRoles, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, removeRoles.Roles.ToArray());

            var finalRoles = await _userManager.GetRolesAsync(user);

            return Ok(finalRoles);
        }


        [HttpDelete("users/{userId}/claims")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> RevokeClaimsFromUser([FromRoute]string userId, [FromBody] UserClaimsDto removeClaims, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            //var claims = model.Claims.Select(c => new Claim(c.ClaimType, c.IsSelected ? "true" : "false")).ToList();
            var claims = removeClaims.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();

            //foreach (Claim claim in ClaimsStore.AllClaims)
            foreach (UserClaimDto claim in removeClaims.Claims)
            {
                if (!claims.Any(c => string.Equals(c.Type, claim.ClaimType, StringComparison.OrdinalIgnoreCase)))
                    claims.Add(new Claim(claim.ClaimType, claim.ClaimValue));
            }

            var result = await _userManager.RemoveClaimsAsync(user, claims);
            if (!result.Succeeded)
                return BadRequest(); // TODO Bad request is not the best returned error 

            var finalClaims = await _userManager.GetClaimsAsync(user);

            return Ok(finalClaims);

        }

        /*[Route("api/users/{userId}/cohorts")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookDto))]
        public async Task<ActionResult> AddCohortsToStudent(Guid userId, [FromBody] JArray cohorts, CancellationToken ct)
        {
            foreach (JObject cohortToAdd in cohorts)
            {
                var userToReturn = await _userCohortService.AssignUsersToCohortAsync(userId, new Guid(cohortToAdd.GetValue("cohortId").ToString()), ct);
                if (userToReturn == null)
                    return BadRequest();
            }

            return Ok();
        }*/

        [HttpPost]
        [Route("users/{userId}/cohorts")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> UpdateCohortsUser([FromRoute]String userId, [FromBody] ManageCohortsUserDto manageCohortsToUserDto, CancellationToken ct)
        {
            if (!await _userCohortService.ExistsAsync(userId, ct))
                return NotFound();

            var userCohortToReturn = await _userCohortService.AssignCohortsToUserAsync(userId, manageCohortsToUserDto, ct);

            return Ok();

            //return CreatedAtRoute("GetCohortsOfUser", userId, userCohortToReturn);

        }

        [HttpGet("{id}")]
        [Route("users/{userId}/cohorts", Name = "GetCohortsOfUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> GetCohortsOfUser([FromRoute]String userId, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var cohortDtos = await _userCohortService.GetCohortsUserAsync(userId, ct);
            if (cohortDtos == null)
                return NotFound();
            return Ok(cohortDtos);

        }

        /*[HttpPut]
        [Route("users/{userId}/cohorts", Name = "UpdateCohortsUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> UpdateCohortsUser([FromRoute]String userId, [FromBody] AddCohortsToUserDto addCohortsToUserDto, CancellationToken ct)
        {
            if (!await _userCohortService.ExistsAsync(userId, ct))
                return NotFound();

            //delete cohorts from the user
            await _userCohortService.DeleteCohortsUser(userId, ct);
            //-----------------------------

            await _userCohortService.AssignCohortsToUserAsync(userId, addCohortsToUserDto, ct);

            return Ok();

        }*/
    }
}
