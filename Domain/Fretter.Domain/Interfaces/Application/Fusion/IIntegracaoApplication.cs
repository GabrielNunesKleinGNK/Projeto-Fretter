using Fretter.Domain.Dto.EmpresaIntegracao;
using Fretter.Domain.Dto.Fusion;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IIntegracaoApplication<TContext> : IApplicationBase<TContext, Integracao>
        where TContext : IUnitOfWork<TContext>
    {
        List<DeParaEmpresaIntegracao> BuscaCamposDePara();
        Task<TesteIntegracaoRetorno> TesteIntegracao(EmpresaIntegracao dadosParaConsulta);
    }
}
