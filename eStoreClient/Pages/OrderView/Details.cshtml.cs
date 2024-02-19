using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Implements;
using BusinessObject;
using System.Text.Json;
using System.Net.Http.Headers;

namespace eStoreClient.Pages.OrderView
{
    public class DetailsModel : PageModel
    {
        public IOrderRepository repository = new OrderRepository();


        private HttpClient client = null;
        private string OrderDetailsApiUlr = "https://localhost:7124/api/Orders/Details";

        public DetailsModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

      public Order Order { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int id)
        {        
            var order = repository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            else 
            {
                Order = order;
            }
            HttpResponseMessage respone = await client.GetAsync($"{OrderDetailsApiUlr}/{id}");
            string strData = await respone.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var orderDetails = (IList<OrderDetail>)JsonSerializer.Deserialize<List<OrderDetail>>(strData, options);
            ViewData["OrderDetailsModel"] = new OrderDetailsView.IndexModel()
            {
                OrderDetails = orderDetails,
            };
            return Page();
        }
    }
}
