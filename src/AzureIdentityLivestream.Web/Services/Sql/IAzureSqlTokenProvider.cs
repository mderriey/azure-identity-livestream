using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureIdentityLivestream.Web.Services.Sql
{
    public interface IAzureSqlTokenProvider
    {
        (string Token, DateTimeOffset ExpiresOn) GetAccessToken();
        Task<(string Token, DateTimeOffset ExpiresOn)> GetAccessTokenAsync(CancellationToken cancellationToken = default);
    }
}
