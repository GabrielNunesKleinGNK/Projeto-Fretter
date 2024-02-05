using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Fretter.Api.Cors
{
    public static class DefaultCorsConfiguration
    {
        private const string ALLOW_ALL = "AllowAll";
        public static void SetAllowAll(IServiceCollection services)
        {
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();
            services.AddCors(options =>
            {
                options.AddPolicy(ALLOW_ALL, corsBuilder.Build());
            });

            //services.Configure<MvcOptions>(options =>
            //{
            //    options.Filters.Add(new CorsAuthorizationFilterFactory(ALLOW_ALL));
            //});
        }

        public static void Use(IApplicationBuilder app) => app.UseCors(ALLOW_ALL);
    }
}
