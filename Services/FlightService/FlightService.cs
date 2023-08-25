namespace FlightAPI.Services.FlightService
{
    public class FlightService : IFlightService
    {
        private readonly DataContext context;

        public FlightService(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Flight>> GetAllFlights()
        {
            var flights = await this.context.Flights.ToListAsync();
            return flights;
        }
    }
}
