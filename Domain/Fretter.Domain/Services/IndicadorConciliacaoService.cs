using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class IndicadorConciliacaoService<TContext> : ServiceBase<TContext, IndicadorConciliacao>, IIndicadorConciliacaoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IIndicadorConciliacaoRepository<TContext> _repository;
        public IndicadorConciliacaoService(IIndicadorConciliacaoRepository<TContext> repository, IUnitOfWork<TContext> unitOfWork, IUsuarioHelper user) : base(repository, unitOfWork, user)
        {
            _repository = repository;
        }
        public int ProcessaIndicadorConciliacao()
        {
            return _repository.ProcessaIndicadorConciliacao();
        }
    }
}
