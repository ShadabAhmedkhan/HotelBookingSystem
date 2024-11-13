using HotelBookingSystem.Application.DTOs;
using HotelBookingSystem.Persistence.Contexts;

namespace HotelBookingSystem.Application.Interfaces
{
    public interface IUser<UserT> where UserT : class
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> Add(User entity);
        Task<User> Login(LoginDTO loginDTO);
        Task<string> Refresh(string token);

        void Update(User entity);
        void Delete(User entity);
        Task Save();
    }
}
