using AzureIdentityLivestream.Web.Services;
using AzureIdentityLivestream.Web.Services.Sql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AzureIdentityLivestream.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();

            services.AddMemoryCache();
            services.AddSingleton<IAzureSqlTokenProvider, AzureIdentityAzureSqlTokenProvider>();
            services.Decorate<IAzureSqlTokenProvider, CacheAzureSqlTokenProvider>();

            services.AddSingleton(provider =>
            {
                var sqlConnectionString = Configuration.GetValue<string>("SqlConnectionString");
                return new SqlConnectionFactory(sqlConnectionString, provider.GetRequiredService<IAzureSqlTokenProvider>());
            });

            services.AddSingleton<IPersonProvider, DapperPersonProvider>();

            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
