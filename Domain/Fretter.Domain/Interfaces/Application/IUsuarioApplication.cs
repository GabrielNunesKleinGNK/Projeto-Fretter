using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Application;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Applications
{
    public interface IUsuarioApplication<TContext> : IApplicationBase<TContext, Usuario>
        where TContext : IUnitOfWork<TContext>
    {
        void AlterarSenha(int usuarioId, string senhaAtual, string senhaNova);
        void RecuperarSenha(string login);
        void NovaSenha(string hash, string senha);
        Usuario ObterUsuarioPorLogin(string login);
        void SavePermissoes(int usuarioId, int[] menus);
        IEnumerable<SistemaMenu> GetMenus(int usuarioId);
        IEnumerable<Fretter.Domain.Dto.Dashboard.Canal> GetCanaisUsuario();
    }
}
