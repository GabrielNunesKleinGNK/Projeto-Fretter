using Fretter.Domain.Entities.Fretter;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IAgendamentoExpedicaoRepository<TContext> : IRepositoryBase<TContext, AgendamentoExpedicao>
        where TContext : IUnitOfWork<TContext>
    {
        List<dynamic> VerificaCadastroCredenciais(int idEmpresa, string idTransportador);
    }
}
