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

namespace eStoreClient.Pages.OrderDetailsView
{
    public class DetailsModel : PageModel
    {

        public IOrderDetailRepository repository = new OrderDetailRepository();
        public DetailsModel()
        {
            
        }

      public OrderDetail OrderDetail { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int id)
        {


            var orderdetail = repository.GetOrderDetailById(id);
            if (orderdetail == null)
            {
                return NotFound();
            }
            else 
            {
                OrderDetail = orderdetail;
            }
            return Page();
        }
    }
}
