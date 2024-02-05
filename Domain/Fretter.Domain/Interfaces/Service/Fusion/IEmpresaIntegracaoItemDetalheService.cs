using Fretter.Domain.Dto.EmpresaIntegracao.EmpresaIntegracaoItemDetalhe;
using Fretter.Domain.Dto.Fusion;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEmpresaIntegracaoItemDetalheService<TContext> : IServiceBase<TContext, EmpresaIntegracaoItemDetalhe>
        where TContext : IUnitOfWork<TContext>
    {
        public bool ReprocessarLote(List<long> ids);
        public EmpresaIntegracaoItemDetalhe ObterStatusReprocessamento(int id);
        List<EmpresaIntegracaoItemDetalheDto> ObterDados(EmpresaIntegracaoItemDetalheFiltro filtro);
    }
}
