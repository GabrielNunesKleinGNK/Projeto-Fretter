using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text.RegularExpressions;

namespace System
{
    public static class HttpContextExtensions
    {
        const string IdiomaPadrao = "pt-BR";

        public static string ObterIdioma(this HttpContext httpContext)
        {
            var url = httpContext.Request.Path;
            var parts = httpContext.Request.Path.Value
                         .Split('/')
                         .Where(p => !String.IsNullOrWhiteSpace(p))
                         .ToList();

            if (!parts.Any())
                return IdiomaPadrao;

            if (!Regex.IsMatch(parts[0], @"^[a-z]{2}(?:-[A-Z]{2})?$"))
                return IdiomaPadrao;

            return parts[0];
        }
    }
}
