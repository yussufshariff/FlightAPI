using Microsoft.AspNetCore.Mvc;

namespace FlightAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService flightService;

        public FlightController(IFlightService flightService)
        {
            this.flightService = flightService;
        }


        [HttpGet("api/Flights")]
        public async Task<ActionResult<List<Flight>>> GetAllFlights()
        {
            return await this.flightService.GetAllFlights();
        }



        [HttpGet("api/FlightLocation")]
        public async Task<ActionResult<List<Flight>>> GetFlightByLocation(string departure, string arrival)
        {
            var flights = await this.flightService.GetFlightByLocation(departure, arrival);

            if (flights == null)
                return NotFound("No flights found.");
            return Ok(flights);
        }


        [HttpGet("api/FlightFilter")]
        public async Task<ActionResult<List<Flight>>> GetFlightsByMonthRange(int startYear, int startMonth, int endYear, int endMonth)
        {
            var flights = await this.flightService.GetFlightsByMonthRange(startYear, startMonth, endYear, endMonth);

            if (flights == null)
                return NotFound("No flights found.");

            return Ok(flights);
        }


        [HttpGet("api/Flight/{FlightId}")]

        public async Task<ActionResult<Flight>> GetSingleFlight(string FlightId)
        {
            var flight = await this.flightService.GetSingleFlight(FlightId);

            if (flight == null)
                return NotFound("No flights found.");

            return Ok(flight);

        }
    }
}

