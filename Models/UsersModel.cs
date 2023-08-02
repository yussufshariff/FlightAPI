namespace FlightAPI.Models
{
    public class UsersModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
        public List<BookingModel> Bookings { get; set; } = new List<BookingModel>();

    }
}
