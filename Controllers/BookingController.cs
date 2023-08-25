using Microsoft.AspNetCore.Mvc;

namespace FlightAPI.Controllers;


[Route("/")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly DataContext context;

    private readonly IBookingService bookingService;

    public BookingController(IBookingService bookingService)
    {
        this.bookingService = bookingService;
    }



    [HttpGet("api/Bookings")]
    public async Task<ActionResult<IEnumerable<Booking>>> GetBooking(int UserId)
    {
        var bookings = await this.bookingService.GetBooking(UserId);

        if (bookings == null)
        {
            return NotFound("No bookings for this user.");
        }

        return Ok(bookings);
    }


    [HttpPost("api/Bookings")]
    public async Task<ActionResult<Booking>> AddBooking(int UserId, string FlightId, Booking booking)
    {
        try
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.UserId == UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var flight = await this.context.Flights.FirstOrDefaultAsync(f => f.FlightId == FlightId);
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
        var user = await this.context.Users.FirstOrDefaultAsync(u => u.UserId == UserId);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var booking = await this.context.Bookings.FirstOrDefaultAsync(x => x.BookingId == BookingId);

        if (booking == null)
        {
            return NotFound("This booking does not exist");
        }

        this.context.Bookings.Remove(booking);
        await this.context.SaveChangesAsync();


        return Ok("Succesfully deleted");

    }

    [HttpPatch("api/Bookings")]

    public async Task<ActionResult<List<Booking>>> CancelBooking(int BookingId, int UserId, Booking request)
    {
        var user = await this.context.Users.FirstOrDefaultAsync(u => u.UserId == UserId);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var booking = await this.context.Bookings.FirstOrDefaultAsync(x => x.BookingId == BookingId);
        if (booking == null)
            return NotFound("This booking does not exist");

        booking.IsCancelled = request.IsCancelled;

        await this.context.SaveChangesAsync();

        return Ok(booking);


    }

}





