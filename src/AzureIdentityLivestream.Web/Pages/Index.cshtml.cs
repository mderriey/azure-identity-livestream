using System.Collections.Generic;
using System.Threading.Tasks;
using AzureIdentityLivestream.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureIdentityLivestream.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IPersonProvider _personProvider;

        public IndexModel(IPersonProvider personProvider)
        {
            _personProvider = personProvider;
        }

        public async Task OnGet()
        {
            People = await _personProvider.GetPeople();
            PersonProviderTypeName = _personProvider.GetType().Name;
        }

        public IEnumerable<Person> People { get; set; }
        public string PersonProviderTypeName { get; set; }
    }
}
