namespace FlightAPI.Services.BookingService;

public class BookingService : IBookingService
{
    private readonly DataContext context;

    public BookingService(DataContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Booking>> GetBooking(int UserId)
    {
        var user = this.context.Users.FirstOrDefault(u => u.UserId == UserId);
        if (user is null)
            throw new InvalidOperationException("User not found.");
        var bookings = this.context.Bookings
            .Where(b => b.UserId == UserId)
            .ToList();
        if (!bookings.Any())
            return null;

        foreach (var booking in bookings)
        {
            context.Entry(booking).Reference(b => b.Flight).Load();
        }

        return bookings;

    }

    public async Task<Booking> AddBooking(int UserId, string FlightId, Booking booking)
    {
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.UserId == UserId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var flight = await this.context.Flights.FirstOrDefaultAsync(f => f.FlightId == FlightId);
            if (flight == null)
            {
                throw new InvalidOperationException("Flight not found.");
            }

            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking), "Booking data cannot be null.");
            }

            booking.BookingDate = DateTime.Now;
            booking.IsCancelled = false;
            booking.UserId = UserId;
            booking.FlightId = FlightId;
            booking.User = user;
            booking.Flight = flight;

            booking.TotalPrice = flight.Price;

            this.context.Bookings.Add(booking);
            await this.context.SaveChangesAsync();

            return booking;
        }
    }


    public async Task<List<Booking>> DeleteBooking(int BookingId, int UserId)
    {
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.UserId == UserId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var booking = await this.context.Bookings.FirstOrDefaultAsync(x => x.BookingId == BookingId);
            if (booking == null)
            {
                throw new InvalidOperationException("This booking does not exist");
            }

            if (booking.UserId == UserId)
            {
                this.context.Bookings.Remove(booking);
            }
            else
            {
                throw new InvalidOperationException("Incorrect user for this Booking");

            }

            await this.context.SaveChangesAsync();

            var remainingBookings = await this.context.Bookings
                .Where(b => b.UserId == UserId)
                .ToListAsync();

            return remainingBookings;
        }
    }
    public async Task<Booking> CancelBooking(int BookingId, int UserId, Booking request)
    {
        var user = await this.context.Users.FirstOrDefaultAsync(u => u.UserId == UserId);
        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        var booking = await this.context.Bookings.FirstOrDefaultAsync(x => x.BookingId == BookingId);
        if (booking == null)
        {
            throw new InvalidOperationException("This booking does not exist");
        }

        if (booking.UserId == UserId)
        {
            booking.IsCancelled = request.IsCancelled;
        }
        else
        {
            throw new InvalidOperationException("Incorrect user for this Booking");

        }

        await this.context.SaveChangesAsync();


        return booking;


    }

}

