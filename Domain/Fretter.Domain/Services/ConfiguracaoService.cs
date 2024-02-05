using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class ConfiguracaoService<TContext> : ServiceBase<TContext, Configuracao>, IConfiguracaoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {

        public ConfiguracaoService(IConfiguracaoRepository<TContext> repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(repository, unitOfWork, user)
        {
        }
    }
}	
