using FlightAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightAPI.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase

    {
        private readonly ILogger<BookingController> _logger;

        public BookingController(ILogger<BookingController> logger)
        {
            _logger = logger;
        }

        private static List<BookingModel> bookings = new List<BookingModel>();

        private static List<UsersModel> users;

        static BookingController()
        {
            users = new List<UsersModel>
        {
            new UsersModel
            {
                UserId = 1,
                UserName = "USERCONTROLLER",
                Password = "Test",
                Email = "Test",
                Phone = "07123817823",
                IsActive = true,
                Bookings = new List<BookingModel>()
            }
        };
        }


        [HttpGet("{bookingId}")]
        public ActionResult<BookingModel> GetBookingById(int bookingId)
        {
            var booking = bookings.Find(b => b.BookingId == bookingId);
            if (booking == null)
                return NotFound("This booking does not exist");
            return Ok(booking);
        }



        [HttpPost]
        public async Task<ActionResult<List<BookingModel>>> AddBooking(int UserId, BookingModel booking)
        {

            await Task.Delay(100);

            try
            {
                if (booking == null)
                    return BadRequest("Booking data cannot be null.");
                if (string.IsNullOrWhiteSpace(booking.PassengerName))
                    return BadRequest("Passenger name is required.");
                if (string.IsNullOrWhiteSpace(booking.Email))
                    return BadRequest("Contact email is required.");

                booking.BookingId = GetNextBookingId();
                booking.BookingDate = DateTime.Now;
                booking.IsCancelled = false;



                var user = users.FirstOrDefault(u => u.UserId == UserId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }
                user.Bookings.Add(booking);


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
        public async Task<ActionResult<List<BookingModel>>> DeleteBooking(int BookingId, int UserId)

        {

            var user = users.FirstOrDefault(u => u.UserId == UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var booking = user.Bookings.Find(x => x.BookingId == BookingId);
            if (booking == null)
                return NotFound("This booking does not exist");
            user.Bookings.Remove(booking);

            return Ok("Succesfully deleted");

        }




        [HttpGet]
        public async Task<ActionResult<List<UsersModel>>> GetUser(int UserId)
        {
            var user = users.Find(x => x.UserId == UserId);
            if (user == null)
                return NotFound("User not found");
            return Ok(user);
        }



    }

}





