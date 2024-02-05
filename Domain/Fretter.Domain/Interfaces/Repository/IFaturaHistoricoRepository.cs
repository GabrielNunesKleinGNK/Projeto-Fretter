using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Fretter.Domain.Interfaces.Repository;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IFaturaHistoricoRepository<TContext> : IRepositoryBase<TContext, FaturaHistorico>
        where TContext : IUnitOfWork<TContext>
    {
        List<FaturaHistorico> GetHistoricoDeFaturasPorEmpresa(int faturaId);
    }
}

