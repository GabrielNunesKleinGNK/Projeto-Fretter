using Fretter.Api.Authorization;
using Fretter.Domain.Config;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Applications;
using Fretter.Repository.Contexts;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Configuration;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fretter.Api.Controllers
{
    [Route("api/[controller]")]
    [Route("{language:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class OAuthController : Controller
    {
        private readonly IUsuarioApplication<FretterContext> _usuarioApplication;
        private readonly ITokenService tokenService;
        private readonly IRefreshTokenService refreshTokenService;
        private readonly IdentityServerOptions _options;
        private readonly UrlsConfigs _urlConfigs;

        public OAuthController(IUsuarioApplication<FretterContext> usuarioApplication,
            ITokenService tokenService,
            IRefreshTokenService refreshTokenService,
            IdentityServerOptions options,
            IOptions<UrlsConfigs> urlConfigs)
        {
            this._usuarioApplication = usuarioApplication;
            this.tokenService = tokenService;
            this.refreshTokenService = refreshTokenService;
            this._options = options;
            this._urlConfigs = urlConfigs.Value;
        }
        /// <summary>
        /// initiate roundtrip to external authentication provider
        /// </summary>
        [HttpGet("ExternalLogin")]
        public async Task<IActionResult> ExternalLogin(string provider, string login, string route, bool isAdmin, int? idUsuarioReal)
        {
            //return Redirect($"{this._urlConfigs.Site}");

            var props = new AuthenticationProperties()
            {
                RedirectUri = Url.Action("ExternalLoginCallback"),
                Items =
                {
                    { "scheme", provider },
                    { "returnUrl", this._urlConfigs.Site },
                    { "loginFusion", login },
                    { "routeFretter", route },
                    { "idUsuarioReal", idUsuarioReal == null? null : idUsuarioReal.ToString() },
                    { "isAdmin", isAdmin.ToString() }
                }
            };

            return Challenge(props, provider);
        }

        /// <summary>
        /// Post processing of external authentication
        /// </summary>
        [HttpGet]
        [Route("/[action]")]
        public async Task<IActionResult> ExternalLoginCallback()
        {
            // read external identity from the temporary cookie
            var result = await HttpContext.AuthenticateAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);
            if (result?.Succeeded != true)
                throw new Exception("External authentication error");

            // retrieve claims of the external user
            var externalUser = result.Principal;
            if (externalUser == null)
                throw new Exception("External authentication error");


            var emailFusion = "";
            result.Properties.Items.TryGetValue("loginFusion", out emailFusion);

            var routeFretter = "";
            result.Properties.Items.TryGetValue("routeFretter", out routeFretter);

            var idUsuarioReal = "";
            result.Properties.Items.TryGetValue("idUsuarioReal", out idUsuarioReal);

            var keyIsAdmin = "";
            result.Properties.Items.TryGetValue("isAdmin", out keyIsAdmin);

            var isAdmin = true;
            if (!string.IsNullOrEmpty(keyIsAdmin))
                isAdmin = Convert.ToBoolean(keyIsAdmin);

            if (string.IsNullOrWhiteSpace(emailFusion))
                throw new Exception("email is null");

            // retrieve claims of the external user
            var claims = externalUser.Claims.ToList();

            // try to determine the unique id of the external user - the most common claim type for that are the sub claim and the NameIdentifier
            // depending on the external provider, some other claim type might be used
            var userIdClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var userNameClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            var emailExterno = claims.FirstOrDefault(x => x.Type == "user_email");
            var idEmpresa = claims.FirstOrDefault(x => x.Type == "id_empresa");

            if (userIdClaim == null || userNameClaim == null || emailFusion == null)
            {
                throw new Exception("Unknown userid");
            }

            //Obtendo ou criando usuario
            //var usuario = _usuarioApplication.ObterUsuarioPorLogin(emailFusion);

            var usuario = new Usuario(id: Convert.ToInt32(userIdClaim.Value), nome: userNameClaim.Value, login: emailExterno.Value, email: emailExterno.Value, usuarioTipoId: Domain.Enum.EnumUsuarioTipo.Impersonate.GetHashCode(), empresaId: Convert.ToInt32(idEmpresa.Value), idUsuarioReal: string.IsNullOrEmpty(idUsuarioReal) ? (int?)null: Convert.ToInt32(idUsuarioReal), isAdmin);

            //Obtendo RefreshToken
            var refreshToken = await GetTokenAsync(usuario);

            string returnUrl = $"{this._urlConfigs.Site}/login?refresh_token={refreshToken}&route={routeFretter}";
            // validate return URL and redirect back to authorization endpoint or a local page
            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            return Redirect("~/");
        }

        private Claim[] GetClaims(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(JwtClaimTypes.Id, usuario.Id.ToString()),
                new Claim(JwtClaimTypes.Role, usuario.Login),
                new Claim(JwtClaimTypes.Email, usuario.Email),
                new Claim(JwtClaimTypes.Subject, usuario.Id.ToString()),
                new Claim(JwtClaimTypes.AuthenticationTime, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString()),
                new Claim(JwtClaimTypes.AuthenticationMethod, "custom"),
                new Claim(JwtClaimTypes.IdentityProvider, "fusion"),
                new Claim("id_empresa",usuario.EmpresaId.ToString()),
                new Claim("user_email",usuario.Login.ToString()),
                new Claim("usuario_tipo_id",usuario.UsuarioTipoId.ToString()),
                new Claim("id_usuario_real",usuario.IdUsuarioReal.ToString() ),
                new Claim("is_admin",usuario.IsAdmin.ToString() ),
                new Claim(JwtClaimTypes.Audience, "FretterApi"),
                new Claim(JwtClaimTypes.Scope, "portal")
            };
            return claims;
        }

        private async Task<string> GetTokenAsync(Usuario usuario)
        {
            var claims = GetClaims(usuario);

            var identity = new ClaimsIdentity(IdentityServerAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaims(claims);
            var principal = new ClaimsPrincipal(identity);

            var Request = new TokenCreationRequest()
            {
                Subject = principal,
                IncludeAllIdentityClaims = true,
                ValidatedRequest = new ValidatedRequest
                {
                    Subject = principal,
                    Options = _options,
                    ClientClaims = claims
                },
                //Analisar os Resources se estão corretos
                ValidatedResources = new ResourceValidationResult(new Resources(null, AuthorizationConfig.GetApiResources(), AuthorizationConfig.GetApiScopes())),
            };

            Request.ValidatedRequest.SetClient(AuthorizationConfig.GetClients().First());
            var token = await tokenService.CreateAccessTokenAsync(Request);
            token.Issuer = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value;
            var refreshToken = await refreshTokenService.CreateRefreshTokenAsync(principal, token, AuthorizationConfig.GetClients().First());

            return refreshToken;
        }

    }
}
