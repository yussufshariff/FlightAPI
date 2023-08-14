using FlightAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {


        private readonly DataContext context;

        public UsersController(DataContext context)
        {
            this.context = context;


        }
        [HttpGet("{UserId}")]
        public async Task<ActionResult<List<User>>> GetUser(int UserId)
        {
            var user = await this.context.Users.FindAsync(UserId);
            if (user == null)
                return NotFound("User not found");
            return Ok(user);
        }


        [HttpPost]
        public async Task<ActionResult<User>> CreateNewUser(User user)
        {
            try
            {
                this.context.Users.Add(user);
                await this.context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUser), new { userId = user.UserId }, user);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the user.");
            }
        }

    }


}
