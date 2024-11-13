namespace HotelBookingSystem.Persistence.Contexts
{
    public class Room
    {
        public int Id { get; set; }                        // Primary key
        public int HotelId { get; set; }                   // Foreign key to the associated Hotel
        public string RoomNumber { get; set; }             // Room number within the hotel
        public RoomType Type { get; set; }                 // Type of the room (e.g., Single, Double, Suite)
        public int Capacity { get; set; }                  // Maximum occupancy for the room
        public decimal PricePerNight { get; set; }         // Price per night for the room
        public bool IsAvailable { get; set; }              // Availability status
        public Hotel Hotel { get; set; }                   // Reference to the associated Hotel

    }
}
