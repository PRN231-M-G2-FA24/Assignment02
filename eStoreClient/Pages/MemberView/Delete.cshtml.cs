using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;
using BusinessObject;
using Repository;
using Repository.Implements;

namespace eStoreClient.Pages.MemberView
{
    public class DeleteModel : PageModel
    {
        public IMemberRepository repository = new MemberRepository();
        public IEnumerable<Member> pro { get; set; }

        private readonly HttpClient client = null;
        private string MemberApiUlr = "";
        public DeleteModel()
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var p = repository.GetMemberById(id);
            if (p == null)

                return NotFound();
            repository.DeleteMember(p);

            return RedirectToPage("./Index");
        }
    }
}
