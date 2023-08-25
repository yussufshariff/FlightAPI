using FlightAPI.Services.FlightService;
using Microsoft.AspNetCore.Mvc;

namespace FlightAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class FlightController : ControllerBase
    {

        private readonly DataContext context;

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

        [HttpGet("api/FlightFilter")]
        public async Task<ActionResult<List<Flight>>> GetFlightsByMonthRange(int startYear, int startMonth, int endYear, int endMonth)
        {
            var filteredFlights = await context.Flights
                .Where(f =>
                    (f.DepartureDate.Year == startYear && f.DepartureDate.Month >= startMonth) ||
                    (f.DepartureDate.Year == endYear && f.DepartureDate.Month <= endMonth) ||
                    (f.DepartureDate.Year > startYear && f.DepartureDate.Year < endYear))
                .ToListAsync();

            return Ok(filteredFlights);
        }



        [HttpGet("api/Flight/{FlightId}")]

        public async Task<ActionResult<List<Flight>>> GetSingleFlight(string FlightId)
        {
            var flight = await this.context.Flights.FindAsync(FlightId);
            if (flight == null)
                return NotFound("Invalid flight number");
            return Ok(flight);
        }


    }
}
