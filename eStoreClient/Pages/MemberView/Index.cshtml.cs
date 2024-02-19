using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Implements;
using System.Net.Http.Headers;
using System.Text.Json;
using BusinessObject;

namespace eStoreClient.Pages.MemberView
{
    public class IndexModel : PageModel
    {
        public IMemberRepository repository = new MemberRepository();
        public IEnumerable<Member> pro { get; set; }

        private readonly HttpClient client = null;
        private string MemberApiUlr = "";



        public IndexModel()
        {
            

            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            MemberApiUlr = "https://localhost:7124/api/Members";
        }

        public IList<Member> Member { get;set; } = default!;

        public async Task OnGetAsync()
        {
            HttpResponseMessage respone = await client.GetAsync(MemberApiUlr);
            string strData = await respone.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Member = (IList<Member>)JsonSerializer.Deserialize<List<Member>>(strData, options);
            
        }
    }
}
