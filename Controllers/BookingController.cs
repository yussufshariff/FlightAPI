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
            var addedBooking = await this.bookingService.AddBooking(UserId, FlightId, booking);
            return CreatedAtAction(nameof(AddBooking), new { UserId, bookingId = addedBooking.BookingId }, addedBooking);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while processing the booking.");
        }
    }



    [HttpDelete("api/Bookings")]
    public async Task<ActionResult<List<Booking>>> DeleteBooking(int BookingId, int UserId)
    {
        try
        {
            var remainingBookings = await this.bookingService.DeleteBooking(BookingId, UserId);
            if (remainingBookings == null)
            {
                return NotFound("This booking does not exist");
            }

            return Ok("Successfully deleted");
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while deleting the booking.");
        }
    }



    [HttpPatch("api/Bookings")]
    public async Task<ActionResult<Booking>> CancelBooking(int BookingId, int UserId, Booking request)
    {
        try
        {
            var updatedBooking = await this.bookingService.CancelBooking(BookingId, UserId, request);
            return Ok(updatedBooking);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while canceling the booking.");
        }
    }


}





