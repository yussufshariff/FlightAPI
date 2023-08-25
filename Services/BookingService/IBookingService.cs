namespace FlightAPI.Services.BookingService
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetBooking(int UserId);
        Booking AddBooking(int UserId, string FlightId, Booking booking);
        List<Booking> DeleteBooking(int BookingId, int UserId);
        List<Booking> CancelBooking(int BookingId, int UserId, Booking request);

    }
}
