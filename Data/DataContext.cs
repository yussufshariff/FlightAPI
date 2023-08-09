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
            modelBuilder.Entity<FlightModel>().HasKey(f => f.FlightId);
            modelBuilder.Entity<BookingModel>().HasKey(b => b.BookingId);
            modelBuilder.Entity<UsersModel>().HasKey(u => u.UserId);

            modelBuilder.Entity<BookingModel>()
           .Property(b => b.TotalPrice)
           .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<FlightModel>()
           .Property(b => b.Price)
           .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<BookingModel>()
               .HasOne(b => b.User)
               .WithMany(u => u.Bookings)
               .HasForeignKey(b => b.UserId);

        }





        public DbSet<FlightModel> Flights { get; set; }
        public DbSet<BookingModel> Bookings { get; set; }
        public DbSet<UsersModel> Users { get; set; }

    }
}
