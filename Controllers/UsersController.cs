using FlightAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<UsersModel> users = new List<UsersModel>
        {
        };
        [HttpGet("{UserId}")]
        public async Task<ActionResult<List<UsersModel>>> GetUser(int UserId)
        {
            var user = users.Find(x => x.UserId == UserId);
            if (user == null)
                return NotFound("User not found");
            return Ok(user);
        }


        [HttpPost]
        public async Task<ActionResult<List<UsersModel>>> CreateNewUser(UsersModel user)
        {
            users.Add(user);
            return Ok(user);

        }

    }


}
