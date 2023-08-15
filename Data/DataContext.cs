using FlightAPI.Models;

namespace FlightAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=FlightDB;Trusted_Connection=true;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>().HasKey(f => f.FlightId);
            modelBuilder.Entity<Booking>().HasKey(b => b.BookingId);
            modelBuilder.Entity<User>().HasKey(u => u.UserId);

            modelBuilder.Entity<Booking>()
           .Property(b => b.TotalPrice)
           .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Flight>()
           .Property(b => b.Price)
           .HasColumnType("decimal(18, 2)");


        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
