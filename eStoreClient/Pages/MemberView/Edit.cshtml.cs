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
using DataAccess.DTO;
using BusinessObject;

namespace eStoreClient.Pages.MemberView
{
    public class EditModel : PageModel
    {
        public IMemberRepository repository = new MemberRepository();
        public IEnumerable<Member> pro { get; set; }

        private readonly HttpClient client = null;
        private string MemberApiUlr = "";

        public EditModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            MemberApiUlr = "https://localhost:7124/api/Members";

        }

        [BindProperty]
        public Member Member { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            MemberApiUlr += "/" + id;
            HttpResponseMessage respone = await client.GetAsync(MemberApiUlr);
            string strData = await respone.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Member = JsonSerializer.Deserialize<Member>(strData, options);
            return Page();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync(IFormCollection collection)
        {
            int id = int.Parse(collection["MemberId"]);
            var mTmp = repository.GetMemberById(id);
            var member = repository.GetMemberById(int.Parse(collection["MemberId"]));

            member.Email = collection["Email"];
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
            if (mTmp == null)
            return NotFound();

            mTmp = mem;
            mTmp.MemberId = id;

            repository.UpdateMember(mTmp);


            return RedirectToPage("./Index");
        }

        
    }
}
