namespace HotelBookingSystem.Persistence.Contexts
{
    public class Booking
    {
        public int Id { get; set; }                           // Primary key
        public int UserId { get; set; }                       // Foreign key to the User who made the booking
        public int RoomId { get; set; }                       // Foreign key to the Room being booked
        public DateTime CheckInDate { get; set; }             // Date of check-in
        public DateTime CheckOutDate { get; set; }            // Date of check-out
        public decimal TotalPrice { get; set; }               // Total price for the booking
        public BookingStatus Status { get; set; }             // Status of the booking (e.g., Pending, Confirmed, Cancelled)
        public User User { get; set; }                        // Reference to the User who made the booking
        public Room Room { get; set; }                        // Reference to the Room being booked

    }
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }

    public enum RoomType
    {
        Single,
        Double,
        Suite
    }
}
