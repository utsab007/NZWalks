using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }
        // POST: api/auth/register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterReqDto registerReqDto)
        {
            var identityUser = new IdentityUser()
            {
                UserName = registerReqDto.UserName,
                Email = registerReqDto.UserName
            };
            var identityResult = await _userManager.CreateAsync(identityUser, registerReqDto.Password);

            if(identityResult.Succeeded)
            {
                // add roles to the user
                if(registerReqDto.Roles != null && registerReqDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerReqDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login");
                    }
                }
            }

            return BadRequest("Something went wrong");
        }

        // POST: api/auth/login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginReqDto loginReqDto)
        {
            var identityUser = await _userManager.FindByEmailAsync(loginReqDto.UserName);
            if (identityUser != null && await _userManager.CheckPasswordAsync(identityUser, loginReqDto.Password))
            {
                // get roles for this user
                var roles = await _userManager.GetRolesAsync(identityUser);

                if (roles != null)
                {
                    // generate jwt token

                    var jwtToken = _tokenRepository.GenerateJwtTokenAsync(identityUser, roles.ToList());

                    var response = new LoginResDto()
                    {
                        JwtToken = jwtToken
                    };
                    return Ok(response);
                }
            }
            return BadRequest("User name or password was incorrect");
        }
    }
}
