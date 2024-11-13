using HotelBookingSystem.Application.DTOs;
using HotelBookingSystem.Persistence.Contexts;

namespace HotelBookingSystem.Application.Converters
{
    public static class BookingConverter
    {
        public static Booking Booking(BookingDTO booking)
        {
            return new Booking
            {
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                //Id = booking.RoomId,
                RoomId = booking.RoomId,
                TotalPrice = booking.TotalPrice,
                UserId = booking.UserId
            };
        }
    }
}
