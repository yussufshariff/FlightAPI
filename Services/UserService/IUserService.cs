namespace FlightAPI.Services.UserService
{
    public interface IUserService
    {
        Task<User> GetUser(int UserId);
        Task<User> CreateNewUser(User user);

    }
}
