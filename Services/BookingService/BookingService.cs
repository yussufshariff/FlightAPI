namespace FlightAPI.Services.BookingService
{
    public class BookingService : IBookingService
    {
        private readonly DataContext context;

        public BookingService(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Booking> GetBooking(int UserId)
        {
            throw new NotImplementedException();
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
}
