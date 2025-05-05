namespace BookSTOER.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[]? image { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string Role { get; set; } = "Client";

        public string Address { get; set; } = string.Empty;

        public List<Order> Orders { get; set; } = new();
    }
}