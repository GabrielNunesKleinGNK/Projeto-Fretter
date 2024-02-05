using Fretter.Domain.Dto.EmpresaIntegracao.EmpresaIntegracaoItemDetalhe;
using Fretter.Domain.Dto.Fusion;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IEmpresaIntegracaoItemDetalheApplication<TContext> : IApplicationBase<TContext, EmpresaIntegracaoItemDetalhe>
        where TContext : IUnitOfWork<TContext>
    {
        bool ReprocessarLote(List<long> ids);
        EmpresaIntegracaoItemDetalhe ObterStatusReprocessamento(int id);
        List<EmpresaIntegracaoItemDetalheDto> ObterDados(EmpresaIntegracaoItemDetalheFiltro filtro);
    }
}
