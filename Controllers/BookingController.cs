using FlightAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private static List<BookingModel> bookings = new List<BookingModel>();

        [HttpGet("{bookingId}")]
        public ActionResult<BookingModel> GetBookingById(int bookingId)
        {
            var booking = bookings.Find(b => b.BookingId == bookingId);
            // can also use FirstOrDefault instead of Find
            if (booking == null)
                return NotFound("This booking does not exist");
            return Ok(booking);
        }

        [HttpPost]
        public async Task<ActionResult<List<BookingModel>>> AddBooking(BookingModel booking)
        {
            await Task.Delay(100);

            try
            {
                if (booking == null)
                    return BadRequest("Booking data cannot be null.");
                if (string.IsNullOrWhiteSpace(booking.PassengerName))
                    return BadRequest("Passenger name is required.");
                if (string.IsNullOrWhiteSpace(booking.ContactEmail))
                    return BadRequest("Contact email is required.");

                booking.BookingId = GetNextBookingId();
                booking.BookingDate = DateTime.Now;
                booking.IsCancelled = false;
                bookings.Add(booking);

                return CreatedAtAction(nameof(GetBookingById), new { bookingId = booking.BookingId }, booking);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the booking.");
            }
        }
        private int GetNextBookingId()
        {
            return bookings.Count + 1;
        }


        [HttpDelete("{BookingId}")]
        public async Task<ActionResult<List<BookingModel>>> DeleteBooking(int BookingId)

        {
            var booking = bookings.Find(x => x.BookingId == BookingId);
            if (booking == null)
                return NotFound("This booking does not exist");
            bookings.Remove(booking);

            return Ok("Succesfully deleted");
        }


    }
}
