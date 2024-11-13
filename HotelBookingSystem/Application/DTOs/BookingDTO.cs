using HotelBookingSystem.Persistence.Contexts;

namespace HotelBookingSystem.Application.DTOs
{
    public class BookingDTO
    {
        public string CustomerName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
