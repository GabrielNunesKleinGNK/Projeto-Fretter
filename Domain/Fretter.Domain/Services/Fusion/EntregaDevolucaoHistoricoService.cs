using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Fretter.Domain.Services
{
    public class EntregaDevolucaoHistoricoService<TContext> : ServiceBase<TContext, EntregaDevolucaoHistorico>, IEntregaDevolucaoHistoricoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEntregaDevolucaoHistoricoRepository<TContext> _Repository;

        public EntregaDevolucaoHistoricoService(IEntregaDevolucaoHistoricoRepository<TContext> Repository, 
            IUnitOfWork<TContext> unitOfWork, IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }

        public async Task<List<EntregaDevolucaoHistorico>> ObterHistoricoEntregaDevolucao(int entregaDevolucaoId)
        {
            return _Repository.ObterHistoricoEntregaDevolucao(entregaDevolucaoId);
        }
    }
}
