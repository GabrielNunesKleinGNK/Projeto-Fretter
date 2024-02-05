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
    public class EntregaOrigemIntegracaoApplication<TContext> : ApplicationBase<TContext, EntregaOrigemImportacao>, IEntregaOrigemImportacaoApplication<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        public new readonly IEntregaOrigemImportacaoService<TContext> _service;
        public EntregaOrigemIntegracaoApplication(IUnitOfWork<TContext> context, IEntregaOrigemImportacaoService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }
    }
}

