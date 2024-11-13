namespace HotelBookingSystem.Persistence.Contexts
{
    public class Role
    {
        public int Id { get; set; }                       // Primary key
        public string Name { get; set; }                  // Name of the role (e.g., Admin, Customer)
        public string Description { get; set; }           // Description of the role's responsibilities
        public ICollection<User> Users { get; set; }      // List of users assigned to this role

    }
}
