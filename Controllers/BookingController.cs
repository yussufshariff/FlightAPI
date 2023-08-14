using FlightAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        private readonly static List<User> users;

        static BookingController()
        {
            users = new List<User>
        {
            new User
            {
                UserId = 1,
                UserName = "USERCONTROLLER",
                Password = "Test",
                Email = "Test",
                Phone = "07123817823",
                IsActive = true,
            },
             new User
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


        [HttpGet("api/Users{UserId}")]
        public async Task<ActionResult<List<User>>> GetUser(int UserId)
        {
            var user = await this.context.Users.FindAsync(UserId);
            if (user == null)
                return NotFound("User not found");
            return Ok(user);
        }


        [HttpPost("api/Bookings")]
        public async Task<ActionResult<Booking>> AddBooking(int UserId, Booking booking)
        {
            try
            {
                var user = await context.Users.Include(u => u.Bookings).FirstOrDefaultAsync(u => u.UserId == UserId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                if (booking == null)
                    return BadRequest("Booking data cannot be null.");

                booking.BookingDate = DateTime.Now;
                booking.IsCancelled = false;
                booking.UserId = UserId;

                booking.BookingId = user.Bookings.Count + 1;
                user.Bookings.Add(booking);

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ex.Entries.Single().Reload();
                    await context.SaveChangesAsync();
                    user.RowVersion = BitConverter.GetBytes(DateTime.UtcNow.Ticks);
                    await context.SaveChangesAsync();
                }

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                var serializedBooking = JsonSerializer.Serialize(booking, options);

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
            var user = users.FirstOrDefault(u => u.UserId == UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var booking = user.Bookings.FirstOrDefault(x => x.BookingId == BookingId);
            if (booking == null)
                return NotFound("This booking does not exist");
            user.Bookings.Remove(booking);

            return Ok("Succesfully deleted");

        }

        [HttpPut("api/Bookings/Passengers")]

        public async Task<ActionResult<List<Booking>>> UpdateSeats(int BookingId, int UserId, Booking request)
        {
            var user = users.FirstOrDefault(u => u.UserId == UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var booking = user.Bookings.FirstOrDefault(x => x.BookingId == BookingId);
            if (booking == null)
                return NotFound("This booking does not exist");

            booking.NumPassengers = request.NumPassengers;

            return Ok(user);


        }

    }

}





