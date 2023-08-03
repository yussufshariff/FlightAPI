using FlightAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightAPI.Controllers

{
    [Route("/")]
    [ApiController]
    public class BookingController : ControllerBase

    {
        private readonly ILogger<BookingController> _logger;

        public BookingController(ILogger<BookingController> logger)
        {
            _logger = logger;
        }

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
            },
             new UsersModel
            {
                UserId = 2,
                UserName = "Michael George",
                Password = "qwerty12",
                Email = "Test@gmail.co.ca",
                Phone = "07123817823",
                IsActive = true,
            }
          };
        }


        [HttpGet("api/Users")]
        public async Task<ActionResult<List<UsersModel>>> GetUser(int UserId)
        {
            var user = users.Find(x => x.UserId == UserId);
            if (user == null)
                return NotFound("User not found");
            return Ok(user);
        }

        [HttpPost("api/Bookings")]
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

                booking.BookingDate = DateTime.Now;
                booking.IsCancelled = false;



                var user = users.FirstOrDefault(u => u.UserId == UserId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }
                booking.BookingId = user.Bookings.Count + 1;
                user.Bookings.Add(booking);


                return CreatedAtAction(nameof(GetUser), new { bookingId = booking.BookingId }, booking);


            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the booking.");
            }
        }

        [HttpDelete("api/Bookings")]
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


        [HttpPut("api/Bookings/Passengers")]

        public async Task<ActionResult<List<BookingModel>>> UpdateSeats(int BookingId, int UserId, BookingModel request)
        {
            var user = users.FirstOrDefault(u => u.UserId == UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var booking = user.Bookings.Find(x => x.BookingId == BookingId);
            if (booking == null)
                return NotFound("This booking does not exist");

            booking.NumPassengers = request.NumPassengers;

            return Ok(user);


        }

    }

}





