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

        public async Task<List<Flight>> GetFlightsByMonthRange(int startYear, int startMonth, int endYear, int endMonth)
        {
            if (startYear < 0 || startMonth < 1 || startMonth > 12 || endYear < 0 || endMonth < 1 || endMonth > 12)
            {
                throw new ArgumentException("Invalid input. Please provide valid year (positive integer) and month (1-12) values.");
            }

            var filteredFlights = await context.Flights
                 .Where(f =>
                     (f.DepartureDate.Year == startYear && f.DepartureDate.Month >= startMonth) ||
                     (f.DepartureDate.Year == endYear && f.DepartureDate.Month <= endMonth) ||
                     (f.DepartureDate.Year > startYear && f.DepartureDate.Year < endYear))
                 .ToListAsync();

            return filteredFlights;
        }

        public async Task<List<Flight>> GetFlightByLocation(string departure, string arrival)
        {
            var filteredLocation = await context.Flights
                 .Where(f =>
                     (f.Origin == departure) || (f.Destination == arrival)).ToListAsync();

            return filteredLocation;
        }

        public async Task<Flight> GetSingleFlight(string FlightId)
        {
            var flight = await this.context.Flights.FindAsync(FlightId);
            if (flight == null)
                throw new ArgumentException("Invalid flight number");
            return flight;
        }
    }
}
