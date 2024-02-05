using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Linq;
using Fretter.Api.Providers;

namespace Fretter.Api.Middleware
{
    public static class InternationalizationMiddleware
    {
        public static IApplicationBuilder ConfigurararIdiomas(this IApplicationBuilder app, params string[] idiomas)
        {
            if (!idiomas.Any()) idiomas = new string[] { "pt-BR" };

            app.UseRequestLocalization();
            string idiomaPadrao = idiomas.FirstOrDefault();
            var supportedCultures = idiomas.Select(x => new CultureInfo(x)).ToArray();
            var requestLocalizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(idiomaPadrao),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };
            requestLocalizationOptions.RequestCultureProviders.Insert(0, new UrlRequestCultureProvider());
            app.UseRequestLocalization(requestLocalizationOptions);
            return app;
        }
    }
}
