using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.Metrics;
using DataAccess.DTO;
using BusinessObject;
using Repository;
using Repository.Implements;

namespace eStoreClient.Pages.MemberView
{
    public class CreateModel : PageModel
    {
        public IMemberRepository repository = new MemberRepository();
        

        public CreateModel()
        {
           
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Member Member { get; set; } = default!;




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync(IFormCollection collection)
        {
            if (!ModelState.IsValid || collection == null)
            {
                return Page();
            }
            MemberDTO member=new MemberDTO();
            member.Email= collection["Email"];
            member.CompanyName = collection["CompanyName"];
            member.City = collection["City"];
            member.Country = collection["Country"];
            member.Password = collection["Password"];
            Member mem = new Member
            {

                Email = member.Email,
                CompanyName = member.CompanyName,
                City = member.City,
                Country = member.Country,
                Password = member.Password
            };
            repository.SaveMember(mem);
           

            return RedirectToPage("./Index");
        }
    }
}
