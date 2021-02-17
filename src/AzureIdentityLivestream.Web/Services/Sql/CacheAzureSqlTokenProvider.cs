using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace AzureIdentityLivestream.Web.Services.Sql
{
    public class CacheAzureSqlTokenProvider : IAzureSqlTokenProvider
    {
        private static readonly string _cacheKey = $"{nameof(CacheAzureSqlTokenProvider)}.{nameof(GetAccessTokenAsync)}";

        private readonly IAzureSqlTokenProvider _inner;
        private readonly IMemoryCache _cache;

        public CacheAzureSqlTokenProvider(IAzureSqlTokenProvider inner, IMemoryCache cache)
        {
            _inner = inner;
            _cache = cache;
        }

        public async Task<(string Token, DateTimeOffset ExpiresOn)> GetAccessTokenAsync(CancellationToken cancellationToken)
        {
            return await _cache.GetOrCreateAsync(_cacheKey, async entry =>
            {
                var (token, expiresOn) = await _inner.GetAccessTokenAsync(cancellationToken);

                entry.SetAbsoluteExpiration(expiresOn);

                return (token, expiresOn);
            });
        }
    }
}
