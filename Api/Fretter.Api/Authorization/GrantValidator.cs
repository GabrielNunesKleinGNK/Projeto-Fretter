using IdentityServer4.Models;
using IdentityServer4.Validation;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Applications;
using Fretter.Repository.Contexts;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.Api.Authorization
{
    public class GrantValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUsuarioApplication<FretterContext> _usuarioApplication;
        private readonly AuthorizationConfigs _config;
        private readonly AuthorizationPortalConfigs _configPortal;


        public GrantValidator(IUsuarioApplication<FretterContext> usuarioApplication, IOptions<AuthorizationConfigs> config, IOptions<AuthorizationPortalConfigs> configMobile)
        {
            _usuarioApplication = usuarioApplication;
            this._config = config.Value;
            this._configPortal = configMobile.Value;
        }
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {

            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Validando Login");

                bool isScopeMobile = context.Request.RequestedScopes.FirstOrDefault() == "mobile" || context.Request.RequestedScopes.FirstOrDefault().Contains("FretterApi");

                Usuario usuario = _usuarioApplication.ObterUsuarioPorLogin(context.UserName);

                if (usuario != null)
                    Console.WriteLine($"Usuario Obtido do banco: " + usuario.Nome);

                if (usuario == null)
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.UnauthorizedClient, "Login inválido.");
                    return Task.FromResult(0);
                }

                var origemFusion = context.Request.Raw["origem"];
                usuario.AlterarOrigemFusion(origemFusion != null && origemFusion.Equals("fusion"));

                if (usuario.Senha != context.Password)
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.UnauthorizedClient, "Senha inválida.");
                    return Task.FromResult(0);
                }

                if((usuario.UsuarioTipoId  != 1 && usuario.UsuarioTipoId != 2) && !isScopeMobile)
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.UnauthorizedClient, "Usuário sem autorização para acessar o portal.");
                    return Task.FromResult(0);
                }

                context.Result = new GrantValidationResult(subject: usuario.Id.ToString(), authenticationMethod: "custom", claims: AuthorizationConfig.GetClaims(usuario));
                Console.WriteLine($"Usuario no Claims " + usuario.Nome);
                Console.ResetColor();
                return Task.FromResult(0);
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient, $"{ex.Message}");
                return Task.FromResult(1);
            } 
        }
    }
}
