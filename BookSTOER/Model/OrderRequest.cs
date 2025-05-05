namespace BookSTOER.Model
{
    public class OrderRequest
    {
        public int UserId { get; set; }
        public List<OrderItemRequest> OrderItems { get; set; }
    }


}
