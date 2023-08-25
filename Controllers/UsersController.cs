using Microsoft.AspNetCore.Mvc;

namespace FlightAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService userService;


        public UsersController(IUserService userService)
        {
            this.userService = userService;


        }
        [HttpGet("api/User/{UserId}")]
        public async Task<ActionResult<List<User>>> GetUser(int UserId)
        {
            var user = await this.userService.GetUser(UserId);
            if (user == null)
                return NotFound("User not found");
            return Ok(user);
        }


        [HttpPost("api/User")]
        public async Task<ActionResult<User>> CreateNewUser(User user)
        {
            try
            {
                var newUser = await this.userService.CreateNewUser(user);

                return CreatedAtAction(nameof(GetUser), new { id = newUser.UserId }, newUser);
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
                return StatusCode(500, "An error occurred while processing creating a new user.");
            }
        }

    }


}
