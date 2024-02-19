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
    public class DeleteModel : PageModel
    {
        public IProductRepository repository = new ProductRepository();
        public IEnumerable<Product> pro { get; set; }

        private readonly HttpClient client = null;
        private string ProductApiUlr = "";
        public DeleteModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUlr = "https://localhost:7124/api/Products";
        }

        [BindProperty]
      public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ProductApiUlr += "/" + id;
            HttpResponseMessage respone = await client.GetAsync(ProductApiUlr);
            string strData = await respone.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Product = JsonSerializer.Deserialize<Product>(strData, options);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var p = repository.GetProductById(id);
            if (p == null)

                return NotFound();
            repository.DeleteProduct(p);

            return RedirectToPage("./Index");
        }
    }
}
