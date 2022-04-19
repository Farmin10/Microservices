using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Dtos;
using IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var user = new ApplicationUser 
            {
                UserName=userForRegisterDto.UserName,
                City=userForRegisterDto.City,
                Email=userForRegisterDto.Email
            };
            var result = await _userManager.CreateAsync(user, userForRegisterDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(ResponseDto<NoContent>.Fail(result.Errors.Select(x=>x.Description).ToList(), 400));
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim==null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if (user==null)
            {
                return BadRequest();
            }
            return Ok(new { user.Id, user.UserName, user.Email, user.City});
        }
    }
}
