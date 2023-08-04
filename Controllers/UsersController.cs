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
        public async Task<ActionResult<List<UsersModel>>> GetUser(int UserId)
        {
            var user = await this.context.Users.FindAsync(UserId);
            if (user == null)
                return NotFound("User not found");
            return Ok(user);
        }


        [HttpPost]
        public async Task<ActionResult<List<UsersModel>>> CreateNewUser(UsersModel user)
        {
            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();
            return await this.context.Users.ToListAsync();

        }

    }


}
