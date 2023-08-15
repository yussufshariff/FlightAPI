using FlightAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightAPI.Controllers

{
    [Route("/")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly DataContext context;

        public BookingController(DataContext context)
        {
            this.context = context;

        }

        [HttpGet("api/Bookings")]
        public async Task<ActionResult<Booking>> GetBooking(int UserId, int BookingId, string FlightId)
        {
            var user = this.context.Users.FirstOrDefault(u => u.UserId == UserId);
            if (user == null)
                return NotFound("User not found");

            var booking = await this.context.Bookings.FindAsync(BookingId);
            if (booking == null)
                return NotFound("Booking not found");

            var flight = await this.context.Flights.FindAsync(FlightId);
            if (flight == null)
            {
                return NotFound("Flight not found");
            }
            else
            {
                booking.Flight = flight;

            }

            if (!string.Equals(booking.FlightId, FlightId, StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Wrong flight for this booking");
            }

            return Ok(booking);
        }

        [HttpPost("api/Bookings")]
        public async Task<ActionResult<Booking>> AddBooking(int UserId, string FlightId, Booking booking)
        {
            try
            {
                var user = this.context.Users.FirstOrDefault(u => u.UserId == UserId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var flight = this.context.Flights.FirstOrDefault(f => f.FlightId == FlightId);
                if (flight == null)
                {
                    return NotFound("Flight not found.");
                }

                if (booking == null)
                    return BadRequest("Booking data cannot be null.");

                booking.BookingDate = DateTime.Now;
                booking.IsCancelled = false;
                booking.UserId = UserId;
                booking.FlightId = FlightId;
                booking.User = user;
                booking.Flight = flight;

                booking.TotalPrice = flight.Price;

                this.context.Bookings.Add(booking);
                await this.context.SaveChangesAsync();

                return CreatedAtAction(nameof(AddBooking), new { UserId, bookingId = booking.BookingId }, booking);

            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the booking.");
            }
        }


        [HttpDelete("api/Bookings")]
        public async Task<ActionResult<List<Booking>>> DeleteBooking(int BookingId, int UserId)

        {
            var user = this.context.Users.FirstOrDefaultAsync(u => u.UserId == UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var booking = this.context.Bookings.FirstOrDefault(x => x.BookingId == BookingId);

            if (booking == null)
            {
                return NotFound("This booking does not exist");
            }

            this.context.Bookings.Remove(booking);
            await this.context.SaveChangesAsync();


            return Ok("Succesfully deleted");

        }

        [HttpPut("api/Bookings")]

        public async Task<ActionResult<List<Booking>>> CancelBooking(int BookingId, int UserId, Booking request)
        {
            var user = this.context.Users.FirstOrDefault(u => u.UserId == UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var booking = this.context.Bookings.FirstOrDefault(x => x.BookingId == BookingId);
            if (booking == null)
                return NotFound("This booking does not exist");

            booking.IsCancelled = request.IsCancelled;

            await this.context.SaveChangesAsync();

            return Ok(booking);


        }

    }

}





