using SiteLib.Bcy.Net;

namespace BookSTOER.Model
{
    public class Order
    {

        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public List<orderItim> OrderItems { get; set; } = new();
    }

    
}
