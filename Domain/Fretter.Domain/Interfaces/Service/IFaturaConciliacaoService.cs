using Fretter.Domain.Dto.Conciliacao;
using Fretter.Domain.Dto.Fatura;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IFaturaConciliacaoService<TContext> : IServiceBase<TContext, FaturaConciliacao>
        where TContext : IUnitOfWork<TContext>
    {
        RetornoFaturaConciliacaoIntegracaoDTO GetAllFaturaConciliacaoIntegracao(int faturaId);
        JsonIntegracaoFaturaConciliacaoDTO GetJsonIntegracaoFaturaConciliacao(int empresaIntegracaoItemDetalheId);
        RetornoReenvioFaturaConciliacaoDTO ReenviarFaturaConciliacaoIndividual(FaturaConciliacaoReenvioDTO conciliacaoReenvio);
        RetornoReenvioFaturaConciliacaoDTO ReenviarFaturaConciliacaoMassivo(List<FaturaConciliacaoReenvioDTO> lstConciliacaoReenvio);
    }
}
