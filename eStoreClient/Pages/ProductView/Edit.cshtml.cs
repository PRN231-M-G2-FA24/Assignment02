using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Implements;
using System.Net.Http.Headers;
using System.Text.Json;
using BusinessObject;

namespace eStoreClient.Pages.ProductView
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;



        public IProductRepository repository = new ProductRepository();
        public IEnumerable<Product> pro { get; set; }

        private readonly HttpClient client = null;
        private string ProductApiUlr = "";

        public EditModel(AppDbContext context)
        {
            _context = context;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUlr = "https://localhost:7124/api/Products";
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormCollection collection)
        {
            int CategoryId = int.Parse(collection["CategoryId"]);

            Product.CategoryId = CategoryId;


            repository.UpdateProduct(Product);


            return RedirectToPage("./Index");
        }

     
    }
}
