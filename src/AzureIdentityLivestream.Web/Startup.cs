using System;
using Azure.Identity;
using Azure.Storage.Blobs;
using AzureIdentityLivestream.Web.Services;
using AzureIdentityLivestream.Web.Services.AzureBlobStorage;
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

            services.AddSingleton(_ =>
            {
                var storageConnectionString = Configuration.GetValue<string>("StorageConnectionString");
                if (Uri.TryCreate(storageConnectionString, UriKind.Absolute, out var storageEndpointUri))
                {
                    var credential = new ChainedTokenCredential(
                        new ManagedIdentityCredential(),
                        new VisualStudioCodeCredential());

                    return new BlobServiceClient(storageEndpointUri, credential);
                }

                return new BlobServiceClient(storageConnectionString);
            });

            services.AddSingleton<IPersonProvider, AzureBlobStoragePersonProvider>();

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
