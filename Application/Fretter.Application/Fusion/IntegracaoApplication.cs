using Fretter.Domain.Dto.EmpresaIntegracao;
using Fretter.Domain.Dto.Fusion;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fretter.Application
{
    public class IntegracaoApplication<TContext> : ApplicationBase<TContext, Integracao>, IIntegracaoApplication<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        public new readonly IIntegracaoService<TContext> _service;
        public IntegracaoApplication(IUnitOfWork<TContext> context, IIntegracaoService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }

        public List<DeParaEmpresaIntegracao> BuscaCamposDePara()
        {
            return _service.BuscaCamposDePara();
        }
        public Task<TesteIntegracaoRetorno>TesteIntegracao(EmpresaIntegracao dadosParaConsulta)
        {
            return _service.TesteIntegracao(dadosParaConsulta);
        }
    }
}

