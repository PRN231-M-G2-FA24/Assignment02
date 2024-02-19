using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.DTO;
using Repository.Implements;
using Repository;
using BusinessObject;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableQuery]
    public class OrderDetailsController : ODataController
    {
        private IOrderDetailRepository repository = new OrderDetailRepository();

        public OrderDetailsController()
        {
            
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
        => repository.GetOrderDetail();        

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {
            var order = repository.GetOrderDetailById(id);
            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/OrderDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetail(int id, OrderDetailsDTO o)
        {
            OrderDetail orderDetail = new OrderDetail
            {
                OrderId = o.OrderId,
                ProductId = o.ProductId,
                UnitPrice = o.UnitPrice,
                Quantity = o.Quantity,
                Discount = o.Discount
            };
            var pTmp = repository.GetOrderDetailById(id);
            if (pTmp == null)

                return NotFound();
            pTmp = orderDetail;
            pTmp.OrderId = id;
            repository.UpdateOrderDetail(pTmp);
            return NoContent();
        }

        // POST: api/OrderDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(OrderDetailsDTO o)
        {
            OrderDetail orderDetail = new OrderDetail
            {
                OrderId = o.OrderId,
                ProductId = o.ProductId,
                UnitPrice = o.UnitPrice,
                Quantity = o.Quantity,
                Discount = o.Discount
            };
            repository.SaveOrderDetail(orderDetail);

            return NoContent();
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var order = repository.GetOrderDetailById(id);
            if (order == null)
            {
                return NotFound();
            }

            repository.DeleteOrderDetail(order);

            return NoContent();
        }

        
    }
}
