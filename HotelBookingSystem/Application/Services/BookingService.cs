using HotelBookingSystem.Application.DTOs;
using HotelBookingSystem.Application.Interfaces;
using HotelBookingSystem.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Application.Services
{
    public class BookingService : IBooking
    {
        private readonly HotelBookingDbContext _context;
        private readonly DbSet<Booking> _dbSet;
        public BookingService(HotelBookingDbContext context) 
        {
            _context = context;
            _dbSet = context.Set<Booking>();
        }
        public async Task<Booking> Add(Booking entity)
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

        public async Task<bool> IsRoomAvailable(BookingDTO bookingDTO)
        {
            return !await _dbSet
                    .AnyAsync(b => b.RoomId == bookingDTO.RoomId &&
                                   ((b.CheckInDate < bookingDTO.CheckOutDate) && (b.CheckOutDate > bookingDTO.CheckInDate)));
        }
        public async Task Save() => await _context.SaveChangesAsync();

    }
}
