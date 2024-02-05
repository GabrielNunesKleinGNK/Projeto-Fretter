using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Fretter.WebHook.Api.Helpers;
using Fretter.WebHook.Api.Models;
using Fretter.Domain.Interfaces.Repositories;
using Fretter.IoC;
using Fretter.Repository.Contexts;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fretter.WebHook.Api.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly ApiKeyMemoryCache _cache;
        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "Token";
        public ApiKeyMiddleware(RequestDelegate next, ApiKeyMemoryCache cache)
        {
            _cache = cache;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            StringValues extractedApiKey = "";

            extractedApiKey = context.Request.Query["Token"].FirstOrDefault();

            if (string.IsNullOrEmpty(extractedApiKey) && !context.Request.Headers.TryGetValue(APIKEYNAME, out extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token was not provided. (Using ApiKeyMiddleware) ");
                return;
            }

            var apikeyCache = _cache.Get(extractedApiKey);
           
            var apiKeyEmCache = string.IsNullOrEmpty(apikeyCache) ? false : true;

            User user = null;

            if (apiKeyEmCache)
            {
                user = JsonConvert.DeserializeObject<User>(apikeyCache);
            }
            else
            {
                var usuariorepository = ServiceLocator.Resolve<IUsuarioRepository<FretterContext>>();

                var userDomain = usuariorepository.ObterUsuarioPorApiKey(extractedApiKey);

                if (userDomain != null)
                {
                    user = new User()
                    {
                        Id = userDomain.Id,
                        ClienteId = 1,
                        UsuarioTipoId = userDomain.UsuarioTipoId,
                        Email = userDomain.Email,
                        Nome = userDomain.Nome,
                        Login = userDomain.Login,
                        ApiKey = userDomain.ApiKey
                    };
                }
            }

            if (user is null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client. (Using ApiKeyMiddleware)");
                return;
            }

            if (!apiKeyEmCache)
            {
                _cache.Set(extractedApiKey, JsonConvert.SerializeObject(user));
            }

            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("clienteId", user.ClienteId.ToString()),
                new Claim("usuarioTipoId", user.UsuarioTipoId.ToString()),
                new Claim("email", user.Login.ToString()),
             });

            context.User = new System.Security.Claims.ClaimsPrincipal();
            context.User.AddIdentity(claimsIdentity);

            await _next(context);
        }
    }
}
