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
    public class EmpresaIntegracaoApplication<TContext> : ApplicationBase<TContext, EmpresaIntegracao>, IEmpresaIntegracaoApplication<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        public new readonly IEmpresaIntegracaoService<TContext> _service;
        public EmpresaIntegracaoApplication(IUnitOfWork<TContext> context, IEmpresaIntegracaoService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }
    }
}

