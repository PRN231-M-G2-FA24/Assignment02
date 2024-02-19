using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.DTO;
using BusinessObject;
using Repository;
using Repository.Implements;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableQuery]
    public class OrdersController : ODataController
    {

        private IOrderRepository repository = new OrderRepository();
        public OrdersController()
        {

        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
            => repository.GetOrder();

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {

            var order = repository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpGet("Details/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails(int orderId)
        => repository.GetOrderDetails(orderId);

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderDTO o)
        {
            Order order = new Order
            {
                OrderId = id,
                MemberId = o.MemberId,
                OrderDate = o.OrderDate,
                RequiredDate = o.RequireDate,
                ShippedDate = o.ShippedDate,
                Freight = o.Freight
            };


            var pTmp = repository.GetOrderById(id);
            if (pTmp == null)

                return NotFound();
            pTmp = order;
            pTmp.OrderId = id;
            repository.UpdateOrder(pTmp);
            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderDTO o)
        {
            Order order = new Order
            {

                MemberId = o.MemberId,
                OrderDate = o.OrderDate,
                RequiredDate = o.RequireDate,
                ShippedDate = o.ShippedDate,
                Freight = o.Freight
            };


            repository.SaveOrder(order);

            return NoContent();
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {

            var order = repository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            repository.DeleteOrder(order);

            return NoContent();
        }


    }
}
