namespace FlightAPI.Services.BookingService
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetBooking(int UserId);
        Task<Booking> AddBooking(int UserId, string FlightId, Booking booking);
        Task<List<Booking>> DeleteBooking(int BookingId, int UserId);
        Task<Booking> CancelBooking(int BookingId, int UserId, Booking request);

    }
}

