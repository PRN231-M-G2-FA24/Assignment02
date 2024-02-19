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

namespace eStoreClient.Pages.OrderView
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        public IOrderRepository repository = new OrderRepository();
        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId");
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormCollection collection)
        {
            int MemberId = int.Parse(collection["MemberId"]);
          
            Order.MemberId = MemberId;
            repository.SaveOrder(Order);

            return RedirectToPage("./Index");
        }
    }
}
