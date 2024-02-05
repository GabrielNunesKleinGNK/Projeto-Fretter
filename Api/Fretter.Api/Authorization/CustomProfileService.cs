using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Fretter.Api.Authorization;
using Fretter.Domain.Interfaces.Applications;
using Fretter.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Fretter.Domain.Enum;

namespace Fretter.Api.Authorization
{
    public class CustomProfileService : IProfileService
    {
        private readonly IUsuarioApplication<FretterContext> _application;

        public CustomProfileService(IUsuarioApplication<FretterContext> application)
        {
            _application = application;
        }


        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();

            var claimsExterno = context.Subject.Claims.ToList();
            var usuarioTipo = (EnumUsuarioTipo)Convert.ToInt32(claimsExterno.FirstOrDefault(x => x.Type == "usuario_tipo_id").Value);

            if (usuarioTipo == EnumUsuarioTipo.Impersonate)
            {
                var idUsuarioReal = claimsExterno.FirstOrDefault(x => x.Type == "id_usuario_real")?.Value;
                var isAdmin = claimsExterno.FirstOrDefault(x => x.Type == "is_admin")?.Value;

                var user = new Domain.Entities.Usuario
                (
                    id: Convert.ToInt32(sub),
                    nome: claimsExterno.FirstOrDefault(x => x.Type == "user_email").Value,
                    login: claimsExterno.FirstOrDefault(x => x.Type == "user_email").Value,
                    email: claimsExterno.FirstOrDefault(x => x.Type == "user_email").Value,
                    usuarioTipoId: EnumUsuarioTipo.Impersonate.GetHashCode(),
                    idUsuarioReal: string.IsNullOrEmpty(idUsuarioReal)?(int?)null:Convert.ToInt32(idUsuarioReal),
                    empresaId: Convert.ToInt32(claimsExterno.FirstOrDefault(x => x.Type == "id_empresa").Value),
                    isAdmin:string.IsNullOrEmpty(isAdmin)?false: Convert.ToBoolean(isAdmin)
                );;
                var claims = AuthorizationConfig.GetClaims(user);
                context.IssuedClaims = claims.ToList();
            }
            else
            {
                var user = _application.Get(Convert.ToInt32(sub));
                var claims = AuthorizationConfig.GetClaims(user);
                context.IssuedClaims = claims.ToList();
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var claimsExterno = context.Subject.Claims.ToList();
            var usuarioTipo = (EnumUsuarioTipo)Convert.ToInt32(claimsExterno.FirstOrDefault(x => x.Type == "usuario_tipo_id").Value);

            if (!(usuarioTipo == EnumUsuarioTipo.Impersonate))
            {
                var user = _application.Get(Convert.ToInt32(sub));
                context.IsActive = user != null;
            }
            else context.IsActive = true;
        }
    }
}
