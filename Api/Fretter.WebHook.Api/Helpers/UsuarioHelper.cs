using Microsoft.AspNetCore.Http;
using Fretter.Domain.Entities;
using Fretter.Domain.Exceptions;
using Fretter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fretter.WebHook.Api.Helpers
{
    public class UsuarioHelper : IUsuarioHelper
    {
        private IHttpContextAccessor _context;
        private UsuarioIdentity _usuarioLogado;
        public UsuarioHelper(IHttpContextAccessor context) => _context = context;

        public UsuarioIdentity UsuarioLogado
        {
            get
            {
                if (_usuarioLogado != null) return this._usuarioLogado;

                ClaimsPrincipal user = _context.HttpContext.User;
                var id = user.Claims.FirstOrDefault(c => c.Type == "id");
                var email = user.Claims.FirstOrDefault(c => c.Type == "email");
                var empresaId = user.Claims.FirstOrDefault(c => c.Type == "id_empresa");
                var idUsuarioReal = user.Claims.FirstOrDefault(c => c.Type == "id_usuario_real");
                var usuarioPerfil = 1;
                var admin = user.Claims.FirstOrDefault(c => c.Type == "is_admin")?.Value;
                bool isAdmin = string.IsNullOrEmpty(admin)?false:Convert.ToBoolean(admin); 

                if (id == null || email == null)
                    throw new UsuarioExpiradoException(nameof(UsuarioIdentity), nameof(UsuarioLogado), "Usuário precisa refazer o login.");

                this._usuarioLogado = new UsuarioIdentity(Convert.ToInt32(id.Value), email.Value, Convert.ToInt32(idUsuarioReal.Value), Convert.ToInt32(empresaId.Value), usuarioPerfil, isAdmin); ;
                return this._usuarioLogado;
            }
        }
        UsuarioIdentity IUsuarioHelper.UsuarioLogado => UsuarioLogado;

    }
}
