using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository<TContext> : IRepositoryBase<TContext, Usuario>
        where TContext : IUnitOfWork<TContext>
    {
        bool ExisteUsuario(long id, string login);
        Usuario ObterUsuarioPorLogin(string login);
        Usuario ObterUsuarioPorApiKey(string apiKey);
        IEnumerable<Fretter.Domain.Dto.Dashboard.Canal> GetCanaisUsuario(int userId, int empresaId);
    }
}
