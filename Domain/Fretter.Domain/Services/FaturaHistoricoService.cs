using Fretter.Domain.Dto.Fatura;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Services
{
    public class FaturaHistoricoService<TContext> : ServiceBase<TContext, FaturaHistorico>, IFaturaHistoricoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        private readonly IFaturaHistoricoRepository<TContext> _repository;

        public FaturaHistoricoService(IFaturaHistoricoRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork, IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _user = user;
            _repository = Repository;
        }

        public async Task<List<FaturaHistorico>> GetHistoricoDeFaturasPorEmpresa(int faturaId)
        {
            return _repository.GetHistoricoDeFaturasPorEmpresa(faturaId);
        }
    }
}
