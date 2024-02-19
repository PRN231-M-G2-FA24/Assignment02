using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Implements;
using System.Net.Http.Headers;
using System.Text.Json;
using BusinessObject;
//using Newtonsoft.Json;

namespace eStoreClient.Pages.Report
{
    public class IndexModel : PageModel
    {
        public IOrderRepository repository = new OrderRepository();
        public IEnumerable<Order> pro { get; set; }

        private readonly HttpClient client = null;
        private string OrderApiUlr = "";

        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            OrderApiUlr = "https://localhost:7124/api/Orders";
        }

        public IList<Order> Order { get;set; } = default!;

        public DateTime fromDate = DateTime.Now;
        public DateTime toDate = DateTime.Now.AddDays(14);

        public async Task OnGetAsync()
        {
            HttpResponseMessage respone = await client.GetAsync(OrderApiUlr);
            string strData = await respone.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Order = (IList<Order>)JsonSerializer.Deserialize<List<Order>>(strData, options);
        }

        public async Task OnPostAsync()
        {
            HttpResponseMessage response = await client.GetAsync($"{OrderApiUlr}$?filter=OrderDate gt {fromDate}");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Order = (IList<Order>)JsonSerializer.Deserialize<List<Order>>(strData, options);
            response.EnsureSuccessStatusCode();
        }
    }
}
