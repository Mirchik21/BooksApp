using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace Web.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddWebConfiguration(this IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });

            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            return services;
        }
    }
}