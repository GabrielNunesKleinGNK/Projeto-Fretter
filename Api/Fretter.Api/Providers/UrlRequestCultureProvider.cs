using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System.Threading.Tasks;

namespace Fretter.Api.Providers
{
    public class UrlRequestCultureProvider : IRequestCultureProvider
    {
        public Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            //var culture = httpContext.ObterIdioma();
            return Task.FromResult(new ProviderCultureResult("pt-br"));
        }
    }
}
