namespace FlightAPI.Services.FlightService
{
    public interface IFlightService
    {
        Task<List<Flight>> GetAllFlights();
        Task<List<Flight>> GetFlightsByMonthRange(int startYear, int startMonth, int endYear, int endMonth);
        Task<Flight> GetSingleFlight(string FlightId);

    }
}
