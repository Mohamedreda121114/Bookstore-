using BookSTOER.Model;

namespace BookSTOER.repos
{
    public interface IOrderRepos
    {
        public List<Order> GetOrderss();
        public LoginResult GetOrder(int id);
        public LoginResult AddOrder(OrderRequest  orderRequest);

        public LoginResult UpdateOrder(int  id);
        public LoginResult RemoveOrder(int id);
    }
}
