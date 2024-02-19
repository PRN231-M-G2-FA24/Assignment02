using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.DTO;
using NuGet.Protocol.Core.Types;
using Repository;
using Repository.Implements;
using BusinessObject;

namespace eStoreClient.Pages.ProductView
{
    public class CreateModel : PageModel
    {
        public IProductRepository repository = new ProductRepository();

        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormCollection collection)
        {
            int CategoryId=int.Parse(collection["CategoryId"]);

            Product.CategoryId = CategoryId;
           
            
            repository.SaveProduct(Product);


            return RedirectToPage("./Index");
        }
    }
}
