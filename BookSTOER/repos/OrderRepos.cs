using Azure.Core;
using BookSTOER.Data;
using BookSTOER.Model;
using Microsoft.EntityFrameworkCore;


namespace BookSTOER.repos
{
    public class OrderRepos : IOrderRepos
    {
        private readonly dbcontext dbcontext;

        public OrderRepos(dbcontext dbcontext )
        {
            this.dbcontext = dbcontext;
        }
        public LoginResult AddOrder(OrderRequest orderRequest)
        {
            var user = dbcontext.Set<User>().Find(orderRequest.UserId);
            if (user == null) { return new LoginResult { Success = false, Message = "User not found." };
            }

            var order = new Order
            {
                UserId = orderRequest.UserId,
                OrderDate = DateTime.Now,
                TotalAmount = 0,
                OrderItems = new List<orderItim>()
            };

            foreach (var item in orderRequest.OrderItems)
            {
                var book = dbcontext.Set<Book>().Find(item.BookId);


                if (book == null)
                {
                    return new LoginResult { Success = false, Message = $"Book with ID {item.BookId} not found. " };
                }
                if (book.Stock < item.Quantity)
                {
                    return new LoginResult { Success = false, Message = $"Not enough stock for book '{book.Title}'. Only {book.Stock} left." };
                }


                book.Stock -= item.Quantity;
                var orderItem = new orderItim
                {
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    Order = order
                };

                order.OrderItems.Add(orderItem);
                order.TotalAmount += book.Price * item.Quantity;
            }


            dbcontext.Set<Order>().Add(order);
            dbcontext.SaveChanges();

            return new LoginResult
            {
                Success = true,
                Message = "Login successful",
                Data = new
                {
                    Item = order,

                }
            };
            }

        public LoginResult GetOrder(int id)
        {
            var orders = dbcontext.Set<Order>()
                 .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book).ToList()
                .FirstOrDefault(o => o.Id == id);

            if (orders == null)
            {
                return new LoginResult { Success = false, Message = "Order not found." };
            }

            return new LoginResult
            {
                Success = true,
                Message = "Order found.",
                Data = orders
            };
        }

        public List<Order> GetOrderss()
        {
            var orders =  dbcontext.Set<Order>()
                  .Include(o => o.User)
                 .Include(o => o.OrderItems)
                 .ThenInclude(oi => oi.Book)
                .ToList();

            return orders; ;
        }

        public LoginResult RemoveOrder(int id)
        {
            
            var order = dbcontext.Set<Order>()
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return new LoginResult { Success = false, Message = "Order not found." };
            }

            foreach (var item in order.OrderItems)
            {
                var book = dbcontext.Set<Book>().Find(item.BookId);
                if (book != null)
                {
                    book.Stock += item.Quantity;
                }
            }
            dbcontext.Set<Order>().Remove(order);
            dbcontext.SaveChanges();

            return new LoginResult { Success = true, Message = "Order removed successfully." };
        }


        public LoginResult UpdateOrder(int id)
        {
            var order =  dbcontext.Set<Order>().Find(id);
             dbcontext.Set<Order>().Include(o => o.OrderItems)
             .FirstOrDefault();

            if (order == null)
            {
                return new LoginResult { Success = false, Message = "Order not found." };
            };

            foreach (var item in order.OrderItems)
            {
                var book =  dbcontext.Set<Book>().Find(item.BookId);
                if (book != null)
                {
                    book.Stock += item.Quantity;

                }

            }
            return new LoginResult
            {
                Success = true,
                Message = "Update Order successful",
                Data = new
                {
                    Item = order,

                }
            };
        }
    }
    }

