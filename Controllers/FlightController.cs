using FlightAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private static List<FlightModel> flights = new List<FlightModel> {
                new FlightModel {
                FlightId = "FL123",
                Origin = "New York (JFK)",
                Destination = "Los Angeles (LAX)",
                DepartureDate = new DateTime(2023, 08, 15, 08, 00, 00),
                ArrivalDate = new DateTime(2023, 08, 15, 11, 30, 00),
                Airline = "Example Airlines",
                Aircraft = "Boeing 737",
                AvailableSeats = 150,
                Price = 250.50m,
                Duration = new TimeSpan(3, 30, 0)
                },
               new FlightModel
             {
                FlightId = "FL789",
                Origin = "Chicago (ORD)",
                Destination = "Miami (MIA)",
                DepartureDate = new DateTime(2023, 08, 17, 11, 00, 00),
                ArrivalDate = new DateTime(2023, 08, 17, 15, 00, 00),
                Airline = "Fly High Airlines",
                Aircraft = "Boeing 787",
                AvailableSeats = 80,
                Price = 320.25m,
                Duration = new TimeSpan(4, 0, 0)
            }

            };

        [HttpGet]
        public async Task<ActionResult<List<FlightModel>>> GetAllFlights()
        {
            return Ok(flights);
        }

        [HttpGet("{FlightId}")]

        public async Task<ActionResult<List<FlightModel>>> GetSingleFlight(string FlightId)
        {
            var flight = flights.Find(x => x.FlightId == FlightId);
            if (flight == null)
                return NotFound("Invalid flight number");
            return Ok(flight);
        }


    }
}
