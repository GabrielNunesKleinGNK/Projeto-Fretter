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
    public class IntegracaoTipoApplication<TContext> : ApplicationBase<TContext, IntegracaoTipo>, IIntegracaoTipoApplication<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        public new readonly IIntegracaoTipoService<TContext> _service;
        public IntegracaoTipoApplication(IUnitOfWork<TContext> context, IIntegracaoTipoService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }
    }
}

