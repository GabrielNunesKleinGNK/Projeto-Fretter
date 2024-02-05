using Fretter.Api.Models;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IFaturaHistoricoApplication<TContext> : IApplicationBase<TContext, FaturaHistorico>
        where TContext : IUnitOfWork<TContext>
    {
        List<FaturaHistorico> GetHistoricoDeFaturasPorEmpresa(int faturaId);
    }
}
