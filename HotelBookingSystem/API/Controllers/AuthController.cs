using HotelBookingSystem.Application.Converters;
using HotelBookingSystem.Application.DTOs;
using HotelBookingSystem.Application.Helper.Exceptions;
using HotelBookingSystem.Application.Interfaces;
using HotelBookingSystem.Application.Services;
using HotelBookingSystem.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUser<User> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly TokenService _tokenService;


        public AuthController(IUser<User> userRepository, TokenService tokenService, ILogger<AuthController> logger)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _logger = logger;
        }

        // Login API
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginUser)
        {
            //var user = Users.SingleOrDefault(u => u.Username == loginUser.Username && u.Password == loginUser.Password);
            User user =await _userRepository.Login(loginUser);
            if (user == null)
            {
                throw new BaseException("Unauthorized user.", 401);
            }
            _logger.LogInformation("User");

            // Generate JWT Token
            var token = _tokenService.Generate(user);
            return Ok(new { token, succsess = "Login successfully" });

        }
        [HttpPost("signup")]
        public async Task<IActionResult> Signup(LoginDTO user)
        {
            // Add user to the list (or DB in a real application)
            try
            {
               await _userRepository.Add(UserConverter.User(user));
               await _userRepository.Save();
                return Ok("User registered successfully");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh(string token)
        {
            //var token = await _userRepository.Refresh();
            return Ok(await _userRepository.Refresh(token));
        }
        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
