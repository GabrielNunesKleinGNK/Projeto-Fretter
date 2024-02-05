using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Services
{
    public interface IUsuarioService<TContext> : IServiceBase<TContext, Usuario> where TContext : IUnitOfWork<TContext>
    {
        void AlterarSenha(int usuarioId, string senhaAtual, string senhaNova);
        Usuario ObterUsuarioPorLogin(string login);
        Usuario RecuperarSenha(string login);
        Usuario NovaSenha(string hash, string senha);
        void RemoverPermissoes(int usuarioId);
        void CadastrarPermissoes(int usuarioId, int[] menus);
        IEnumerable<SistemaMenu> GetMenus(int usuarioId);
        void CadastrarPermissoesUsuarioAdmin(int id);
        IEnumerable<Fretter.Domain.Dto.Dashboard.Canal> GetCanaisUsuario();
    }
}
