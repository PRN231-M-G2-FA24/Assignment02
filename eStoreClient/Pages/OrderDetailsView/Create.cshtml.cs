using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using Repository.Implements;
using BusinessObject;

namespace eStoreClient.Pages.OrderDetailsView
{
    public class CreateModel : PageModel
    {
        public IOrderDetailRepository repository = new OrderDetailRepository();


        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
        ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return Page();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormCollection collection)
        {
            int OrderId = int.Parse(collection["OrderId"]);
            int ProductId = int.Parse(collection["ProductId"]);

            OrderDetail.OrderId=OrderId;
            OrderDetail.ProductId=ProductId;
            repository.SaveOrderDetail(OrderDetail);

            return RedirectToPage("./Index");
        }
    }
}
