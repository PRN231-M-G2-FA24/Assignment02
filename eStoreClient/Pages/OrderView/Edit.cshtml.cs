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
using BusinessObject;

namespace eStoreClient.Pages.OrderView
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;
        public IOrderRepository repository = new OrderRepository();
        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if ( _context.Orders == null)
            {
                return NotFound();
            }

            var order = repository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            Order = order;
           ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormCollection collection)
        {
           

            try
            {
                Order.MemberId = int.Parse(collection["MemberId"]);
               repository.UpdateOrder(Order);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(Order.OrderId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
