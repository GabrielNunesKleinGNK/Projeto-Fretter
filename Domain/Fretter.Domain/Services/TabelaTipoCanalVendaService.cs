using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class TabelaTipoCanalVendaService<TContext> : ServiceBase<TContext, TabelaTipoCanalVenda>, ITabelaTipoCanalVendaService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly ITabelaTipoCanalVendaRepository<TContext> _Repository;

        public TabelaTipoCanalVendaService(ITabelaTipoCanalVendaRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
