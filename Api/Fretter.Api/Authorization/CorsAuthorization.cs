using IdentityServer4.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fretter.Api.Authorization
{
    public static class CorsAuthorization
    {
        public static void SetAllowAll(IServiceCollection services)
        {
            services.AddTransient<ICorsPolicyService>(p =>
            {
                var corsService = new DefaultCorsPolicyService(p.GetRequiredService<ILogger<DefaultCorsPolicyService>>());
                corsService.AllowAll = true;
                return corsService;
            });
        }
    }
}
