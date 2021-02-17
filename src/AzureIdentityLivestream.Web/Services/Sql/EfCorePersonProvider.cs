using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AzureIdentityLivestream.Web.Services.Sql
{
    public class EfCorePersonProvider : IPersonProvider
    {
        private readonly LivestreamContext _livestreamContext;

        public EfCorePersonProvider(LivestreamContext livestreamContext)
        {
            _livestreamContext = livestreamContext;
        }

        public async Task<IEnumerable<Person>> GetPeople() => await _livestreamContext.People.ToListAsync();
    }
}
