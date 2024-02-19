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

namespace eStoreClient.Pages.OrderView
{
    public class DeleteModel : PageModel
    {
        public IOrderRepository repository = new OrderRepository();

        public DeleteModel()
        {
            
        }

        [BindProperty]
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
          
            var order = repository.GetOrderById(id);

            if (order != null)
            {
                Order = order;
                repository.DeleteOrder(Order);
             
            }

            return RedirectToPage("./Index");
        }
    }
}
