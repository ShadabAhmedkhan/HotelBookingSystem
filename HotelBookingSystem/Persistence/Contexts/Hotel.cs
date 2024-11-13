namespace HotelBookingSystem.Persistence.Contexts
{
    public class Hotel
    {
        public int Id { get; set; }                  // Primary key
        public string Name { get; set; }              // Name of the hotel
        public string Address { get; set; }           // Address of the hotel
        public string City { get; set; }              // City where the hotel is located
        public string State { get; set; }             // State of the hotel location
        public string Country { get; set; }           // Country of the hotel location
        public string Phone { get; set; }             // Contact phone number of the hotel
        public ICollection<Room> Rooms { get; set; }  // List of rooms in the hotel
    }
}
