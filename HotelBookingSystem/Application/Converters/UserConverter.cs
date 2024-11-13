using HotelBookingSystem.Application.DTOs;
using HotelBookingSystem.Persistence.Contexts;

namespace HotelBookingSystem.Application.Converters
{
    public static class UserConverter
    {
        public static User User(LoginDTO userDTO)
        {
            return new User
            {
                Username = userDTO.Username,
                PasswordHash = userDTO.Password,
                Role = "Admin",
                Email = userDTO.Username,
                FullName = userDTO.Username,
                PhoneNumber = userDTO.Username,
                 RoleId=1
            };
        }
    }
}
