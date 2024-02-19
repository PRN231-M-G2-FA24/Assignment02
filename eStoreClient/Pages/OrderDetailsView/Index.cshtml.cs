using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Net.Http.Headers;
using System.Text.Json;
using BusinessObject;
using Repository;
using Repository.Implements;

namespace eStoreClient.Pages.OrderDetailsView
{
    public class IndexModel : PageModel
    {
        public IOrderDetailRepository repository = new OrderDetailRepository();
        public IEnumerable<OrderDetail> pro { get; set; }

        private readonly HttpClient client = null;
        private string OrderDetailApiUlr = "";
        private string OrderDetailsApiUlr = "https://localhost:7124/api/Orders/Details";
        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            OrderDetailApiUlr = "https://localhost:7124/api/OrderDetails";

        }

        public IList<OrderDetail> OrderDetails { get;set; } = default!;

        //public async Task OnGetAsync()
        //{
        //    HttpResponseMessage respone = await client.GetAsync(OrderDetailApiUlr);
        //    string strData = await respone.Content.ReadAsStringAsync();
        //    var options = new JsonSerializerOptions
        //    {
        //        PropertyNameCaseInsensitive = true
        //    };
        //    OrderDetails = (IList<OrderDetail>)JsonSerializer.Deserialize<List<OrderDetail>>(strData, options);
        //}
        public async Task OnGetAsync(int orderId)
        {
            string partialUrl = $"{OrderDetailsApiUlr}/{orderId}";
            HttpResponseMessage respone = await client.GetAsync($"{OrderDetailsApiUlr}/{orderId}");
            string strData = await respone.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            OrderDetails = (IList<OrderDetail>)JsonSerializer.Deserialize<List<OrderDetail>>(strData, options);
        }

    }
}
