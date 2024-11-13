using HotelBookingSystem.Application.DTOs;
using HotelBookingSystem.Application.Interfaces;
using HotelBookingSystem.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Application.Services
{
    public class UserService<UserT> : IUser<UserT> where UserT : class
    {
        private readonly HotelBookingDbContext _context;
        private readonly DbSet<User> _dbSet;

        private readonly TokenService _tokenService;
        public UserService(HotelBookingDbContext context, TokenService tokenService)
        {
            _context = context;
            _dbSet = context.Set<User>();
            _tokenService = tokenService;
        }
        //Curently use for Signup
        //public async Task Add(User entity) => await _context.Users.AddAsync(entity);
        public async Task<IEnumerable<User>> GetAll() => await _dbSet.ToListAsync();
        public async Task<User> GetById(int id) => await _dbSet.FindAsync(id);
        public async Task<User> Add(User entity) 
        {
            try
            {
                _dbSet.Add(entity);
                return entity;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Update(User entity) => _dbSet.Update(entity);
        public void Delete(User entity) => _dbSet.Remove(entity);
        public async Task Save() => await _context.SaveChangesAsync();
        //for login
        public async Task<User> Login(LoginDTO loginDTO)
        {
            return await _context.Users
                            .FirstOrDefaultAsync(u => u.Username == loginDTO.Username && u.PasswordHash == loginDTO.Password);
        }
        //for refresh token
        public async Task<string> Refresh(string token)
        {
            var user = await _context.Users.FirstAsync(x =>
            x.Id == int.Parse(_tokenService.GetUserIdFromToken(token)));
            var RefreshToken = _tokenService.Generate(user);
            return RefreshToken.ToString();
        }
    }
}
