
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Fretter.WebHook.Api.Helpers;
using Fretter.WebHook.Api.Middleware;
using Fretter.Domain.Interfaces;
using Fretter.IoC;
using Fretter.Repository.Contexts;
using Microsoft.Extensions.Caching.Memory;
using Fretter.Domain.Interfaces.Service.Webhook;
using Fretter.Domain.Services.Webhook;
using Fretter.Domain.Interfaces.Service;
using Fretter.Domain.Services;
using Fretter.Domain.Config.WebHook;

namespace Fretter.WebHook.Api
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
            services.AddHttpContextAccessor();
            FretterCoreIoc.Initialize(services, Configuration);

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<IUsuarioHelper, UsuarioHelper>();
            services.AddScoped(typeof(ICacheService<>), typeof(CacheService<>));
            services.AddSingleton(typeof(IMessageBusService<>), typeof(MessageBusService<>));
            services.Configure<WebHookConfig>(Configuration.GetSection("WebHookConfig"));
            services.Configure<TrackingIntegracaoConfig>(Configuration.GetSection("TrackingIntegracaoConfig"));

            ServiceLocator.Init(services.BuildServiceProvider());
            services.AddMemoryCache();
            services.AddMvc();

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            services.AddApplicationInsightsTelemetry(Configuration["ApplicationInsights:InstrumentationKey"]);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ICacheService<FretterContext> cacheService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            cacheService.InicializaCache();
            //app.UseMiddleware<ApiKeyMiddleware>();
            //app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.UseCors("EnableAll");

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
