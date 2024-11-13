namespace HotelBookingSystem.Persistence.Contexts
{
    public class User
    {
        public int Id { get; set; }                           // Primary key
        public string Username { get; set; }                  // Username for the user
        public string Email { get; set; }                     // Email address of the user
        public string PasswordHash { get; set; }              // Hashed password for authentication
        public string FullName { get; set; }                  // Full name of the user
        public string PhoneNumber { get; set; }               // Contact number
        public int RoleId { get; set; }                       // Foreign key to the Role of the user
        public string Role { get; set; }                        // Reference to the user's Role
        public ICollection<Booking> Bookings { get; set; }    // List of bookings made by the user
    }
}
