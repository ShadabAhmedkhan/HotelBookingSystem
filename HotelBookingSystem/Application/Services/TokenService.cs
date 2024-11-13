using HotelBookingSystem.Persistence.Contexts;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelBookingSystem.Application.Services
{
    public class TokenService
    {
        public string Generate(User user)
        {
            var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role)
        };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SHADAB_AHMED_@!_KEY_!@_SHADAB_AHMED"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var now = DateTime.Now;

            var tokenDescriptor = new JwtSecurityToken(
                "issuer",
                "audience",
                claims,
                now,
                now.AddMinutes(1), // expiry add for testing purpuse
                credentials);

            var token = tokenHandler.WriteToken(tokenDescriptor);
            return token;
        }
        public string GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                throw new ArgumentException("Invalid JWT token");

            // Replace the claim type with the one used in your token (e.g., "nameidentifier" or other custom claim)
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            return userIdClaim ?? throw new ArgumentException("User ID not found in token");
        }
    }
}
