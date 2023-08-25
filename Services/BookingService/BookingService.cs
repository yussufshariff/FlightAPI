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
            return null;
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

    public Booking AddBooking(int UserId, string FlightId, Booking booking)
    {
        throw new NotImplementedException();
    }


    public List<Booking> DeleteBooking(int BookingId, int UserId)
    {
        throw new NotImplementedException();
    }
    public List<Booking> CancelBooking(int BookingId, int UserId, Booking request)
    {
        throw new NotImplementedException();
    }

}

