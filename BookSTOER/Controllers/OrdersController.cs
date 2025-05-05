using BookSTOER.Model;

using Microsoft.AspNetCore.Mvc;
using BookSTOER.Data;
using Microsoft.EntityFrameworkCore;
using BookSTOER.repos;



namespace BookSTOER.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderRepos orderRepos;

        public OrdersController(OrderRepos orderRepos )
        {
            this.orderRepos = orderRepos;
        }




        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderRequest orderRequest)
        {
            return Ok(orderRepos.AddOrder(orderRequest));
          
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            return Ok(orderRepos.RemoveOrder(orderId));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(int ID)
        {
            return Ok(orderRepos.UpdateOrder(ID));

        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(orderRepos.GetOrderss());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllOrder( int id)
        {
            return Ok(orderRepos.GetOrder(id));
        }

    }

}

