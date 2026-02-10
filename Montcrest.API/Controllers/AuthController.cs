using Microsoft.AspNetCore.Mvc;
using Montcrest.API.DTOs.Auth;
using Montcrest.BLL.Helpers;
using Montcrest.BLL.Interfaces;
using Montcrest.DAL.Models;

namespace Montcrest.API.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly JwtHelper _jwtHelper;

        public AuthController(IAuthService authService, JwtHelper jwtHelper)
        {
            _authService = authService;
            _jwtHelper = jwtHelper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto)
        {
            var user = new User
            {
                FullName = registerRequestDto.FullName,
                Email = registerRequestDto.Email,
                MobileNumber = registerRequestDto.MobileNumber,
                Address = registerRequestDto.Address,
            };

            await _authService.RegisterAsync(user, registerRequestDto.Password);

            return Ok(new { message = "Registration successful" });
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var user= await _authService.ValidateUserAsync(loginRequestDto.Email, loginRequestDto.Password);

            if(user == null)
            {
                return Unauthorized("Invalid Email or Password");
            }

            var token= _jwtHelper.GenerateToken(user);

            return Ok(new LoginResponseDto
            {
                Token = token,
                Role = user.Role.ToString(),
                FullName = user.FullName
            });
        }
        
    }
}
