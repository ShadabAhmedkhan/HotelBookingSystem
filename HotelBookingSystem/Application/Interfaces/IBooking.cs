using HotelBookingSystem.Application.DTOs;
using HotelBookingSystem.Persistence.Contexts;

namespace HotelBookingSystem.Application.Interfaces
{
    public interface IBooking
    {
        Task<Booking> Add(Booking entity);
        Task<bool> IsRoomAvailable(BookingDTO bookingDTO);
        Task Save();
    }
}
