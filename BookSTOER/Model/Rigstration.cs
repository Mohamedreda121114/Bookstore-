namespace BookSTOER.Model
{
    public class Rigstration
    {
        public string FullName { get; set; } = string.Empty;
        public IFormFile? image { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string password {  get; set; } = string.Empty;   

    }
}
