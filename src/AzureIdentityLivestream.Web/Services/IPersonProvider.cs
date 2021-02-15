using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureIdentityLivestream.Web.Services
{
    public interface IPersonProvider
    {
        Task<IEnumerable<Person>> GetPeople();
    }
}
