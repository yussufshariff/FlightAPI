namespace FlightAPI.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext context;

        public UserService(DataContext context)
        {
            this.context = context;
        }

        public async Task<User> GetUser(int UserId)
        {
            var user = await this.context.Users.FindAsync(UserId);
            if (user == null)
            {
                throw new ArgumentException("User does not exist.");

            }
            return user;

        }
        public async Task<User> CreateNewUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User data cannot be null.");
            }
            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();
            return user;

        }

    }
}
