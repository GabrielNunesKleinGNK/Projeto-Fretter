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
    public class EmpresaTransporteTipoApplication<TContext> : ApplicationBase<TContext, EmpresaTransporteTipo>, IEmpresaTransporteTipoApplication<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        public new readonly IEmpresaTransporteTipoService<TContext> _service;
        public EmpresaTransporteTipoApplication(IUnitOfWork<TContext> context, IEmpresaTransporteTipoService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }
    }
}

