namespace FlightAPI.Services.FlightService
{
    public interface IFlightService
    {
        Task<List<Flight>> GetAllFlights();

    }
}
