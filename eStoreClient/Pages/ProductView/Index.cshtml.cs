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

namespace eStoreClient.Pages.ProductView
{
    public class IndexModel : PageModel
    {
        public IProductRepository repository = new ProductRepository();
        public IEnumerable<Product> pro { get; set; }

        private readonly HttpClient client = null;
        private string ProductApiUlr = "";

        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUlr = "https://localhost:7124/api/Products";
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            HttpResponseMessage respone = await client.GetAsync(ProductApiUlr);
            string strData = await respone.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Product = (IList<Product>)JsonSerializer.Deserialize<List<Product>>(strData, options);

        }
    }
}
