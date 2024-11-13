using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HotelBookingSystem.Persistence.Contexts
{
    public class HotelBookingDbContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public HotelBookingDbContext(DbContextOptions<HotelBookingDbContext> options)
            : base(options)
        {
        }
    }
}
