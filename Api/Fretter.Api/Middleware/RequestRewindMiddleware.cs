using Microsoft.AspNetCore.Http;

using System.IO;
using System.Threading.Tasks;

namespace Fretter.Api.Middleware
{
    public class RequestRewindMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestRewindMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            PreencherBodyRequest(context.Request);

            await _next(context);
        }

        private void PreencherBodyRequest(HttpRequest request)
        {
            request.EnableBuffering();

            string bodyContent = new StreamReader(request.Body).ReadToEnd();
            request.Body.Position = 0;
            if (!string.IsNullOrWhiteSpace(bodyContent))
                request.HttpContext.Items.Add("body", bodyContent);
        }
    }
}
