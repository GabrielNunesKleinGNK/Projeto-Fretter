using IdentityModel;
using IdentityServer4;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using Fretter.Domain.Entities;
using Fretter.IoC;
using Fretter.Domain.Extensions;

namespace Fretter.Api.Authorization
{
    public static class AuthorizationConfig
    {
        private static readonly AuthorizationConfigs _config = ServiceLocator.Resolve<IOptions<AuthorizationConfigs>>().Value;
        private static readonly AuthorizationPortalConfigs _configPortal = ServiceLocator.Resolve<IOptions<AuthorizationPortalConfigs>>().Value;

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource( _config.ClientId,  _config.ClientId){Scopes = {"portal", "mobile", "offline_access", "portal offline_access" },}
            };
        }

        public static IEnumerable<Client> GetClients() => new List<Client>
        {
            new Client
            {
                ClientId = _config.ClientId,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets ={ new Secret(_config.Secret.Sha256()) },
                AllowedScopes = {"portal","mobile",_config.ClientId,"offline_access","portal offline_access",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OfflineAccess},
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowOfflineAccess = true,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                AccessTokenLifetime = 86400 * 5,
                IdentityTokenLifetime = 86400 * 5,
                AlwaysSendClientClaims = true,
                UpdateAccessTokenClaimsOnRefresh=true,
            },
        };


        public static RsaSecurityKey GetCertificate()
        {
            var filename = Path.Combine(Directory.GetCurrentDirectory(), "tempkey.rsa");

            if (File.Exists(filename))
            {
                var keyFile = File.ReadAllText(filename);
                var tempKey = JsonConvert.DeserializeObject<TemporaryRsaKey>(keyFile, new JsonSerializerSettings { ContractResolver = new RsaKeyContractResolver() });

                return CreateRsaSecurityKey(tempKey.Parameters, tempKey.KeyId);
            }
            else
            {
                var key = CreateRsaSecurityKey();
                RSAParameters parameters;

                if (key.Rsa != null)
                    parameters = key.Rsa.ExportParameters(includePrivateParameters: true);
                else
                    parameters = key.Parameters;

                var tempKey = new TemporaryRsaKey
                {
                    Parameters = parameters,
                    KeyId = key.KeyId
                };

                File.WriteAllText(filename, JsonConvert.SerializeObject(tempKey, new JsonSerializerSettings { ContractResolver = new RsaKeyContractResolver() }));
                return key;
            }
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope(name: "portal", displayName: "portal"),
                new ApiScope(name: _config.ClientId,displayName: _config.ClientId)
            };
        }

        public static IdentityServerAuthenticationOptions GetServerAutentication(IdentityServerAuthenticationOptions options)
        {
            options.ApiName = _config.ClientId;
            options.Authority = _config.AuthUrl;
            options.RequireHttpsMetadata = false; // only for development
            options.ApiSecret = _config.Secret;
            options.SupportedTokens = SupportedTokens.Both;

            return options;
        }

        public static OAuthOptions GetFusionAutentication(OAuthOptions fusion)
        {
            fusion.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            fusion.ClientId = _config.FusionClientId;
            fusion.ClientSecret = _config.FusionSecret;
            fusion.CallbackPath = new PathString(_config.FusionUrlRetorno);
            fusion.AuthorizationEndpoint = _config.FusionAuthorizationEndpoint;
            fusion.TokenEndpoint = _config.FusionTokenEndpoint;
            fusion.UserInformationEndpoint = _config.FusionUserInformationEndpoint;
            fusion.SaveTokens = true;
            fusion.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
            fusion.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
            fusion.ClaimActions.MapJsonKey("user_email", "user_email");
            fusion.ClaimActions.MapJsonKey("id_empresa", "id_empresa");
            fusion.ClaimActions.MapJsonKey("id_usuario_real", "id_usuario_real");
            fusion.ClaimActions.MapJsonKey("usuario_tipo_id", "usuario_tipo_id");
            fusion.ClaimActions.MapJsonKey(ClaimTypes.Role, "roles");
            fusion.Events = new OAuthEvents
            {
                OnCreatingTicket = async context =>
                {
                    // Get the GitHub user
                    var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await context.Backchannel.SendAsync(request, context.HttpContext.RequestAborted);
                    response.EnsureSuccessStatusCode();

                    var json = System.Text.Json.JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                    context.RunClaimActions(json.RootElement);
                }
            };
            return fusion;
        }

        public static Claim[] GetClaims(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(JwtClaimTypes.Role, usuario.Email),
                new Claim(JwtClaimTypes.Id, usuario.Id.ToString()),
                new Claim(JwtClaimTypes.Email, usuario.Email),
                new Claim(JwtClaimTypes.Issuer, _config.AuthUrl),
                new Claim(JwtClaimTypes.Audience, _config.ClientId),
                new Claim("user_email",usuario.Login.ToString()),
                new Claim("id_empresa",usuario.EmpresaId.ToString()),
                new Claim("usuario_tipo_id",usuario.UsuarioTipoId.ToString()),
                new Claim("id_usuario_real",usuario.IdUsuarioReal==null?"0": usuario.IdUsuarioReal.ToString()),
                new Claim("is_admin",usuario.IsAdmin.ToString())
            };

            return claims;
        }
        private static RsaSecurityKey CreateRsaSecurityKey()
        {
            var rsa = RSA.Create();
            RsaSecurityKey key;

            if (rsa is RSACryptoServiceProvider)
            {
                rsa.Dispose();
                var cng = new RSACng(2048);

                var parameters = cng.ExportParameters(includePrivateParameters: true);
                key = new RsaSecurityKey(parameters);
            }
            else
            {
                rsa.KeySize = 2048;
                key = new RsaSecurityKey(rsa);
            }

            key.KeyId = CryptoRandom.CreateUniqueId(16);
            return key;
        }

        private static RsaSecurityKey CreateRsaSecurityKey(RSAParameters parameters, string id)
        {
            var key = new RsaSecurityKey(parameters)
            {
                KeyId = id
            };
            return key;
        }
        private class RsaKeyContractResolver : DefaultContractResolver
        {
            protected override Newtonsoft.Json.Serialization.JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var property = base.CreateProperty(member, memberSerialization);

                property.Ignored = false;

                return property;
            }
        }
    }
}
