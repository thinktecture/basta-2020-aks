using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Thinktecture.AKS.Sample.Configuration;
using Thinktecture.AKS.Sample.Services;

namespace Thinktecture.AKS.Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var config = new BastaConfiguration();
            var configSection = Configuration.GetSection(BastaConfiguration.SECTION_NAME);
            configSection.Bind(config);
            services.AddSingleton(config);

            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton<ProductsService>();

            services.AddResponseCompression(options =>
            {
                options.Providers.Clear();
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.Configure<GzipCompressionProviderOptions>(config =>
            {
                config.Level = System.IO.Compression.CompressionLevel.Optimal;
            });
            services.AddControllers();
            // Add Health Checks
            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseResponseCaching();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // Map liveness and readiness
                endpoints.MapHealthChecks("readiness");
                endpoints.MapHealthChecks("liveness");

            });
        }
    }
}
