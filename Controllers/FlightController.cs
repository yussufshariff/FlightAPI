using FlightAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {



        private readonly DataContext context;

        public FlightController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Flight>>> GetAllFlights()
        {
            return Ok(await this.context.Flights.ToListAsync());
        }

        [HttpGet("{FlightId}")]

        public async Task<ActionResult<List<Flight>>> GetSingleFlight(string FlightId)
        {
            var flight = await this.context.Flights.FindAsync(FlightId);
            if (flight == null)
                return NotFound("Invalid flight number");
            return Ok(flight);
        }


    }
}
